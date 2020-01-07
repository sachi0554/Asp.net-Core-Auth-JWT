using App.Core.RequestFlow;
using App.Core.ResponseFlow;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace App.Testing
{
    public class AccountContoller:Base
    {
        [Fact]
        public async void Login_success()
        {
            var response = await TestClient.PostAsJsonAsync("Account/login", new UserRegistrationRequest
            {
                Email = "test@demo.com",
                Password = "Neo@0554"
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void Login_Faild()
        {
            var response = await TestClient.PostAsJsonAsync("Account/login", new UserRegistrationRequest
            {
                Email = "test@demo.com",
                Password = "Neo@s"
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Login_Null_value()
        {
            var response = await TestClient.PostAsJsonAsync("Account/login", new UserRegistrationRequest
            {
                Email = "",
                Password = ""
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Register_Null_value()
        {
            var response = await TestClient.PostAsJsonAsync("Account/register", new UserRegistrationRequest
            {
                Email = "",
                Password = "",
                PhoneNumber="",
                FirstName="",
                LastName=""
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Register_on_success()
        {
            var response = await TestClient.PostAsJsonAsync("Account/register", new UserRegistrationRequest
            {
                Email = "testk27r32@demo.com",
                Password = "kill@34343Bill",
                PhoneNumber="8687314864",
                FirstName="demo",
                LastName="demo"
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            Assert.NotEmpty(registrationResponse.Token);
        }
    }
}
