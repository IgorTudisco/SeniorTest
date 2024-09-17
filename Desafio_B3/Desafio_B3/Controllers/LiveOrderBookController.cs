using Desafio_B3.data;
using Desafio_B3.Model;
using Desafio_B3.Service;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_B3.Controllers;

/// <summary>
/// Controlador para gerenciar operações no Live Order Book da API.
/// Permite realizar operações de CRUD sobre os dados de Bitstamp.
/// </summary>
[ApiController]
[Route("[Controller]")]
public class LiveOrderBookController : ControllerBase
{
    private readonly LiveOrderBookService _service;

    /// <summary>
    /// Construtor do controlador, injeta o serviço de LiveOrderBook.
    /// </summary>
    /// <param name="service">Serviço que contém a lógica de negócios para operações com Bitstamp.</param>
    public LiveOrderBookController(LiveOrderBookService service)
    {
        _service = service;
    }

    /// <summary>
    /// Adiciona um novo registro de Bitstamp no banco de dados.
    /// </summary>
    /// <param name="bitstampOrder">Objeto Bitstamp contendo as informações do novo registro.</param>
    /// <returns>Retorna o objeto criado com seu ID ou um erro caso a adição falhe.</returns>
    /// <response code="201">Retorna o Bitstamp recém-criado.</response>
    /// <response code="400">Retorna um erro se o Bitstamp não puder ser adicionado.</response>
    [HttpPost]
    public IActionResult AcionarOrderBitstamp([FromBody] LiveOrderBookBitstamp bitstampOrder)
    {
        bool adicionado = _service.AcionarOrder(bitstampOrder);

        if (adicionado)
        {
            return CreatedAtAction(nameof(GetOrderId), new { id = bitstampOrder.Id }, bitstampOrder);
        }
        else
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Retorna uma lista de todos os registros de Bitstamp.
    /// </summary>
    /// <returns>Uma lista de todos os Bitstamps registrados no banco de dados.</returns>
    /// <response code="200">Retorna a lista de Bitstamps.</response>
    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(_service.FindOrders());
    }

    /// <summary>
    /// Retorna os detalhes de um Bitstamp específico pelo ID.
    /// </summary>
    /// <param name="id">O ID do Bitstamp a ser retornado.</param>
    /// <returns>O Bitstamp correspondente ao ID ou um erro se não encontrado.</returns>
    /// <response code="200">Retorna o Bitstamp encontrado.</response>
    /// <response code="404">Retorna um erro se o Bitstamp não for encontrado.</response>
    [HttpGet("{id}")]
    public IActionResult GetOrderId(int id)
    {
        LiveOrderBookBitstamp? findingOrder = _service.FindOrderById(id);

        if (findingOrder != null)
        {
            return Ok(findingOrder);
        }
        else
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deleta um registro de Bitstamp pelo ID.
    /// </summary>
    /// <param name="id">O ID do Bitstamp a ser deletado.</param>
    /// <returns>Status da deleção: sem conteúdo ou erro.</returns>
    /// <response code="204">Retorna se o Bitstamp foi deletado com sucesso.</response>
    /// <response code="404">Retorna um erro se o Bitstamp não for encontrado.</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        String? deletingOrders = _service.DeleteOrders(id);

        if (deletingOrders != null)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}
