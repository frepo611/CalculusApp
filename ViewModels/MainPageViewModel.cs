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

    [ObservableProperty]
    public string? _firstExtraParameter;

    [ObservableProperty]
    public string? _secondExtraParameter;

    [ObservableProperty]
    public string? _expression;

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
            UpdateLatexExpression();
        }
    }

    public MainPageViewModel(SolutionService solutionService, DatabaseService databaseService)
    {
        _solutionService = solutionService;
        _databaseService = databaseService;
        _selectedOperation = Operations.First(); // Default to the first operation
        LatexExpression = "";
        LoadExpressionHistory();
    }

    partial void OnSelectedOperationChanged(Operation value)
    {
        UpdateFirstExtraFieldVisible();
        UpdateSecondExtraFieldVisible();
        Solution = ""; // Clear the solution when a new operation is selected
        UpdateLatexExpression();
    }

    private void UpdateFirstExtraFieldVisible()
    {
        IsFirstExtraParameterVisible = SelectedOperation.Name == "Find Tangent" || SelectedOperation.Name == "Area Under Curve";
    }

    private void UpdateSecondExtraFieldVisible()
    {
        IsSecondExtraParameterVisible = SelectedOperation.Name == "Area Under Curve";
    }

    private void UpdateLatexExpression()
    {
        if (SelectedOperation != null && !string.IsNullOrEmpty(Expression))
        {
            LatexExpression = TransformToLatex(Expression, Solution, SelectedOperation.Name);
        }
    }

    private string TransformToLatex(string expression, string result, string operation)
    {
        string transformedExpression = expression.Replace("(", "{").Replace(")", "}");
        string transformedResult = TransformResultToLatex(result);
        return operation.ToLower() switch
        {
            "derive" => $@"\frac{{d}}{{dx}}f({transformedExpression}) = {transformedResult}",
            "integrate" => $@"\int {transformedExpression} \, dx = {transformedResult}",
            "area under curve" => $@"\int_{{{FirstExtraParameter}}}^{{{SecondExtraParameter}}} {transformedExpression} \, dx = {transformedResult}",
            "factor" => $@"\text{{Factor}}({transformedExpression}) = {transformedResult}",
            "simplify" => $@"\text{{Simplify}}({transformedExpression}) = {transformedResult}",
            _ => transformedExpression // Default case, return the transformed expression
        };
    }

    private string TransformResultToLatex(string result)
    {
        // Replace "/" with "\frac{}{}" syntax
        var parts = result.Split('/');
        if (parts.Length == 2)
        {
            return $@"\frac{{{parts[0]}}}{{{parts[1]}}}";
        }
        return result;
    }

    [RelayCommand]
    private async Task NewtonClicked()
    {
        try
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
            UpdateLatexExpression(); // Update LaTeX expression with the result
        }
        catch (Exception ex)
        {
            await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            ResetState();
        }
    }

    private void ResetState()
    {
        Solution = "";
        Expression = "";
        FirstExtraParameter = "";
        SecondExtraParameter = "";
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
