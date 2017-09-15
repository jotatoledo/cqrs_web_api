using CQRSExample.Model.Plant;
using System.ComponentModel.DataAnnotations;

namespace CQRSExample.WebAPI.Models.Plant
{
    public class PlantFormModel : PlantData
    {
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Id { set; get; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { set; get; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string SAPId { set; get; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string SAPWarehouse { set; get; }
    }
}