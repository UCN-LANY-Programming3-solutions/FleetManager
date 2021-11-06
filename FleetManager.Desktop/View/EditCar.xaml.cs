using FleetManager.Desktop.Model;
using System.Windows;

namespace FleetManager.Desktop.View
{
    /// <summary>
    /// Interaction logic for EditCar.xaml
    /// </summary>
    public partial class EditCar : Window
    {
        private readonly EditCarModel _model;

        public EditCar(EditCarModel model)
        {
            InitializeComponent();
            _model = model;
        }

        private void Update()
        {
            locationsComboBox.ItemsSource = _model.Locations;

            foreach (object item in locationsComboBox.Items)
            {
                if (((LocationModel)item).Name == _model.Location?.Name)
                {
                    locationsComboBox.SelectedItem = item;
                    break;
                }
            }
            brandTextBox.Text = _model.Brand;
            mileageTextBox.Text = _model.Mileage.ToString();
            btnOk.IsEnabled = true;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            _model.Brand = brandTextBox.Text;
            _model.Mileage = int.Parse(mileageTextBox.Text);
            _model.Location = (LocationModel)locationsComboBox.SelectedItem;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Dialog_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
