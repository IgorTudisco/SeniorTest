using Desafio_B3.data;
using Desafio_B3.Model;
using Microsoft.IdentityModel.Tokens;

namespace Desafio_B3.Service;

public class LiveOrderBookService
{

    private LiveOrderBookContext _context;

    public LiveOrderBookService(LiveOrderBookContext context)
    {
        _context = context;
    }

    public bool AcionarDadosBitstamps(Bitstamp? bitstamp)
    {
        bool adicionado = false;

        if (bitstamp != null)
        {
            _context.Bitstamps.Add(bitstamp);
            _context.SaveChanges();
            adicionado = true;
        }

        return adicionado;
    }


    public List<Bitstamp>? FindBitstamp()
    {
        List<Bitstamp>? result = _context.Bitstamps.ToList();

        if (result.IsNullOrEmpty())
        {
            return null;
        }
        else
        {
            return result;
        }
    }


    public Bitstamp? FindBitstampById(int id)
    {

        Bitstamp? findingBitstamp = _context.Bitstamps.FirstOrDefault(bitstamp => bitstamp.Id == id);

        if (findingBitstamp == null)
        {
            return null;
        }
        else
        {

            return findingBitstamp;
        }
    }

    public String? DeleteBitstamp(int id)
    {

        Bitstamp? findingBitstamp = _context.Bitstamps.FirstOrDefault(bitstamp => bitstamp.Id == id);

        if (findingBitstamp == null)
        {
            return null;
        }
        else
        {
            _context.Remove(findingBitstamp);
            _context.SaveChanges();
            return "ok";
        }

    }

}
