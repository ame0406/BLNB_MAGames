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
}
