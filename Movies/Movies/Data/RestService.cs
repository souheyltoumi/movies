using Movies.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace Movies.Data
{
    public class RestService : INotifyPropertyChanged
    {
        public RootObject _rootObject { get; set; }
        public RootObject RootObject
        {
            get
            {
                return RootObject;
            }
            set
            {
                if (value != _rootObject)
                {
                    _rootObject = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public List<Result> _resultsList{ get; set; }
        public List<Result> ResultsList
        {
            get
            {
                return ResultsList;
            }
            set
            {
                if (value != _resultsList)
                {
                    _resultsList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RestService()
        {
            GetDataAsync();
        }
        public async void GetDataAsync()
        {
            HttpClient httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync("https://api.themoviedb.org/3/movie/now_playing?api_key=46babd1c0b67a27afb31c7766f0f5589&language=en-US");
            if (reponse.IsSuccessStatusCode)
            {
                var content = await reponse.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<RootObject>(content);
                 var obj = JsonConvert.DeserializeObject<List<Result>>(Convert.ToString(json.results));
                ResultsList = new List<Result> (obj);
                ResultsList.Reverse();

            }
        }


    }


}