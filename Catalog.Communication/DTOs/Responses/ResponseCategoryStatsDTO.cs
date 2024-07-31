namespace Catalog.Communication.DTOs.Responses;

public class ResponseCategoryStatsDTO
{
    public string CategoryName { get; set; } = string.Empty;
    public double ProductsAvarage { get; set; }
    public double StandartLeviation { get; set; }
    public double Mode {  get; set; }
    public double Variance { get; set; }
    public int Ranking { get; set; }
    public int QuantityOfProducts { get; set; }
}
