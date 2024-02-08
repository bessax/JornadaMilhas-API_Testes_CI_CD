using JornadaMilhas.API.DTO.Auth;
using JornadaMilhas.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace JornadaMilhas.Integration.Test.API;

public class JornadaMilhas_RotaTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public JornadaMilhas_RotaTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact]
    public async Task GET_Retorna_Todas_Rotas()
    {
        //Arrange      

        using var client = _factory.CreateClient();

        var token = await GetAccessTokenAsync(client);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var resultado = await client.GetAsync("/rota-viagem");
        var response = await client.GetFromJsonAsync<ICollection<Rota>>("/rota-viagem");

        Assert.True(response != null);
        Assert.Equal(HttpStatusCode.OK,resultado.StatusCode);

       
    }

    private async Task<string> GetAccessTokenAsync(HttpClient client)
    {     

        var user = new UserDTO { Email = "tester@email.com", Password = "Senha123@" };

        var response = await client.PostAsJsonAsync("/auth-login", user);

        response.EnsureSuccessStatusCode();

         var result = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var token = JsonSerializer.Deserialize<UserTokenDTO>(result, options);

        return token!.Token;
    }
}