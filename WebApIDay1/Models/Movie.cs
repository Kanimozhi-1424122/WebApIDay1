//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApIDay1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Movie
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string ActorName { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int AvailableStocks { get; set; }
    
        public virtual Genre Genre { get; set; }
    }
}