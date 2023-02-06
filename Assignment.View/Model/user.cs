using System;

namespace Assignment.View.Model
{
    public class user
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateOnly dateofbirth { get; set; }
        public int gender { get; set; }
        public string emailaddress { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
    }
}
