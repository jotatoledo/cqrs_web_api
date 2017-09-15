namespace CQRSExample.Model.MaterialNumber
{
    public interface MaterialNumberData
    {
        string Id { get; set; }
        int MaxQuantity { get; set; }
        int MinQuantity { get; set; }
        bool Renner { get; set; }
    }
}