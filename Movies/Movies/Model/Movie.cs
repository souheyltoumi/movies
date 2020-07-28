using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Model
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
       
        public string poster_path { get; set; }

        public string title { get; set; }
        public string release_date { get; set; }
        public string overview { get; set; }


    }
}
