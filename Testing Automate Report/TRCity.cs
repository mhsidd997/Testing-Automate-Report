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
    
    public partial class TRCity
    {
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public bool IsActive { get; set; }
        public int ID { get; set; }
    
        public virtual TRCountry TRCountry { get; set; }
    }
}
