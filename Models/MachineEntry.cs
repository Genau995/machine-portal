using System;

namespace MachinePortal.Models
{
    public class MachineEntry
    {
        public int Id { get; set; }
        public string TechnicianName { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string MachineType { get; set; }
        public string SerialNumber { get; set; }
        public string SoftwareVersion { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}