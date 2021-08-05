using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Data
{
    public class ReservationTable
    {
        public int Id { get; set; }
        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }
        public Table Table { get; set; }

        public int TableId { get; set; }
    }
}
