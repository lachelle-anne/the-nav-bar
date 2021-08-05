using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Areas.Administration.Models.Table
{
    public class Table
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int SectionId { get; set; }
        public Data.Section Section { get; set; }
        public int Seats { get; set; }
    }
}
