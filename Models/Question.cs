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
    
    public partial class Question
    {
        public int qID { get; set; }
        public string question1 { get; set; }
        public string answer { get; set; }
        public System.DateTime questionDate { get; set; }
        public int UID { get; set; }
        public int agencyID { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
