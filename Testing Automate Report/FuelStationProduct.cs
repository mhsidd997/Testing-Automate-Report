//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Testing_Automate_Report
{
    using System;
    using System.Collections.Generic;
    
    public partial class FuelStationProduct
    {
        public int FuelStationProductID { get; set; }
        public int FuelStationID { get; set; }
        public int ProductGroupID { get; set; }
    
        public virtual FuelStation FuelStation { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
    }
}
