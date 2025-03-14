using CalculusApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace CalculusApp.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    public int MyProperty { get; set; }

    [ObservableProperty]
    private string _solution = "";
    public string? FirstExtraField { get; set;}
    public string? SecondExtraField { get; set; }
    public string? Expression { get; set; }

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
    private bool _isSecondExtraFieldVisible;

    // The LaTeX expression bound to the UI
    [ObservableProperty]
    private string latexExpression;

    public MainPageViewModel(SolutionService solutionService)
    {
        _solutionService = solutionService;
        _selectedOperation = Operations.First(); // Default to the first operation
        LatexExpression = @"\sum_{i=1}^{n} i = \frac{n(n+1)}{2}";
    }

    // Generates the WebView HTML content dynamically
    public string GetHtmlContent()
    {
        return $@"
            <html>
            <head>
                <script type='text/javascript' async src='https://polyfill.io/v3/polyfill.min.js?features=es6'></script>
                <script type='text/javascript' async src='https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js'></script>
            </head>
            <body>
                <h2>Math Expression:</h2>
                <p>\({LatexExpression}\)</p>
                <script>
                    MathJax.typeset();
                </script>
            </body>
            </html>";
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

    [RelayCommand]
    private async Task NewtonClicked()
    {
        Solution = await GetSolutionAsync();
    }

    public async Task<string> GetSolutionAsync()
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
                return $"{FirstExtraField}:{SecondExtraField}|{expression}";
            default:
                // Return the original expression for other operations
                return expression;
        }
    }
}
