namespace PocketBook.BLL.Services.Statics;

public static class LeastSquares
{
    private static double CalculateA(List<decimal> sample, List<int> timeSymbol)
    {
        var length = sample.Count;
            
        var minuend = sample.Select((s, i) => s * timeSymbol[i]).Sum();
        var subtrahend = timeSymbol.Sum() * sample.Sum() / length;
        var divider = timeSymbol.Sum(x => Math.Pow(x, 2)) - Math.Pow(timeSymbol.Sum(), 2) / length;
                
        return (double)Math.Round((minuend - subtrahend) / (decimal)divider, 5);
    }

    private static double CalculateB(List<decimal> sample, List<int> timeSymbol,
        double a)
    {
        var length = sample.Count;

        var minuend = sample.Sum();
        var subtrahend = a * timeSymbol.Sum();

        return (double)Math.Round((minuend - (decimal)subtrahend) / length, 5);
    }

    public static decimal ForecastForOneValue(List<decimal> sample, List<(int year, int month)> timeSymbol)
    {
        if (sample.Count != timeSymbol.Count)
            throw new Exception("Массивы должны быть одинаковыми по длинне");

        var normalizedTimeSymbol = timeSymbol
            .Select(x => DateTime.DaysInMonth(x.year, x.month))
            .ToList();
        
        var a = CalculateA(sample, normalizedTimeSymbol);
        var b = CalculateB(sample, normalizedTimeSymbol, a);
        var lastTimeSymbol = timeSymbol.Last();
        
        var nextTimeSymbol = new DateTime(lastTimeSymbol.year, lastTimeSymbol.month, 1).AddMonths(1);
        var nextNormalizedTimeSymbol = DateTime.DaysInMonth(nextTimeSymbol.Year, nextTimeSymbol.Day);
            
        return (decimal)Math.Round(a * nextNormalizedTimeSymbol + b, 2);
    }
}