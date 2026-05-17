using Avalonia.Controls;
using Mapsui;
using Mapsui.Tiling;
using Mapsui.Extensions;

namespace EngineWays.Frontend.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeMap();
    }

    private void InitializeMap()
    {
        MapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
        
        // Center on Mexico City
        var centerOfCdmx = new MPoint(-99.1332, 19.4326);
        var pos = Mapsui.Projections.SphericalMercator.FromLonLat(centerOfCdmx.X, centerOfCdmx.Y);
        var sphericalMercatorCoordinate = new MPoint(pos.x, pos.y);
        
        // Use the navigator to set the initial view
        MapControl.Map.Navigator.CenterOn(sphericalMercatorCoordinate);
        MapControl.Map.Navigator.ZoomToLevel(10);
    }
}
