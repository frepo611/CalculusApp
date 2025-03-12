using CalculusApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculusApp.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    public int MyProperty { get; set; }

    [ObservableProperty]
    private string _solution = "The answer";

    public string Expression { get; set; }

    private readonly SolutionService _solutionService;

    public MainPageViewModel(SolutionService solutionService)
    {
        _solutionService = solutionService;
    }

    [ObservableProperty]
    private bool _isDeriveChecked = true;

    [ObservableProperty]
    private bool _isIntegrateChecked;

    [ObservableProperty]
    private bool _isFactorChecked;

    [ObservableProperty]
    private bool _isSimplifyChecked;

    [RelayCommand]
    private async Task NewtonClicked()
    {
        Solution = await GetSolutionAsync();
    }

    public async Task<string> GetSolutionAsync()
    {
        return await _solutionService.GetSolutionAsync("derive", Expression);
    }
}
