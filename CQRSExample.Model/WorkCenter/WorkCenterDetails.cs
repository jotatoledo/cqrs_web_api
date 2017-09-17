using CQRSExample.Model.Plant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSExample.Model.WorkCenter
{
    public class WorkCenterDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public PlantDetails Plant { get; set; }
        public int MaterialNumberCount { get; set; }
    }
}