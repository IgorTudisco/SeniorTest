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

    public bool AcionarOrder(LiveOrderBookBitstamp? LiveOrderBookBitstamp)
    {
        bool adicionado = false;

        if (LiveOrderBookBitstamp != null)
        {
            _context.BitstampOrders.Add(LiveOrderBookBitstamp);
            _context.SaveChanges();
            adicionado = true;
        }

        return adicionado;
    }


    public List<LiveOrderBookBitstamp>? FindOrders()
    {
        List<LiveOrderBookBitstamp>? result = _context.BitstampOrders.ToList();

        if (result.IsNullOrEmpty())
        {
            return null;
        }
        else
        {
            return result;
        }
    }


    public LiveOrderBookBitstamp? FindOrderById(int id)
    {

        LiveOrderBookBitstamp? findingOrders = _context.BitstampOrders.FirstOrDefault(LiveOrderBookBitstamp => LiveOrderBookBitstamp.Id == id);

        if (findingOrders == null)
        {
            return null;
        }
        else
        {

            return findingOrders;
        }
    }

    public String? DeleteOrders(int id)
    {

        LiveOrderBookBitstamp? findingOrders = _context.BitstampOrders.FirstOrDefault(LiveOrderBookBitstamp => LiveOrderBookBitstamp.Id == id);

        if (findingOrders == null)
        {
            return null;
        }
        else
        {
            _context.Remove(findingOrders);
            _context.SaveChanges();
            return "ok";
        }

    }

}
