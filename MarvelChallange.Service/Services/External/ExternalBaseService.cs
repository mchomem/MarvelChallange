namespace MarvelChallange.Service.Services.External
{
    public class ExternalBaseService
    {
        public ExternalBaseService() { }

        public async Task<string> Get(string url)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
