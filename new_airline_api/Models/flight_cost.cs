//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace new_airline_api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class flight_cost
    {
        public int id { get; set; }
        public Nullable<int> flight_number { get; set; }
        public decimal economy_price { get; set; }
        public decimal business_price { get; set; }
    
        public virtual Flight_Master Flight_Master { get; set; }
    }
}
