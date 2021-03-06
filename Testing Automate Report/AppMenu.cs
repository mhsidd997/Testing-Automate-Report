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
    
    public partial class AppMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AppMenu()
        {
            this.InstitutionParameterAtts = new HashSet<InstitutionParameterAtt>();
            this.InstitutionParameterValues = new HashSet<InstitutionParameterValue>();
            this.AppMenuTemplates = new HashSet<AppMenuTemplate>();
            this.TransactionLogs = new HashSet<TransactionLog>();
            this.UserFavorites = new HashSet<UserFavorite>();
        }
    
        public int AppMenuID { get; set; }
        public string MenuItem { get; set; }
        public string Color { get; set; }
        public Nullable<int> IconLibraryID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public bool IsActive { get; set; }
        public string ServiceName { get; set; }
        public Nullable<int> AppMenuGroupID { get; set; }
        public Nullable<int> TransactionCodeID { get; set; }
        public Nullable<int> Sequence { get; set; }
        public Nullable<int> ProductCodeID { get; set; }
    
        public virtual AppMenuGroup AppMenuGroup { get; set; }
        public virtual ProductCode ProductCode { get; set; }
        public virtual TransactionCode TransactionCode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstitutionParameterAtt> InstitutionParameterAtts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstitutionParameterValue> InstitutionParameterValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppMenuTemplate> AppMenuTemplates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionLog> TransactionLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
    }
}
