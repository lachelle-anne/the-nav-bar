using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Data
{
    public class Table
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
    }
}
