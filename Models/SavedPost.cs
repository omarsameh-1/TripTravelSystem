//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TripTravelSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SavedPost
    {
        public int USERid { get; set; }
        public int POSTid { get; set; }
        public Nullable<bool> saved { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
