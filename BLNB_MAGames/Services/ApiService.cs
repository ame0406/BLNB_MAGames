using SharedParams.DTOs;
using SharedParams.Tables;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient, IConfiguration configuration)
    {
        // Récupérer l'URL de base depuis la configuration
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]);
    }

    #region Condition
    public async Task<List<Condition>> GetAllConditionsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Condition>>("condition");
    }
    public async Task<Condition> AddConditionAsync(Condition condition)
    {
        var response = await _httpClient.PostAsJsonAsync("condition", condition);
        response.EnsureSuccessStatusCode(); // Vérifie si la réponse est un succès
        return await response.Content.ReadFromJsonAsync<Condition>(); // Retourne l'objet ajouté
    }
    #endregion

    #region Types de vente
    public async Task<List<SaleType>> GetAllTypesVenteAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<SaleType>>("saletype");
    }
    public async Task<SaleType> AddTypesVenteAsync(SaleType st)
    {
        var response = await _httpClient.PostAsJsonAsync("saletype", st);
        response.EnsureSuccessStatusCode(); // Vérifie si la réponse est un succès
        return await response.Content.ReadFromJsonAsync<SaleType>(); // Retourne l'objet ajouté
    }
    #endregion

    #region Status
    public async Task<List<Status>> GetAllStatusAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Status>>("status");
    }
    public async Task<Status> AddStatusAsync(Status st)
    {
        var response = await _httpClient.PostAsJsonAsync("status", st);
        response.EnsureSuccessStatusCode(); // Vérifie si la réponse est un succès
        return await response.Content.ReadFromJsonAsync<Status>(); // Retourne l'objet ajouté
    }
	#endregion

	#region Marques
	public async Task<List<Marques>> GetAllMarquesAsync()
	{
		return await _httpClient.GetFromJsonAsync<List<Marques>>("marque");
	}
	public async Task<Marques> AddMarqueAsync(Marques marque)
	{
		var response = await _httpClient.PostAsJsonAsync("marque", marque);
		response.EnsureSuccessStatusCode(); // Vérifie si la réponse est un succès
		return await response.Content.ReadFromJsonAsync<Marques>(); // Retourne l'objet ajouté
	}
	#endregion

	#region DropDeepSearch
	public async Task<List<GenericObjDTO>> GetObjectsFilteredAsync(DropDeepSearchDTO searchDTO)
	{
        var response = await _httpClient.PostAsJsonAsync("dropdeepsearch", searchDTO);
        response.EnsureSuccessStatusCode(); // Vérifie si la réponse est un succès
        return await response.Content.ReadFromJsonAsync<List<GenericObjDTO>>(); // Retourne l'objet ajouté

	}
    #endregion#

    #region Base_Obj
    public async Task<Base_Obj?> GetGameByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Base_Obj>($"base_obj/{id}");
        }
        catch (HttpRequestException ex)
        {
            // Handle the exception and log it if necessary
            Console.WriteLine($"Request error: {ex.Message}");
            return null;
        }
    }
	#endregion

	#region Stocks
	public async Task<List<Stocks>> GetAllInStocksAsync()
	{
		try
		{
			var response = await _httpClient.GetFromJsonAsync<List<Stocks>>("stocks/GetAllInStocks");

			return response ?? new List<Stocks>();
		}
		catch (Exception ex)
		{
			return new List<Stocks>();
		}
	}
	public async Task<bool> AddStockAsync(Stocks stock)
	{
		try
		{
			var response = await _httpClient.PostAsJsonAsync("stocks/AddStock", stock);

			return response.IsSuccessStatusCode;
		}
		catch (Exception ex)
		{
			return false;
		}
	}
	#endregion

	#region Lots
	public async Task<Lots> AddLotAsync(Lots lot)
	{
		var response = await _httpClient.PostAsJsonAsync("lots/AddLot", lot);
		response.EnsureSuccessStatusCode(); 
		return await response.Content.ReadFromJsonAsync<Lots>();
	}
	#endregion
}

