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
	public partial class Favorite : ContentPage
	{
		public Favorite ()
		{
			InitializeComponent ();
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            exampleListView.ItemsSource = await App.Database.GetMovieAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            StackLayout stack = (StackLayout)button.Parent;
            Label label = (Label)stack.Children[0];
            string title = label.Text;
            List<Movie> movies = await App.Database.GetMovieAsync();
            foreach(var element in movies)
            {
                if (element.title == title)
                {
                    bool answer = await DisplayAlert("Question?", "Would you like to delete from favorites", "Yes", "No");
                    if (answer)
                    {
                        App.Database.DeleteMovieAsync(element);
                        break;
                    }
                    else
                    {
                        break;
                    }
                  

                }
            }
            exampleListView.ItemsSource = await App.Database.GetMovieAsync();

                
        

    }

        private void ExampleListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Movie;
    
            App.Current.MainPage = new NavigationPage(new DetailPage(item));
        }
    }
}