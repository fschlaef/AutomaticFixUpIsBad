using System;
using System.Collections.Generic;

namespace AutomaticFixUpIsBad.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceLine = new HashSet<InvoiceLine>();
        }

        public int Id { get; set; }
        public int CustomerFk { get; set; }

        public virtual Customer CustomerFkNavigation { get; set; }
        public virtual ICollection<InvoiceLine> InvoiceLine { get; set; }
    }
}
