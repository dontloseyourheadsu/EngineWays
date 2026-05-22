using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EngineWays.Frontend.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string origin = "Current Location";

    [ObservableProperty]
    private string destination = "Palacio de Bellas Artes";

    [ObservableProperty]
    private int totalMinutes = 35;

    [ObservableProperty]
    private double totalDistanceKm = 4.2;

    public ObservableCollection<StopPointViewModel> Stops { get; } = new();
    public ObservableCollection<ModeOptionViewModel> Modes { get; } = new();
    public ObservableCollection<RouteStepViewModel> Steps { get; } = new();

    public MainWindowViewModel()
    {
        Modes.Add(new ModeOptionViewModel("Metro", "W+M"));
        Modes.Add(new ModeOptionViewModel("Walk", "W"));
        Modes.Add(new ModeOptionViewModel("Bike", "B"));
        SelectMode(Modes[0]);

        Steps.Add(new RouteStepViewModel("Walk 5 min", "To Metro Insurgentes", "#9E9E9E"));
        Steps.Add(new RouteStepViewModel("Metro Line 1", "Direction Pantitlan - 4 stops", "#FF5722"));
        Steps.Add(new RouteStepViewModel("Walk 8 min", "Arrive at Palacio de Bellas Artes", "#9E9E9E"));
    }

    [RelayCommand]
    private void AddStop()
    {
        Stops.Add(new StopPointViewModel());
    }

    [RelayCommand]
    private void RemoveStop(StopPointViewModel stop)
    {
        Stops.Remove(stop);
    }

    [RelayCommand]
    private void SelectMode(ModeOptionViewModel mode)
    {
        foreach (var option in Modes)
        {
            option.IsSelected = option == mode;
        }
    }
}
