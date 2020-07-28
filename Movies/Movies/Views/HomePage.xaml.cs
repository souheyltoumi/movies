using Movies.Data;
using Movies.MasterDetailsPage;
using Movies.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class HomePage : ContentPage
    {

        List<ListViewItem> Items;

        public HomePage()
        {
            InitializeComponent();
            InitList();
        }
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
                        popularity=item.popularity,
                        video=item.video,
                        adult=item.adult,
                        backdrop_path=item.backdrop_path,
                        original_language=item.original_language,
                        original_title=item.original_title,
                        genre_ids=item.genre_ids,
                        vote_average=item.vote_average,
                        overview=item.overview,
                        vote_count=item.vote_count,
                        



                    });

                }
                exampleListView.ItemsSource = Items;
                Random rnd = new Random();
                int index = rnd.Next(0, Items.Count);
                image.Source = Items[index].poster_path;
                title.Text = Items[index].title;
                release_date.Text = Items[index].release_date;



            }
        }

        public class MyListItemEventArgs : EventArgs
        {
            public List<ListViewItem> MyItem { get; set; }
            public MyListItemEventArgs(List<ListViewItem> item)
            {
                this.MyItem = item;
            }
        }

        public async void Delete(Object Sender, EventArgs args)
        {
            ImageButton button = (ImageButton)Sender;
            StackLayout listViewItem = (StackLayout)button.Parent;
            StackLayout listViewItem1 = (StackLayout)listViewItem.Parent;
            Label label = (Label)listViewItem.Children[0];
            Label label2 = (Label)listViewItem.Children[1];
            Image image = (Image)listViewItem1.Children[1];
            string url = image.Source.ToString();
            string text = label.Text;
            string text2 = label2.Text;
            Boolean exists = false;
            List<Movie> movies = await App.Database.GetMovieAsync();
            foreach(var ele in movies)
            {
                if(ele.title.Equals(text) && ele.release_date.Equals(text2)&& ele.poster_path.Equals(url.Substring(4)))
                {
                    exists = true;
                }
            }
            if (!exists)
            {
                foreach(var item in Items)
                {
                    if (item.title.Equals(text)){
                        await App.Database.SaveMovieAsync(new Movie
                        {
                            title = item.title,
                            release_date = item.release_date,
                            poster_path =  item.poster_path,
                  
                            overview = item.overview,




                        });

                        await DisplayAlert("title", "Movie:" + text + " is added to Favorites :)", "ok");
                    }
                }

                
            }
            else
            {
                await DisplayAlert("title", "Movie:" + text + " already added to Favorites :)", "ok");

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
    }
}