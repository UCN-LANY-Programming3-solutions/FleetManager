using FleetManager.Desktop.Presenter;
using FleetManager.Desktop.Model;
using System.Windows;
using FleetManager.Desktop.View;

namespace FleetManager.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CarPresenter _carPresenter;

        private CarModel _selectedCar;

        public MainWindow(CarPresenter carPresenter)
        {
            InitializeComponent();
            _carPresenter = carPresenter;
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            // Open dialog in new state
            EditCarModel model = new(_carPresenter.GetAllLocations());
            EditCar dialog = new(model);
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _carPresenter.SaveCar(model.Car);
            }
            Update();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            _selectedCar = (CarModel)carsListView.SelectedItem;

            // Open dialog in update state
            EditCar dialog = new(new EditCarModel(_carPresenter.GetAllLocations(), _selectedCar))
            {
                DataContext = _selectedCar
            };
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _carPresenter.SaveCar(_selectedCar);
            }
            Update();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _selectedCar = (CarModel)carsListView.SelectedItem;
            _carPresenter.DeleteCar(_selectedCar);
            Update();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            _selectedCar = null;
            carsListView.ItemsSource = _carPresenter.GetAllCars();
        }
    }
}
