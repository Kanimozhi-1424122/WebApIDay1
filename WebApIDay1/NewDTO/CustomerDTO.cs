using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApIDay1.NewDTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string customerName { get; set; }
        public System.DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
        public string city { get; set; }
        public int MemberShipTypeId { get; set; }

        
    }
}