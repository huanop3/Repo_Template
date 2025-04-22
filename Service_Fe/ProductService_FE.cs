using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using web_api_base.Models.dbebay;
using web_api_base.Service_FE.ViewModel;

public class ProductStateService(HttpClient http)
{
    public IEnumerable<ListingVM> productListing = new List<ListingVM>();

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public async Task getProductListing()
    {
        //Gọi api cập nhật state
        //blazor-state-get
      
        var result = await http.GetFromJsonAsync<HTTPResponseClient<IEnumerable<ListingVM>>>("http://localhost:5208/api/Product/GetAllProductListing");
        productListing = result.Data;

        // Console.WriteLine($"{JsonSerializer.Serialize(result.Data)}");

        NotifyStateChanged();
    }

    // Place your HTTP methods below
}