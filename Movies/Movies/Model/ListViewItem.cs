using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Model
{
    public class ListViewItem
    {
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<int> genre_ids { get; set; }
        public string title { get; set; }
        public double vote_average { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public static implicit operator List<object>(ListViewItem v)
        {
            throw new NotImplementedException();
        }
    }
}
