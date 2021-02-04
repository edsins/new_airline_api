﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class new_airlineEntities : DbContext
    {
        public new_airlineEntities()
            : base("name=new_airlineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<cancellation> cancellations { get; set; }
        public virtual DbSet<credit_card> credit_card { get; set; }
        public virtual DbSet<flight_cost> flight_cost { get; set; }
        public virtual DbSet<Flight_Master> Flight_Master { get; set; }
        public virtual DbSet<flight_schedule> flight_schedule { get; set; }
        public virtual DbSet<passenger> passengers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User_Master> User_Master { get; set; }
    
        public virtual ObjectResult<string> fetchseat(Nullable<int> flight_number, Nullable<System.DateTime> booking_date)
        {
            var flight_numberParameter = flight_number.HasValue ?
                new ObjectParameter("flight_number", flight_number) :
                new ObjectParameter("flight_number", typeof(int));
    
            var booking_dateParameter = booking_date.HasValue ?
                new ObjectParameter("booking_date", booking_date) :
                new ObjectParameter("booking_date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("fetchseat", flight_numberParameter, booking_dateParameter);
        }
    
        public virtual ObjectResult<searchflightbyday_Result> searchflightbyday(string departure, string arrival, string day)
        {
            var departureParameter = departure != null ?
                new ObjectParameter("departure", departure) :
                new ObjectParameter("departure", typeof(string));
    
            var arrivalParameter = arrival != null ?
                new ObjectParameter("arrival", arrival) :
                new ObjectParameter("arrival", typeof(string));
    
            var dayParameter = day != null ?
                new ObjectParameter("day", day) :
                new ObjectParameter("day", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<searchflightbyday_Result>("searchflightbyday", departureParameter, arrivalParameter, dayParameter);
        }
    
        public virtual int searchflightbynumberofseats(string departure, string arrival, Nullable<System.DateTime> traveldate, Nullable<int> no_of_seats)
        {
            var departureParameter = departure != null ?
                new ObjectParameter("departure", departure) :
                new ObjectParameter("departure", typeof(string));
    
            var arrivalParameter = arrival != null ?
                new ObjectParameter("arrival", arrival) :
                new ObjectParameter("arrival", typeof(string));
    
            var traveldateParameter = traveldate.HasValue ?
                new ObjectParameter("traveldate", traveldate) :
                new ObjectParameter("traveldate", typeof(System.DateTime));
    
            var no_of_seatsParameter = no_of_seats.HasValue ?
                new ObjectParameter("no_of_seats", no_of_seats) :
                new ObjectParameter("no_of_seats", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("searchflightbynumberofseats", departureParameter, arrivalParameter, traveldateParameter, no_of_seatsParameter);
        }
    
        public virtual int sp_Addflightschedule(Nullable<int> flightno, string day)
        {
            var flightnoParameter = flightno.HasValue ?
                new ObjectParameter("flightno", flightno) :
                new ObjectParameter("flightno", typeof(int));
    
            var dayParameter = day != null ?
                new ObjectParameter("day", day) :
                new ObjectParameter("day", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Addflightschedule", flightnoParameter, dayParameter);
        }
    
        public virtual ObjectResult<string> sp_getseats(Nullable<int> flight_number, Nullable<System.DateTime> traveldate)
        {
            var flight_numberParameter = flight_number.HasValue ?
                new ObjectParameter("flight_number", flight_number) :
                new ObjectParameter("flight_number", typeof(int));
    
            var traveldateParameter = traveldate.HasValue ?
                new ObjectParameter("traveldate", traveldate) :
                new ObjectParameter("traveldate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_getseats", flight_numberParameter, traveldateParameter);
        }
    
        public virtual ObjectResult<sp_searchflight_Result> sp_searchflight(string departure, string arrival, Nullable<System.DateTime> traveldate, Nullable<int> no_of_seats, string day)
        {
            var departureParameter = departure != null ?
                new ObjectParameter("departure", departure) :
                new ObjectParameter("departure", typeof(string));
    
            var arrivalParameter = arrival != null ?
                new ObjectParameter("arrival", arrival) :
                new ObjectParameter("arrival", typeof(string));
    
            var traveldateParameter = traveldate.HasValue ?
                new ObjectParameter("traveldate", traveldate) :
                new ObjectParameter("traveldate", typeof(System.DateTime));
    
            var no_of_seatsParameter = no_of_seats.HasValue ?
                new ObjectParameter("no_of_seats", no_of_seats) :
                new ObjectParameter("no_of_seats", typeof(int));
    
            var dayParameter = day != null ?
                new ObjectParameter("day", day) :
                new ObjectParameter("day", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_searchflight_Result>("sp_searchflight", departureParameter, arrivalParameter, traveldateParameter, no_of_seatsParameter, dayParameter);
        }
    
        public virtual ObjectResult<sp_booked_history_Result> sp_booked_history(Nullable<int> userid)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_booked_history_Result>("sp_booked_history", useridParameter);
        }
    }
}
