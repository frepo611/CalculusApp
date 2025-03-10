using CalculusApp.ViewModels;

namespace CalculusApp;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _mainPageViewModel;

    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        _mainPageViewModel = mainPageViewModel;
        BindingContext = _mainPageViewModel;
    }

    private void OnNewtonClicked(object sender, EventArgs e)
    {
        
    }
}
