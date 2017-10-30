using Firebase.Xamarin.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace diagnosticos
{
	public partial class MainPage : ContentPage
	{

        IList<paciente> pacientes = new ObservableCollection<paciente>();
        IList<String> pru = new ObservableCollection<String>();
        FirebaseClient firebase;

        public MainPage()
		{
            Title = "Pacientes";
            BindingContext = pacientes;
			InitializeComponent();

            firebase = new FirebaseClient("https://histopacient.firebaseio.com/");

            //pacientes.Add(new paciente("Aza", pru));

            getList();
        }

        public async Task<int> getList()
        {
            var list = (await firebase.Child("yourentity").OnceAsync<paciente>());

            pacientes.Clear();

            foreach (var item in list)
            {
                paciente c = item.Object as paciente;
                c.id = item.Key;
                pacientes.Add(c);
            }

            return 0;
        }


        public async void selection(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem== null) { return; }
            paciente p = e.SelectedItem as paciente;
            //Navigation.PushAsync(new diagn(p.his));
            Navigation.PushAsync(new diagn(p.name));

            //DisplayAlert("Test", "Selected " + p.name, "ok");
            ((ListView)sender).SelectedItem = null;
        }

        void agrega(object sender, System.EventArgs e)
        {
           

            EnteredName.Text = string.Empty;
            overlay.IsVisible = true;
            EnteredName.Focus();

            
        }

        public async void OnOKButtonClicked(object sender, EventArgs args)
        {
            IList<String> nuevo = new ObservableCollection<String>();
            overlay.IsVisible = false;
            paciente p = new paciente(EnteredName.Text, nuevo);
            //pacientes.Add(p);

            await firebase.Child("yourentity").PostAsync(p);
            await firebase.Child("diag"+ EnteredName.Text).PostAsync("OK");
            
            await getList();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await getList();
        }
    }
}
