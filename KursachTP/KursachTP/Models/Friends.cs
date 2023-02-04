using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KursachTP.Models;

namespace KursachTP.Models
{
    public class Friend
    {
        public Friend(string f0, string f1, string f2, string f3, string f4, string f5)
        {
            UserID = Convert.ToInt32(f0);
            Name = f1;
            LastName = f2;
            Birthday = f3.Substring(0, 11);
            if (f4 == "1")
            {
                Pol = true;
            }
            else
            {
                Pol = false;
            }
            Phone = f5;
        }
        public Friend()
        {

        }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Birthday { get; set; }
        [Required]
        public bool? Pol { get; set; }
        [Required]
        public string? Phone { get; set; }
    }
    public static class Friends
    {
        public static List<Friend> friends = new List<Friend>();
    }
}
