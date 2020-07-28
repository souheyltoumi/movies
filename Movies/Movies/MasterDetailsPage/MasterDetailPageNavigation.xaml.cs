using Movies.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.MasterDetailsPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailPageNavigation : IMasterDetailPageController
    {
		public MasterDetailPageNavigation ()
		{
			InitializeComponent ();
            IsPresented = false;

            Detail = new NavigationPage(new HomePage());

        }

        private void Home(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        private void Favorite(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Favorite());
            IsPresented = false;

        }

        private void Search(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Search());
            IsPresented = false;

        }
    }
}