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
		// Envoie une requête POST avec l'objet de recherche (searchDTO) dans le corps de la requête
		var response = await _httpClient.PostAsJsonAsync("dropdeepsearch", searchDTO);

		// Vérifie si la requête a réussi
		response.EnsureSuccessStatusCode();

		// Retourne la liste des objets filtrés
		return await response.Content.ReadFromJsonAsync<List<GenericObjDTO>>();
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
}

