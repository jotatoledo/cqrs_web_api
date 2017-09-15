namespace CQRSExample.Model.Plant
{
    public interface PlantData
    {
        string Id { get; set; }
        string Name { get; set; }
        string SAPId { get; set; }
        string SAPWarehouse { get; set; }
    }
}