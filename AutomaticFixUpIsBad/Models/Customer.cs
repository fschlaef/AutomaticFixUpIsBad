using System;
using System.Collections.Generic;

namespace AutomaticFixUpIsBad.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int Id { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
