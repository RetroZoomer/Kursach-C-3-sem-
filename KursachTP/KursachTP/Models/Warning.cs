using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KursachTP.Models
{
    public class Warning
    {
        public Warning(string f0, string f1, string f2, string f3, string f4)
        {
            WarningID = Convert.ToInt32(f0);
            NameUser = f1;
            LastNameUser = f2;
            WarningDescription = f3;
            WarningTime = Convert.ToDateTime(f4);
        }
        public Warning()
        {

        }
        [Required]
        public int? WarningID { get; set; }
        [Required]
        public string? NameUser { get; set; }
        [Required]
        public string? LastNameUser { get; set; }
        [Required]

        public string? WarningDescription { get; set; }
        [Required]
        public DateTime? WarningTime { get; set; }

    }
    public static class Warnings
    {
        public static List<Warning> warnings = new List<Warning>();
    }
}
