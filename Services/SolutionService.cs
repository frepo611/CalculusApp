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

    public async Task<string> GetSolutionAsync(Models.CalculusTask calculusTask)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var baseUri = new Uri("https://newton.now.sh");
            httpClient.BaseAddress = baseUri;
            var parameterUri = Uri.EscapeDataString(calculusTask.Parameters);
            var response = await httpClient.GetAsync($"/api/v2/{calculusTask.Operation}/{parameterUri}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API request failed with status code: {response.StatusCode}");
            }

            Solution responseSolution = await response.Content.ReadFromJsonAsync<Solution>() ?? throw new Exception("No solution returned from the API.");

            return responseSolution.Result;
        }
        catch (HttpRequestException httpRequestException)
        {
            // Handle HTTP request specific exceptions
            throw new Exception("Error occurred while making the HTTP request.", httpRequestException);
        }
        catch (Exception)
        {
            // Handle all other exceptions
            throw;
        }
    }
}