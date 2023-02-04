using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KursachTP.Models
{
    public class Hobby
    {
        public Hobby(string f0, string f1)
        {
            Hobby_id = Convert.ToInt32(f0);
            Category = f1;
        }
        public Hobby()
        {

        }
        [Required]
        public int? Hobby_id { get; set; }
        [Required]
        public string? Category { get; set; }

        public static class Hobbys
        {
            public static List<Hobby> hobbys = new List<Hobby>();
        }
    }
}
