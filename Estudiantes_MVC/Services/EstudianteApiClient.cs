using System.Net.Http.Json;
using Estudiantes_MVC.Models;

namespace Estudiantes_MVC.Services;

public class EstudianteApiClient : IEstudianteApiClient
{
    private const string Endpoint = "api/estudiantes";
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public EstudianteApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _baseUrl = httpClient.BaseAddress?.ToString() ?? "(sin configurar)";
    }

    public async Task<IReadOnlyList<EstudianteViewModel>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(Endpoint);
            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var estudiantes = await response.Content.ReadFromJsonAsync<List<EstudianteViewModel>>();
            return estudiantes ?? [];
        }
        catch (HttpRequestException ex)
        {
            throw CreateApiUnavailableException(ex);
        }
    }

    public async Task<EstudianteViewModel?> GetByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{Endpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EstudianteViewModel>();
        }
        catch (HttpRequestException ex)
        {
            throw CreateApiUnavailableException(ex);
        }
    }

    public async Task<bool> CreateAsync(EstudianteViewModel estudiante)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, estudiante);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            throw CreateApiUnavailableException(ex);
        }
    }

    public async Task<bool> UpdateAsync(EstudianteViewModel estudiante)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{estudiante.Id}", estudiante);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            throw CreateApiUnavailableException(ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            throw CreateApiUnavailableException(ex);
        }
    }

    private ApiUnavailableException CreateApiUnavailableException(HttpRequestException ex)
    {
        return new ApiUnavailableException(
            $"No fue posible conectarse con el API de estudiantes en '{_baseUrl}'. Verifica que el proyecto Estudiantes_API_MVC esté ejecutándose.",
            ex);
    }
}
