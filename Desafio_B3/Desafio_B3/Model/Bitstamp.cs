using System.ComponentModel.DataAnnotations;

namespace Desafio_B3.Model;

public class Bitstamp
{
    [Key]
    [Required]
    public int Id { get; set; }

    // Taken from the website https://www.bitstamp.net/api/#tag/Tickers/operation/GetMarketTicker
    public decimal Ask { get; set; }             // "ask": "2211.00"
    public decimal Bid { get; set; }             // "bid": "2188.97"
    public decimal High { get; set; }            // "high": "2811.00"
    public decimal Last { get; set; }            // "last": "2211.00"
    public decimal Low { get; set; }             // "low": "2188.97"
    public decimal Open { get; set; }            // "open": "2211.00"
    public decimal Open24 { get; set; }          // "open_24": "2211.00"
    public decimal PercentChange24 { get; set; } // "percent_change_24": "13.57"
    public string Side { get; set; }             // "side": "0"
    public long Timestamp { get; set; }          // "timestamp": "1643640186"
    public decimal Volume { get; set; }          // "volume": "213.26801100"
    public decimal Vwap { get; set; }            // "vwap": "2189.80"

}
