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

        public MainPage()
		{
            Title = "Pacientes";
            BindingContext = pacientes;
			InitializeComponent();
            

            pacientes.Add(new paciente("Aza", pru));

        }

        void selection(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem== null) { return; }
            paciente p = e.SelectedItem as paciente;
            Navigation.PushAsync(new diagn(p.his));
           
            //DisplayAlert("Test", "Selected " + p.name, "ok");
            ((ListView)sender).SelectedItem = null;
        }

        void agrega(object sender, System.EventArgs e)
        {
           

            EnteredName.Text = string.Empty;
            overlay.IsVisible = true;
            EnteredName.Focus();

            
        }

        void OnOKButtonClicked(object sender, EventArgs args)
        {
            IList<String> nuevo = new ObservableCollection<String>();
            overlay.IsVisible = false;
            pacientes.Add(new paciente(EnteredName.Text, nuevo));
        }

    }
}
