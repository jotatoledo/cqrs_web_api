using CQRSExample.Model.WorkCenter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CQRSExample.WebAPI.Models.WorkCenter
{
    public class WorkCenterFormModel : WorkCenterData
    {
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}