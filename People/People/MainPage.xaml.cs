using People.Models;



using System.Collections.Generic;

namespace People
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Evento para agregar una nueva persona
        public void OnNewButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            // Llamada al repositorio para agregar una persona
            App.PersonRepo.AddNewPerson(newPerson.Text);
            statusMessage.Text = App.PersonRepo.StatusMessage;

            // Limpia la entrada
            newPerson.Text = string.Empty;

            // Actualiza la lista de personas
            RefreshPeopleList();
        }

        // Evento para obtener todas las personas
        public void OnGetButtonClicked(object sender, EventArgs args)
        {
            RefreshPeopleList();
        }

        // Evento para eliminar una persona
        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var person = (Person)button.BindingContext;

            // Mostrar alerta para confirmar la eliminación
            bool confirm = await DisplayAlert(
                "Eliminar Registro",
                $"{person.Name} será eliminado. ¿Deseas continuar?",
                "Sí",
                "No");

            if (confirm)
            {
                // Eliminar la persona del repositorio
                App.PersonRepo.DeletePerson(person.Id);

                // Mensaje de confirmación
                await DisplayAlert("Registro Eliminado",
                    $"{person.Name} acaba de ser eliminado.", "OK");

                // Actualiza la lista de personas
                RefreshPeopleList();
            }
        }

        // Método para refrescar la lista de personas
        private void RefreshPeopleList()
        {
            statusMessage.Text = "";
            List<Person> people = App.PersonRepo.GetAllPeople();
            peopleList.ItemsSource = people;
        }
    }
}
