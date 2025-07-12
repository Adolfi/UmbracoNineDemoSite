using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UmbracoNineDemoSite.Tests.Integration;
[TestFixture]
public class ContactFormIntegrationTests
{
    private HttpClient? _client;
    private WebApplicationFactory<Program>? _factory;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        WebApplicationFactoryClientOptions options = new()
        {
            AllowAutoRedirect = false, // We want to test the redirect behavior
            BaseAddress = new System.Uri("https://localhost:44372/") // Ensure this matches your application's base address
        };
        _client = _factory.CreateClient(options);
    }

    [Test]
    public async Task Post_FormModel_BindsCorrectly()
    {
        // Arrange
        var formData = new Dictionary<string, string>
        {
            { "formModel.Name", "John" },
            { "formModel.Email", "john@example.com" },
            { "formModel.Comment", "Hi" }
        };
        var content = new FormUrlEncodedContent(formData);

        // Act
        var response = await _client!.PostAsync("/Contact-us/", content);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
    }
}