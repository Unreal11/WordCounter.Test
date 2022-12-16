namespace Api.Serivces;

public class TextRequestService : ITextRequestService
{
    public async Task<string> GetTextAsync(string uri)
    {
        uri = uri.Trim();

        if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
        {
            throw new UriFormatException();
        }
        
        var client = new HttpClient();

        client.BaseAddress = new Uri(uri);

        return await client.GetStringAsync(string.Empty);
    }
}