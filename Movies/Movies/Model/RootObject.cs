using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Model
{
   public class RootObject
    {
 
        public Result[] results { get; set; }

        public long page { get; set; }

        public long total_results { get; set; }

        public Dates dates { get; set; }

        public long total_pages { get; set; }
    }
    public partial class Dates
    {
        public DateTimeOffset maximum { get; set; }

        public DateTimeOffset minimum { get; set; }
    }
}
