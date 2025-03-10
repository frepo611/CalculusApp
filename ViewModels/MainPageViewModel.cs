using CalculusApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculusApp.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    public int MyProperty { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private string _solution = "";
    public string Solution
    {
        get => _solution;
        set
        {
            _solution = value;
            OnPropertyChanged();
        }
    }

    private readonly SolutionService _solutionService;

    public MainPageViewModel(SolutionService solutionService)
    {
        _solutionService = solutionService;
    }

    public async Task<string> GetSolutionAsync()
    {
        return await _solutionService.GetSolutionAsync("derive","x^2");
    }
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
