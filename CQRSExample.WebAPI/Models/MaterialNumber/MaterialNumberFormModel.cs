using CQRSExample.Model.MaterialNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRSExample.WebAPI.Models.MaterialNumber
{
    public class MaterialNumberFormModel : MaterialNumberData
    {
        public string Id { get; set; }
    }
}