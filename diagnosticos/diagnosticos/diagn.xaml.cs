using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diagnosticos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class diagn : ContentPage
	{
        public IList<String> histo;
        String x;
        public diagn (IList<String> his)
		{
            histo = his;
            BindingContext = histo;
            Title = "Diagnosticos";
			InitializeComponent ();
		}

        void agrega(object sender, System.EventArgs e)
        {

            EnteredName.Text = string.Empty;
            overlay1.IsVisible = true;
            
        }

        void elimina(object sender, System.EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            String data = item.CommandParameter as String;

            histo.Remove(data);
        }

        void edita(object sender, System.EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            String data = item.CommandParameter as String;
            x = data;
            EnteredName.Text = string.Empty;
            overlay.IsVisible = true;
        }

        void OnOKButtonClicked1(object sender, EventArgs args)
        {
            overlay1.IsVisible = false;
            histo.Add(EnteredName1.Text);
        }

        void OnOKButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
            histo.Insert(histo.IndexOf(x), EnteredName.Text);
            histo.RemoveAt(histo.IndexOf(x)+1);
        }
    }
}