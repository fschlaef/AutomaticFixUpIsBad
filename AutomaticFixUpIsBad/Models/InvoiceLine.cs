using System;
using System.Collections.Generic;

namespace AutomaticFixUpIsBad.Models
{
    public partial class InvoiceLine
    {
        public int Id { get; set; }
        public int InvoiceFk { get; set; }

        public virtual Invoice InvoiceFkNavigation { get; set; }
    }
}
