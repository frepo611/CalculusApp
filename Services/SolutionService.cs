using CalculusApp.Models;
using System.Net.Http.Json;

namespace CalculusApp.Services;
public class SolutionService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public SolutionService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetSolutionAsync(string operation, string expression)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var baseUri = new Uri("https://newton.now.sh");
        httpClient.BaseAddress = baseUri;
        var expressionUri = Uri.EscapeDataString(expression);
        Solution responseSolution = await httpClient.GetFromJsonAsync<Solution>($"/api/v2/{operation}/{expressionUri}") ?? throw new Exception();
        return responseSolution.Result;
    }

}