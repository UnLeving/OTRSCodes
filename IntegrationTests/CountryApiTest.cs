using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using otrsCodes;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace IntegrationTests
{
    public class CountryApiTest
    {
        readonly HttpClient _httpClient;
        public CountryApiTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<MvcApplication>());
            _httpClient = server.CreateClient();
        }
        [Theory]
        [InlineData("GET")]
        public async void GetAllCountries(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/CountryDropDown/");

            // act
            var response = await _httpClient.SendAsync(request);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
