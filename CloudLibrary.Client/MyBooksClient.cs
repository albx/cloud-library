using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace CloudLibrary.Client;

public class MyBooksClient
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public MyBooksClient(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));
    }

    public async Task AddBookToMyListAsync(int bookId)
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var userId = authenticationState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var response = await _httpClient.PostAsJsonAsync("data-api/rest/AddBookToMyList", new { bookId, userId });
        response.EnsureSuccessStatusCode();
    }
}
