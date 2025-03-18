using System.Globalization;

namespace CalculusApp.Converters;

public class LatexToHtmlConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string latexExpression)
        {
            return new HtmlWebViewSource
            {
                Html = $@"
                    <html>
                    <head>
                        <script type='text/javascript' async src='https://polyfill.io/v3/polyfill.min.js?features=es6'></script>
                        <script type='text/javascript' async src='https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js'></script>
                    </head>
                    <body>
                        <p>\({latexExpression}\)</p>
                        <script>
                            MathJax.typeset();
                        </script>
                    </body>
                    </html>"
            };
        }
            return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}
