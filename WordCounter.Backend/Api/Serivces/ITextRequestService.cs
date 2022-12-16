namespace Api.Serivces;

public interface ITextRequestService
{
    Task<string> GetTextAsync(string uri);
}