using System.Net.Http;
using System.Net.Http.Json;

public class CartService(HttpClient http, Blazored.LocalStorage.ILocalStorageService localStorage)
{

    public Cart cart = new Cart();
    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public void AddToCart(CartItem newItem)
    {
        cart.AddToCart(newItem);
        NotifyStateChanged();
    }
    public void UpdateCart(int id, int quantity)
    {
        cart.UpdateItem(id, quantity);
        NotifyStateChanged();
    }
    public void DeleteItem(int deleteId)
    {
        cart.DeleteItem(deleteId);
        NotifyStateChanged();
    }
    public void UpdateCartButton(int id, int quantity)
    {
        cart.UpdateItemCartButton(id, quantity);
        NotifyStateChanged();
    }
    //blazor-service-post
    public async Task<T?> PostAsync<T>(string url, object data, string? token = null)
    {
        token = await localStorage.GetItemAsync<string>("accessToken");
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(data)
            };
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"POST failed: {ex.Message}");
            return default;
        }
    }
    // Place your HTTP methods below
}