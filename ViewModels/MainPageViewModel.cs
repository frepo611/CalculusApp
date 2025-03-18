using CalculusApp.Services;
using CalculusApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CalculusApp.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _solution = "";
    public string? FirstExtraParameter { get; set; }
    public string? SecondExtraParameter { get; set; }
    public string? Expression { get; set; }

    private readonly SolutionService _solutionService;
    private readonly DatabaseService _databaseService;

    public ObservableCollection<Operation> Operations { get; } = new ObservableCollection<Operation>(Models.Operations.GetAllOperations());

    public ObservableCollection<ExpressionHistory> ExpressionHistory { get; } = new ObservableCollection<ExpressionHistory>();

    [ObservableProperty]
    private bool _isSecondExtraParameterVisible;

    [ObservableProperty]
    private bool _isFirstExtraParameterVisible;

    [ObservableProperty]
    private Operation _selectedOperation;

    [ObservableProperty]
    private string _latexExpression;

    [ObservableProperty]
    private ExpressionHistory _selectedHistoryItem;

    partial void OnSelectedHistoryItemChanged(ExpressionHistory value)
    {
        if (value != null)
        {
            SelectedOperation = Operations.FirstOrDefault(op => op.Name == value.OperationName);
            Expression = value.Expression;
            Solution = value.Solution;
            FirstExtraParameter = value.FirstExtraParameter;
            SecondExtraParameter = value.SecondExtraParameter;
        }
    }

    public MainPageViewModel(SolutionService solutionService, DatabaseService databaseService)
    {
        _solutionService = solutionService;
        _databaseService = databaseService;
        _selectedOperation = Operations.First(); // Default to the first operation
        LatexExpression = @"\sum_{i=1}^{n} i = \frac{n(n+1)}{2}";
        LoadExpressionHistory();
    }

    partial void OnSelectedOperationChanged(Operation value)
    {
        UpdateFirstExtraFieldVisible();
        UpdateSecondExtraFieldVisible();
    }

    private void UpdateFirstExtraFieldVisible()
    {
        IsFirstExtraParameterVisible = SelectedOperation.Name == "Find Tangent" || SelectedOperation.Name == "Area Under Curve";
    }

    private void UpdateSecondExtraFieldVisible()
    {
        IsSecondExtraParameterVisible = SelectedOperation.Name == "Area Under Curve";
    }

    [RelayCommand]
    private async Task NewtonClicked()
    {
        var calculusTaskFactory = new CalculusTaskFactory();
        ICalculusTask calculusTask = calculusTaskFactory.CreateCalculusTask(SelectedOperation, Expression, FirstExtraParameter, SecondExtraParameter);
        Solution = await GetSolutionAsync(calculusTask);
        
        var history = new ExpressionHistory
        {
            Timestamp = DateTime.Now,
            OperationName = SelectedOperation.Name,
            Expression = Expression,
            FirstExtraParameter = FirstExtraParameter,
            SecondExtraParameter = SecondExtraParameter,
            Solution = Solution
        };

        await _databaseService.AddExpressionHistoryAsync(history);
        ExpressionHistory.Add(history);
        SelectedHistoryItem = history;
    }

    private async void LoadExpressionHistory()
    {
        var histories = await _databaseService.GetExpressionHistoryAsync();
        foreach (var history in histories)
        {
            ExpressionHistory.Add(history);
        }
    }

    public async Task<string> GetSolutionAsync(ICalculusTask calculusTask)
    {
        return await _solutionService.GetSolutionAsync(calculusTask);
    }
}
