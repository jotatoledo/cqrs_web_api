namespace CQRSExample.Model.MaterialNumber
{
    public class MaterialNumberDetails
    {
        public string Id { get; set; }
        public int MaxQuantity { get; set; }
        public int MinQuantity { get; set; }
        public bool Renner { get; set; }
        public int WorkCenterCount { get; set; }
    }
}