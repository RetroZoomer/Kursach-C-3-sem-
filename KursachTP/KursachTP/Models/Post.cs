using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
