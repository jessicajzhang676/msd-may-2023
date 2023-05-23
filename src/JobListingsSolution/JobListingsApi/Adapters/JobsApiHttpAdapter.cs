namespace JobListingsApi.Adapters;

//"typed 
public class JobsApiHttpAdapter
{
    private readonly HttpClient _httpClient;

    public JobsApiHttpAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> JobExistsAsync(string jobId)
    {
        var uribuilder = new UriBuilder(_httpClient.BaseAddress);
        uribuilder.Path = $"/Jobs/{jobId}";
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Head,
            RequestUri = uribuilder.Uri
        };
        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        else
        {
            return true;
        }
        // this looks dumb, but I'll explain more in a bit.
    }
}