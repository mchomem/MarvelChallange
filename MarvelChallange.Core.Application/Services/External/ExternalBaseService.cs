namespace MarvelChallange.Core.Application.Services.External;

public class ExternalBaseService
{
    public ExternalBaseService() { }

    public async Task<string> SendRequest(string url)
    {
        HttpClient httpClient = new();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}
