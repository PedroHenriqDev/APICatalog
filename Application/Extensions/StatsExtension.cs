using Communication.DTOs.Responses;

namespace Application.Extensions;

public static class StatsExtension
{
    public static double StandartLeviation(this IEnumerable<double> set)
    {
        if (set.Any())
        {
            var variance = set.Variance();

            return Math.Sqrt(variance);
        }

        return 0;
    }

    public static double Variance(this IEnumerable<double>? set)
    {
        if (set.Any())
        {
            var avarage = set.Average(x => x);

            var variance = set.Select(num => Math.Pow(num - avarage, 2)).Average();

            return variance;
        }

        return 0;
    }

    public static double CalculateAvarage(this IEnumerable<double> set) 
    {
        if (set.Any())
        {
            return set.Average(x => x);
        }

        return 0;
    }

    public static double ModeWithLinq(this IEnumerable<double> set) 
    {
        if (set.Any())
        {
            return set.GroupBy(num => num)
                       .OrderByDescending(group => group.Count())
                       .First()
                       .Key;
        }

        return 0;
    }

    public static double ModeWithoutLinq(this IEnumerable<double> set)
    {
        if (set.Any())
        {
            Dictionary<double, int> numberCounts = [];

            foreach (var number in set)
            {
                if (numberCounts.TryGetValue(number, out int value))
                {
                    numberCounts[number] = ++value;
                }
                else
                {
                    numberCounts[number] = 1;
                }
            }

            double mode = 0.0;
            int maxValue = 0;
            foreach (var pair in numberCounts)
            {
                if (pair.Value > maxValue)
                {
                    maxValue = pair.Value;
                    mode = pair.Key;
                }
            }

            return mode;
        }

        return 0.0;
    }


    public static void Ranking(this IEnumerable<ResponseCategoryStatsDTO> categoriesStats)
    {
        int rank = 1;

        var categoriesStatsOrdered = categoriesStats.OrderByDescending(categoryStats => categoryStats.ProductsAvarage).ToList();

        foreach (var stats in categoriesStatsOrdered)
        {
            stats.Ranking = rank++;
        }
    }

}
