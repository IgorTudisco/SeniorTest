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

        // URL do WebSocket do Live Order Book
        private readonly string _webSocketUrl = "wss://ws.bitstamp.net";

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _client = new ClientWebSocket();

            // Iniciando a conexão com o WebSocket
            try
            {
                await _client.ConnectAsync(new Uri(_webSocketUrl), cancellationToken);
                Console.WriteLine("Conectado ao WebSocket.");

                // Substitua [currency_pair] por um par de moedas real, por exemplo, "btcusd"
                string channelName = "order_book_btcusd";
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

        // Método para cancelar a assinatura de um canal
        public async Task UnsubscribeFromChannel(string channelName, CancellationToken cancellationToken)
        {
            var unsubscribeMessage = new SubscriptionMessage("bts:unsubscribe", channelName);
            string messageJson = JsonConvert.SerializeObject(unsubscribeMessage);

            var bytes = Encoding.UTF8.GetBytes(messageJson);
            try
            {
                await _client?.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cancellationToken);
                Console.WriteLine($"Cancelada a assinatura do canal {channelName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar mensagem de cancelamento: {ex.Message}");
            }
        }

        // Método para receber dados do WebSocket
        private async Task ReceiveData(CancellationToken cancellationToken)
        {
            var buffer = new byte[1024 * 4];

            while (_client?.State == WebSocketState.Open)
            {
                try
                {
                    WebSocketReceiveResult result = await _client.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                    // Decodifica a mensagem recebida
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Mensagem recebida: {message}");

                    // Aqui você pode processar os dados do Live Order Book e salvá-los no banco de dados
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao receber dados: {ex.Message}");
                }
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
