using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_PROJECT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoviesPage : ContentPage
	{
		public MoviesPage ()
		{
			InitializeComponent ();
		}
		public MoviesPage(int type)
		{
			InitializeComponent ();
			if (type == 0) // movie
			{

			}
			if (type == 1) // serial
			{

			}
			if (type == 2) // anime
			{

			}
		}
	}
}