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
    
    public partial class AdvertisementSet
    {
        public int AdvertisementSetID { get; set; }
        public int AdvertisementID { get; set; }
        public int AdvertisementGroupID { get; set; }
    
        public virtual Advertisement Advertisement { get; set; }
        public virtual AdvertisementGroup AdvertisementGroup { get; set; }
    }
}
