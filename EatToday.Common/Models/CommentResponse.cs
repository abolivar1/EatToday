using System;
using System.Collections.Generic;
using System.Text;

namespace EatToday.Common.Models
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public string Remarks { get; set; }
        public DateTime Date { get; set; }

        public string Customer { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

    }
}
