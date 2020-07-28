using Movies.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        string day, month, year,date;
        
        List<Language> languages;
        public Search()
        {
            InitializeComponent();
             languages = new List<Language>()
            {
                new Language(){name =" english"  , value= "en"},
                new Language(){name =" spanich"  , value= "es"},
                new Language(){name =" koriean"  , value= "ko"},
                new Language(){name =" no value"  , value= "nv"},

            };
            picker.ItemsSource = languages;
            picker.SelectedIndex = 3;
            InitList();
        }

        public List<ListViewItem> Items { get; private set; }

        public async void InitList()
        {
            Items = new List<ListViewItem>();

            HttpClient httpClient = new HttpClient();
            var reponse = await httpClient.GetAsync("https://api.themoviedb.org/3/movie/now_playing?api_key=46babd1c0b67a27afb31c7766f0f5589&language=en-US");
            if (reponse.IsSuccessStatusCode)
            {
                var content = await reponse.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<RootObject>(content);

                foreach (var item in json.results)
                {
                    Items.Add(new ListViewItem
                    {
                        title = item.title,
                        release_date = item.release_date,
                        poster_path = "https://image.tmdb.org/t/p/w500" + item.poster_path,
                        popularity = item.popularity,
                        video = item.video,
                        adult = item.adult,
                        backdrop_path = item.backdrop_path,
                        original_language = item.original_language,
                        original_title = item.original_title,
                        genre_ids = item.genre_ids,
                        vote_average = item.vote_average,
                        overview = item.overview,
                        vote_count = item.vote_count,




                    });

                }
                exampleListView.ItemsSource = Items;


            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            exampleListView.ItemsSource = Items;
            picker.SelectedItem =languages[3];
            filter.Text = "";
            
        }



        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Language json = (Language)picker.SelectedItem;

            if (json.value.Equals("nv") || (picker.SelectedIndex == -1))
            {
                exampleListView.ItemsSource = Items.Where(s => s.title.Contains(e.NewTextValue.ToString()) || s.original_language.Contains(e.NewTextValue.ToString()) || s.overview.Contains(e.NewTextValue.ToString()))
                                                            .Select(s => s);
            }
            else
            {
                exampleListView.ItemsSource = Items.Where(s => s.original_language == json.value && (s.title.Contains(e.NewTextValue.ToString()) || s.original_language.Contains(e.NewTextValue.ToString()) || s.overview.Contains(e.NewTextValue.ToString())))
                                                    .Select(s => s);
            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Language json =(Language) picker.SelectedItem;

            if (json.value.Equals("nv")||(picker.SelectedIndex==-1))
            {
                if(string.IsNullOrWhiteSpace(filter.Text)||filter.Text.ToString()==""){
                    exampleListView.ItemsSource = Items;

                }
                else
                {
                    exampleListView.ItemsSource = Items.Where(s => s.title.Contains(filter.Text.ToString()) || s.original_language.Contains(filter.Text.ToString()) || s.overview.Contains(filter.Text.ToString()))
                                                           .Select(s => s);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(filter.Text) || filter.Text.ToString() == "")
                {
                    exampleListView.ItemsSource = Items.Where(s => s.original_language == json.value)
                                                                     .Select(s => s);
                }
                else
                {
                    exampleListView.ItemsSource = Items.Where(s => (s.title.Contains(filter.Text.ToString()) || s.original_language.Contains(filter.Text.ToString()) || s.overview.Contains(filter.Text.ToString())) && s.original_language == json.value)
                                                           .Select(s => s);
                }
     
                           
            }

        }

        private void ExampleListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as ListViewItem;
            var item1 = new Movie
            {
                title = item.title,
                release_date = item.release_date,
                poster_path = item.poster_path,

                overview = item.overview,
            };

            App.Current.MainPage = new DetailPage(item1);



        }

        private void Date_picker_DateSelected(object sender, DateChangedEventArgs e)
        {

            Language json = (Language)picker.SelectedItem;
            if (date_picker.Date.Day < 10)
            {
                day = "0" + date_picker.Date.Day.ToString();
            }
            else
            {
                day = date_picker.Date.Day.ToString();
            }
            if (date_picker.Date.Month < 10)
            {
                month = "0" + date_picker.Date.Month.ToString();
            }
            else
            {
                month = date_picker.Date.Day.ToString();
            }
            year= date_picker.Date.Year.ToString();
            date = year + "-" + month + "-" + day;
        
        if (json.value.Equals("nv") || (picker.SelectedIndex == -1))
        {
                if (string.IsNullOrWhiteSpace(filter.Text) || filter.Text.ToString() == "")
                {

                    exampleListView.ItemsSource = Items.Where(s => s.release_date == date)
                                                                .Select(s => s);
                }
                else
                {
                    exampleListView.ItemsSource = Items.Where(s =>( s.title.Contains(filter.Text.ToString()) || s.original_language.Contains(filter.Text.ToString()) || s.overview.Contains(filter.Text.ToString())) && s.release_date == date)
                                                           .Select(s => s);
                }

        }
        else
        {
                exampleListView.ItemsSource = Items.Where(s => (s.original_language == json.value && s.release_date == date)&& (s.title.Contains(filter.Text.ToString()) || s.original_language.Contains(filter.Text.ToString()) || s.overview.Contains(filter.Text.ToString())))
                                                    .Select(s => s);
        }

    }
    }
}