using Microsoft.JSInterop;

public class ProfileStateService
{
    private readonly IJSRuntime _js;
    public string Profile { get; private set; } = "";
    public event Action? OnProfileChanged;

    public ProfileStateService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitializeAsync()
    {
        Profile = await _js.InvokeAsync<string>("localStorageHelper.getProfile") ?? "";
        OnProfileChanged?.Invoke();
    }

    public async Task SetProfile(string profile)
    {
        Profile = profile;
        await _js.InvokeVoidAsync("localStorage.setItem", "profile", profile);
        OnProfileChanged?.Invoke();
    }

    public async Task ClearProfile()
    {
        Profile = "";
        await _js.InvokeVoidAsync("localStorage.removeItem", "profile");
        OnProfileChanged?.Invoke();
    }
}
