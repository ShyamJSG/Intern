using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApiMVC.Models;

public class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Crud>> GetAllEmployeesAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7142/api/Crud/GetAllEmployees");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Response>();
        return result.cruds;
    }

    public async Task<Crud> GetEmployeeByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7142/api/Crud/GetEmployeeById/{id}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Response>();
        return result.crud;
    }

    public async Task<HttpResponseMessage> AddEmployeeAsync(Crud crud)
    {
        return await _httpClient.PostAsJsonAsync("https://localhost:7142/api/Crud/AddEmployee", crud);
    }

    public async Task<HttpResponseMessage> UpdateEmployeeAsync(Crud crud)
    {
        return await _httpClient.PostAsJsonAsync("https://localhost:7142/api/Crud/UpdateEmployee", crud);
    }

    public async Task<HttpResponseMessage> DeleteEmployeeAsync(int id)
    {
        return await _httpClient.PostAsync($"https://localhost:7142/api/Crud/DeleteEmployee/{id}", null);
    }
}
