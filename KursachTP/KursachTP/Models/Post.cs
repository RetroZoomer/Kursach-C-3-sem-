using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KursachTP.Models
{
    public class Post
    {
        private string header;
        public static string Header
        {
            get
            {
                return Header;
            }
            set
            {
                Header = value;
            }
        }
        private string text;
        public static string Text
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }
        List<string> comments = new List<string>();

        public Post(string f0, string f1, string f2, string f3, string f4, string f5)
        {
            PostID = Convert.ToInt32(f0);
            UserID = Convert.ToInt32(f1);
            PostTitle = f2;
            PostDescription = f3;
            StartTime = Convert.ToDateTime(f4);
            Hide = Convert.ToBoolean(f5);
        }
        public Post()
        {

        }
        [Required]
        public int? PostID { get; set; }
        [Required]
        public int? UserID { get; set; }
        [Required]
        public string? PostTitle { get; set; }
        [Required]
        public string? PostDescription { get; set; }
        [Required]
        public DateTime? StartTime { get; set; }
        [Required]
        public bool? Hide { get; set; }

        //[Range(1, 150, ErrorMessage = "Incorrect age")]
        //public int Age { get; set; }
    }
    public static class Posts
    {
        public static List<Post> posts = new List<Post>();
    }

}

