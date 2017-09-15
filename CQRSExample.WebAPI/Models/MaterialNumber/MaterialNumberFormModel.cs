using CQRSExample.Model.MaterialNumber;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CQRSExample.WebAPI.Models.MaterialNumber
{
    public class MaterialNumberFormModel : MaterialNumberData
    {
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MaxQuantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MinQuantity { get; set; }

        [Required]
        public bool Renner { get; set; }
    }
}