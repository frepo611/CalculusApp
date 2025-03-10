namespace CalculusApp.ViewModels;

public class MainPageViewModel
{
    public int MyProperty { get; set; }

    private readonly IHttpClientFactory _httpClientFactory;
    public MainPageViewModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
}
