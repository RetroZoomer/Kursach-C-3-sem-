using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KursachTP.Models
{
    public class Profile
    {
        public Profile(string f0, string f1, string f2, string f3, string f4, string f5)
        {
            Name = f0;
            LastName = f1;
            UserDescription = f2;
            Birthday = Convert.ToDateTime(f3);
            Pol = Convert.ToBoolean(f4);
            UserID = Convert.ToString(f5);
        }
        public Profile()
        {

        }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserDescription { get; set; }
        [Required]
        public DateTime? Birthday { get; set; }
        [Required]
        public bool? Pol { get; set; }
        [Required]
        public string? UserID { get; }
    }

    public static class Profiles
    {
        public static List<Profile> profiles = new List<Profile>();
    }
    public static class Hobbys
    {
        public static List<Hobby> hobbys = new List<Hobby>();
    }
}
