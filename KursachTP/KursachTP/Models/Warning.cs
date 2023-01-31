using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KursachTP.Models
{
    public class Warning
    {
        public Warning(string f0, string f1, string f2, string f3, string f4, string f5, string f6)
        {
            WarningID = Convert.ToInt32(f0);
            UserID = Convert.ToInt32(f1);
            NameUser = f2;
            LastNameUser = f3;
            WarningDescription = f4;
            WarningTime = Convert.ToDateTime(f5);
            PostID = Convert.ToInt32(f6);


        }
        public Warning()
        {

        }
        [Required]
        public int? WarningID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string? NameUser { get; set; }
        [Required]
        public string? LastNameUser { get; set; }
        [Required]

        public string? WarningDescription { get; set; }
        [Required]
        public DateTime? WarningTime { get; set; }
        [Required]
        public int PostID { get; set; }
        [Required]
        public int WarningCount { get; set; }
    }
    public static class Warnings
    {
        public static List<Warning> warnings = new List<Warning>();
    }
}
