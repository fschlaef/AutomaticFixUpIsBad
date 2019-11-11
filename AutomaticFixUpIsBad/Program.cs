using AutomaticFixUpIsBad.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AutomaticFixUpIsBad
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new AutomaticFixUpIsBadContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Customer.Add(new Customer());
            context.SaveChanges();

            var invoices = new List<Invoice>();

            // Create invoices for our customer
            for (int i = 0; i < 100; i++)
            {
                invoices.Add(new Invoice() { CustomerFk = 1 });
            }

            context.Invoice.AddRange(invoices);
            context.SaveChanges();

            var invoiceLines = new List<InvoiceLine>();
            var rand = new Random();

            // Create invoice lines attached to random invoices
            for (int i = 0; i < 100; i++)
            {
                invoiceLines.Add(new InvoiceLine() { InvoiceFk = rand.Next(1, 100) });
            }

            context.InvoiceLine.AddRange(invoiceLines);
            context.SaveChanges();

            var customer = context.Customer
                .Select(c => new Customer()
                {
                    Invoice = c.Invoice  // I don't want invoice lines, only invoices
                })
                .First();

            Debug.WriteLine("Customer invoices : " + customer.Invoice.Count());
            
            if(customer.Invoice.Any(i => i.InvoiceLine.Count > 0))
            {
                Debug.WriteLine("Invoice line found despite never wanted !");
                Debugger.Break();
            }

            // Same query but reinstanciated context
            using var context2 = new AutomaticFixUpIsBadContext();
            var customer2 = context2.Customer
                .Select(c => new Customer()
                {
                    Invoice = c.Invoice  // I don't want invoice lines, only invoices
                })
                .First();

            Debug.WriteLine("Customer 2 invoices : " + customer.Invoice.Count());

            if (customer2.Invoice.Any(i => i.InvoiceLine.Count > 0))
            {
                Debug.WriteLine("Invoice line found despite never wanted !");
                Debugger.Break(); // This will never be hit
            }
        }
    }
}
