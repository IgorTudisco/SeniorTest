using Desafio_B3.data;
using Desafio_B3.Model;
using Desafio_B3.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Desafio_B3.Controllers;

[ApiController]
[Route("[Controller]")]
public class LiveOrderBookController : ControllerBase
{

    private LiveOrderBookService _service;

    public LiveOrderBookController(LiveOrderBookService service)
    {
        _service = service;
    }



    [HttpPost]
    public IActionResult AcionarDadosBitstamps([FromBody] Bitstamp bitstamp)
    {
        bool adicionado = _service.AcionarDadosBitstamps(bitstamp);

        if (adicionado)
        {
            return CreatedAtAction(nameof(GetBitstampId), new { id = bitstamp.Id }, bitstamp);
        }
        else
        {
            
            return BadRequest();

        }


    }

    [HttpGet]
    public IActionResult GetBitstamps()
    {
        return Ok(_service.FindBitstamp());
    }

    [HttpGet("{id}")]
    public IActionResult GetBitstampId(int id)
    {
        Bitstamp? findingBitstamp = _service.FindBitstampById(id);

        if (findingBitstamp != null)
        {
            return Ok(findingBitstamp);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBitstamp(int id)
    {
        String? deletingBiststamp = _service.DeleteBitstamp(id);

        if (deletingBiststamp != null)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }

    }


}
