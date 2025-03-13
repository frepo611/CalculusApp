using CalculusApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CalculusApp.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _solution = "";
    public string? FirstExtraField { get; set;}
    public string? SecondExtraField { get; set; }
    public string? Expression { get; set; }

    private readonly SolutionService _solutionService;

    public ObservableCollection<Operation> Operations { get; } = new ObservableCollection<Operation>(CalculusApp.Operations.GetAllOperations());

    [ObservableProperty]
    private Operation _selectedOperation;

    [ObservableProperty]
    private bool _isFirstExtraFieldVisible;

    [ObservableProperty]
    private bool _isSecondExtraFieldVisible;

    public MainPageViewModel(SolutionService solutionService)
    {
        _solutionService = solutionService;
        _selectedOperation = Operations.First(); // Default to the first operation
    }

    partial void OnSelectedOperationChanged(Operation value)
    {
        UpdateFirstExtraFieldVisible();
        UpdateSecondExtraFieldVisible();

    }

    private void UpdateSecondExtraFieldVisible()
    {
        IsSecondExtraFieldVisible = SelectedOperation.Name == "Area Under Curve";
    }

    private void UpdateFirstExtraFieldVisible()
    {
        IsFirstExtraFieldVisible = SelectedOperation.Name == "Find Tangent" || SelectedOperation.Name == "Area Under Curve";
    }

    [RelayCommand]
    private async Task NewtonClicked()
    {
        Solution = await GetSolutionAsync(SelectedOperation.Endpoint);
    }

    public async Task<string> GetSolutionAsync(string operation)
    {
        string modifiedExpression = RebuildExpression(operation, Expression);
        return await _solutionService.GetSolutionAsync(operation, modifiedExpression);
    }

    private string RebuildExpression(string operation, string expression)
    {
        switch (operation)
        {
            case "tangent":
                // Modify the expression for Find Tangent operation
                return $"{FirstExtraField}|{expression}";
            case "area":
                // Modify the expression for Area Under Curve operation
                return $"area({expression})";
            default:
                // Return the original expression for other operations
                return expression;
        }
    }
}
