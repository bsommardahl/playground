using System;

public static void Main(string[] args)
{
    var trailer = new ShippingTrailer();
    var result = trailer.Calculate(Data.TestOrders(), Config.MaxWeight);
    Console.WriteLine($"Max profit for {Config.MaxWeight} weight is: ${result}");

    var sum = (decimal)Data.RawMaterials().Sum(e => e.NetProfit);
    var exclude = sum - result;
    Console.WriteLine($"Which orders to exclude {exclude}");
}

public static class Config
{
    public const int MaxWeight = 200000;
}

public class Data
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Weight { get; set; }
    public double NetProfit { get; set; }

    public Data(int id, string description, int weight, double netProfit)
    {
        Id = id;
        Description = description;
        NetProfit = netProfit;
        Weight = weight;
    }

    public static List<Data> TestOrders()
    {
        return new List<Data>
        {
            new Data(1, "Some order number 1", 5724, 17.74),
            new Data(2, "Some order number 2", 9873, 37.12),
            new Data(3, "Some order number 3", 13492, 46.14),
            new Data(4, "Some order number 4", 7727, 30.44),
            new Data(5, "Some order number 5", 2924, 10.64),
            new Data(6, "Some order number 6", 1544, 5),
            new Data(7, "Some order number 7", 7082, 28.18),
            new Data(8, "Some order number 8", 13960, 50.82),
            new Data(9, "Some order number 9", 6371, 22.94),
            new Data(10, "Some order number 10", 14380, 53.2),
            new Data(11, "Some order number 11", 19045, 58.66),
            new Data(12, "Some order number 12", 14057, 13.72),
            new Data(13, "Some order number 13", 7082, 28.18),
            new Data(14, "Some order number 14", 13960, 50.82),
            new Data(15, "Some order number 15", 6371, 22.94),
            new Data(16, "Some order number 16", 13380, 53.2),
            new Data(17, "Some order number 17", 19045, 58.66),
            new Data(18, "Some order number 18", 7057, 12.72),
            new Data(19, "Some order number 19", 19045, 58.66),
            new Data(20, "Some order number 20", 6057, 3.72)
        };
    }
}

public class ShippingTrailer
{
    private static IList<Data> Items = new List<Data>();

    public decimal Calculate(IReadOnlyList<Data> data)
    {
        return (decimal)Calculate(Config.MaxWeight, data, data.Length);
    }

    private static double Calculate(double maxWeight, IReadOnlyList<Data> datas, int n)
    {
        var idx = n - 1;

        if (n <= 0 || maxWeight <= 0.0) return 0;
        var currentItem = datas[idx];
        if (currentItem.Weight > maxWeight)
        {
            return 0;
        }

        var a = currentItem.NetProfit + Calculate(maxWeight - currentItem.Weight, datas, idx);
        var b = Calculate(maxWeight, datas, idx);

        var result = Max(a, b);
        
        items.Add(result);
        
        return result.NetProfit;
    }

    private static double Max(double a, double b)
    {
        return a > b ? a : b;
    }
}