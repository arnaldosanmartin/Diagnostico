using Firebase.Xamarin.Database;
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
        public IList<String> histo = new ObservableCollection<String>();
        FirebaseClient firebase;
        String x;
        String nombre;
        public diagn (String name)
		{
            
            //histo = his;
            nombre = name;
            BindingContext = histo;
            Title = "Diagnosticos de "+ name;
			InitializeComponent ();

            firebase = new FirebaseClient("https://histopacient.firebaseio.com/");

            getList();
        }

        public async Task<int> getList()
        {
            var list = (await firebase.Child("diag"+nombre).OnceAsync<String>());

            histo.Clear();

            foreach (var item in list)
            {
                histo.Add(item.Object);
            }

            return 0;
        }

        public async void agrega(object sender, System.EventArgs e)
        {

            EnteredName.Text = string.Empty;
            overlay1.IsVisible = true;
            
        }

        public async void elimina(object sender, System.EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            String data = item.CommandParameter as String;

            //histo.Remove(data);
        }

        public async void edita(object sender, System.EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            String data = item.CommandParameter as String;
            x = data;
            EnteredName.Text = string.Empty;
            overlay.IsVisible = true;
        }

        public async void OnOKButtonClicked1(object sender, EventArgs args)
        {
            overlay1.IsVisible = false;
            //histo.Add(EnteredName1.Text);
            await firebase.Child("diag" + nombre).PostAsync(EnteredName1.Text);

            await getList();
        }

        public async void OnOKButtonClicked(object sender, EventArgs args)
        {
            var item = (String)BindingContext;
            overlay.IsVisible = false;
            //histo.Insert(histo.IndexOf(x), EnteredName.Text);
            //histo.RemoveAt(histo.IndexOf(x)+1);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await getList();
        }
    }
}