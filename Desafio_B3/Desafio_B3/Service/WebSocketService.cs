using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Desafio_B3.Model;

namespace Desafio_B3.Service
{
    public class WebSocketService : IHostedService
    {
        private ClientWebSocket? _client;
        private readonly IServiceProvider _serviceProvider; // Injeção do IServiceProvider

        public WebSocketService(IServiceProvider serviceProvider) // Remova o LiveOrderBookService aqui
        {
            _serviceProvider = serviceProvider;
        }

        // URL do WebSocket do Live Order Book
        private readonly String _webSocketUrl = "wss://ws.bitstamp.net";
        private readonly String _currencyPair = "order_book_btcusd";

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _client = new ClientWebSocket();

            // Iniciando a conexão com o WebSocket
            try
            {
                await _client.ConnectAsync(new Uri(_webSocketUrl), cancellationToken);
                Console.WriteLine("Conectado ao WebSocket.");

                // Substitua [currency_pair] por um par de moedas real, por exemplo, "btcusd"
                string channelName = _currencyPair;
                await SubscribeToChannel(channelName, cancellationToken);

                // Consumir os dados continuamente
                await ReceiveData(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ou assinar o WebSocket: {ex.Message}");
            }
        }

        // Método para assinar um canal
        public async Task SubscribeToChannel(string channelName, CancellationToken cancellationToken)
        {
            var subscribeMessage = new SubscriptionMessage("bts:subscribe", channelName);
            string messageJson = JsonConvert.SerializeObject(subscribeMessage);

            var bytes = Encoding.UTF8.GetBytes(messageJson);
            try
            {
                await _client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cancellationToken);
                Console.WriteLine($"Assinado ao canal {channelName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar mensagem de assinatura: {ex.Message}");
            }
        }

        // Método para receber dados do WebSocket
        private async Task ReceiveData(CancellationToken cancellationToken)
        {
            var buffer = new byte[1024 * 4]; // Buffer inicial
            var messageBuilder = new StringBuilder(); // Acumula partes da mensagem

            while (_client?.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    WebSocketReceiveResult result;
                    do
                    {
                        result = await _client.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                        var messagePart = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        messageBuilder.Append(messagePart);

                    } while (!result.EndOfMessage);

                    // Quando a mensagem completa for recebida
                    var completeMessage = messageBuilder.ToString();
                    Console.WriteLine($"Mensagem completa recebida: {completeMessage}");

                    // Deserializa a mensagem recebida em um objeto
                    var liveOrderBookBitstamp = JsonConvert.DeserializeObject<LiveOrderBookBitstamp>(completeMessage);

                    // Processa os dados recebidos
                    await ProcessOrderBookMessageAsync(liveOrderBookBitstamp, cancellationToken);

                    // Limpa o acumulador para a próxima mensagem
                    messageBuilder.Clear();
                }
                catch (WebSocketException wsEx)
                {
                    Console.WriteLine($"Erro no WebSocket: {wsEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao receber dados: {ex.Message}");
                }
            }
        }

        // Método para processar a mensagem recebida
        private async Task ProcessOrderBookMessageAsync(LiveOrderBookBitstamp liveOrderBook, CancellationToken cancellationToken)
        {
            // Criando um escopo para o serviço scoped
            using (var scope = _serviceProvider.CreateScope())
            {
                var liveOrderBookService = scope.ServiceProvider.GetRequiredService<LiveOrderBookService>();

                await Task.Run(() =>
                {
                    // Exemplo de manipulação de dados
                    Console.WriteLine($"Ask: {liveOrderBook.Ask}, Bid: {liveOrderBook.Bid}, High: {liveOrderBook.High}, Last: {liveOrderBook.Last}");

                    // Use o serviço scoped para processar a mensagem
                    liveOrderBookService.AcionarOrder(liveOrderBook);

                }, cancellationToken);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (_client?.State == WebSocketState.Open)
                {
                    await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Encerrando a conexão", cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao encerrar a conexão: {ex.Message}");
            }
            finally
            {
                _client?.Dispose();
            }
        }
    }
}
