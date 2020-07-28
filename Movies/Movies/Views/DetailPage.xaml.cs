using Movies.MasterDetailsPage;
using Movies.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        Movie Item;
        public DetailPage(Movie item)
        {
            Item = item;
            InitializeComponent();
            image.Source = item.poster_path;
            title.Text = item.title;
            overview.Text = item.overview;
            release_date.Text = item.release_date;
        }
        public async void Delete(Object Sender, EventArgs args)
        {

            Boolean exists = false;
            List<Movie> movies = await App.Database.GetMovieAsync();
            foreach (var ele in movies)
            {
                if (ele.title.Equals(Item.title) && ele.release_date.Equals(Item.release_date) && ele.poster_path.Equals(Item.poster_path))
                {
                    exists = true;
                }
            }
            if (!exists)
            {
              
                        await App.Database.SaveMovieAsync(new Movie
                        {
                            title = Item.title,
                            release_date = Item.release_date,
                            poster_path = Item.poster_path,
                     
                            overview = Item.overview,



                        });

                        await DisplayAlert("title", "Movie:" + Item.title + " is added to Favorites :)", "ok");
                    
                


            }
            else
            {
                await DisplayAlert("title", "Movie:" + Item.title + " already added to Favorites :)", "ok");

            }


        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MasterDetailPageNavigation();
        }
    }
}