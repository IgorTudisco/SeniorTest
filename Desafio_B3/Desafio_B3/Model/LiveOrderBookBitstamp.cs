using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio_B3.Model;

public class LiveOrderBookBitstamp
{
    [Key]
    [Required]
    public int Id { get; set; }

    [DefaultValue(2211.00)]
    public decimal Ask { get; set; } = 2211.00M;             // "ask": "2211.00"

    [DefaultValue(2188.97)]
    public decimal Bid { get; set; } = 2188.97M;             // "bid": "2188.97"

    [DefaultValue(2811.00)]
    public decimal High { get; set; } = 2811.00M;            // "high": "2811.00"

    [DefaultValue(2211.00)]
    public decimal Last { get; set; } = 2211.00M;            // "last": "2211.00"

    [DefaultValue(2188.97)]
    public decimal Low { get; set; } = 2188.97M;             // "low": "2188.97"

    [DefaultValue(2211.00)]
    public decimal Open { get; set; } = 2211.00M;            // "open": "2211.00"

    [DefaultValue(2211.00)]
    public decimal Open24 { get; set; } = 2211.00M;          // "open_24": "2211.00"

    [DefaultValue(13.57)]
    public decimal PercentChange24 { get; set; } = 13.57M;   // "percent_change_24": "13.57"

    [DefaultValue("0")]
    public string Side { get; set; } = "0";                  // "side": "0"

    [DefaultValue(1643640186)]
    public long Timestamp { get; set; } = 1643640186L;       // "timestamp": "1643640186"

    [DefaultValue(213.26801100)]
    public decimal Volume { get; set; } = 213.26801100M;     // "volume": "213.26801100"

    [DefaultValue(2189.80)]
    public decimal Vwap { get; set; } = 2189.80M;            // "vwap": "2189.80"
}
