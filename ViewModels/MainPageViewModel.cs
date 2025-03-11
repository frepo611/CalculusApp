using CalculusApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculusApp.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    public int MyProperty { get; set; }

    [ObservableProperty] 
    private string _solution = "";

    private readonly SolutionService _solutionService;

    public MainPageViewModel(SolutionService solutionService)
    {
        _solutionService = solutionService;
    }

    [RelayCommand]
    private async Task NewtonClicked()
    {
        Solution = await GetSolutionAsync();
    }

    public async Task<string> GetSolutionAsync()
    {
        return await _solutionService.GetSolutionAsync("derive","x^2");
    }
}
