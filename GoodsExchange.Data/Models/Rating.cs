using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.Data.Models
{
    public class Rating
    {
        public Guid RatingId { get; set; }
        public int NumberStars { get; set; }
        public string Feedback { get; set; }
        public Guid RatingUserId { get; set; }
        public User RatingGiven { get; set; }
        public Guid TargetUserId { get; set; }
        public User RatingReceived { get; set; }
    }
}
