using System.Net.Http.Json;
using System.Net;
using JornadaMilhas.API.DTO.Auth;
using Microsoft.AspNetCore.Mvc.Testing;

namespace JornadaMilhas.Integration.Test.API;
public class JornadaMilhas_AuthTest:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public JornadaMilhas_AuthTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact]
    public async Task POST_Efetua_Login_Com_Falha()
    {
       
        var user = new UserDTO { Email = "test@email.com", Password = "Senha123@" };

        using var client = _factory.CreateClient();
              
        var resultado = await client.PostAsJsonAsync("/auth-login",user);

        Assert.Equal(HttpStatusCode.BadRequest, resultado.StatusCode);
    }

    [Fact]
    public async Task POST_Efetua_Login_Com_Sucesso()
    {      

        var user = new UserDTO { Email = "tester@email.com", Password = "Senha123@" };

        using var client = _factory.CreateClient();

        var resultado = await client.PostAsJsonAsync("/auth-login", user);

        Assert.Equal(HttpStatusCode.OK, resultado.StatusCode);
    }
}
