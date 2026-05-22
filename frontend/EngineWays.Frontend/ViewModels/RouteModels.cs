using CommunityToolkit.Mvvm.ComponentModel;

namespace EngineWays.Frontend.ViewModels;

public partial class StopPointViewModel : ObservableObject
{
    [ObservableProperty]
    private string? name;
}

public partial class ModeOptionViewModel : ObservableObject
{
    public ModeOptionViewModel(string name, string icon)
    {
        Name = name;
        Icon = icon;
    }

    public string Name { get; }
    public string Icon { get; }

    [ObservableProperty]
    private bool isSelected;
}

public partial class RouteStepViewModel : ObservableObject
{
    public RouteStepViewModel(string title, string description, string color)
    {
        Title = title;
        Description = description;
        Color = color;
    }

    public string Title { get; }
    public string Description { get; }
    public string Color { get; }
}
