//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CQRSExample.Data.Sql.StarterDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkCenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkCenter()
        {
            this.MaterialNumber = new HashSet<MaterialNumber>();
        }
    
        public int Poid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int PlantPoid { get; set; }
    
        public virtual Plant Plant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialNumber> MaterialNumber { get; set; }
    }
}