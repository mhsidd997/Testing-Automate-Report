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
    
    public partial class OfferProvince
    {
        public int OfferProvinceId { get; set; }
        public Nullable<int> OfferId { get; set; }
        public Nullable<int> CityId { get; set; }
    
        public virtual City City { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
