using Desafio_B3.Model;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_B3.Controllers;

[ApiController]
[Route("[Controller]")]
public class LiveOrderBookController : ControllerBase
{

    private static List<Bitstamp> bitstamps = new List<Bitstamp>();
    private static int Id = 0;

    [HttpPost]
    public IActionResult AcionarDadosBitstamps([FromBody] Bitstamp bitstamp)
    {
        bitstamp.Id = Id++;
        bitstamps.Add(bitstamp);

        return CreatedAtAction(nameof(GetBitstampId), new { id = bitstamp.Id }, bitstamp);

    }

    [HttpGet]
    public IEnumerable<Bitstamp> GetBitstamps()
    {
        return bitstamps;
    }

    [HttpGet("{id}")]
    public Bitstamp? GetBitstampId(int id)
    {
        return bitstamps.FirstOrDefault(bitstamp => bitstamp.Id == id);
    }
}
