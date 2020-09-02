using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam
{
    public class Expense
    {
        public static int nextId;
        public int Id { get; private set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; }
        public string Name { get; set; }
        public string Client { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public Expense(decimal sum, DateTime date, string name, string client, string category, string description)
        {
            Sum = sum;
            Date = date;
            Name = name;
            Client = client;
            Category = category;
            Description = description;
            Id = Interlocked.Increment(ref nextId);
        }
    }
}
