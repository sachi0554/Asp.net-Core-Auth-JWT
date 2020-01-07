using App.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace App.Testing
{
    public class GameController:Base
    {
        [Fact]
        public async void Game_on_create_check_required_field()
        {
            await AuthenticateAsync();
            var response = await TestClient.PostAsJsonAsync("Api/game/create", new Game
            {
                GameCode = "",
                GameName="",
                GameType="NA",
                Game_Description="Thwo Partipaction do this this "
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Game_on_create_success()
        {
            await AuthenticateAsync();
            var response = await TestClient.PostAsJsonAsync("Api/game/create", new Game
            {
                GameCode = "54545",
                GameName = "New Game",
                GameType = "NA",
                Game_Description = "Thwo Partipaction do this this"
            }); 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void Get_all_games_not_null()
        {
            await AuthenticateAsync();
            var response = await TestClient.GetAsync("api/game/AllGames");

            // Assert
            Assert.NotNull(response.Content.ReadAsAsync<Game>());
        }

        [Fact]
        public async void Get_all_games_count()
        {
            await AuthenticateAsync();
            var response = await TestClient.GetAsync("api/game/AllGames");

           
            var data = await response.Content.ReadAsStringAsync();
            List<Game> game = JsonConvert.DeserializeObject<List<Game>>(data);
            // Assert
            var count = game.Count;
            Console.WriteLine(count);
            Assert.Equal(2, count);
        }

        [Fact]
        public async void Game_on_Delete_success()
        {
            string id = "5d224ebf-ed51-41df-90b7-ec8c3c0bb6cd";
            await AuthenticateAsync();
            var response = await TestClient.DeleteAsync("Api/game/id?id=" + id);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Game_on_Delete_not_found()
        { 
            string id = "041538d2-0f6b-4d42-9902-02b179899539";
            await AuthenticateAsync();
            var response = await TestClient.DeleteAsync("Api/game/id?id=" + id);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void Get_game_by_id_on_success()
        {
            string id = "4f6b3785-011b-4b93-862b-4ab36de95811";
            await AuthenticateAsync();
            var response = await TestClient.GetAsync("api/game/id?id="+id);


            var data = await response.Content.ReadAsStringAsync();
           // List<Game> game = JsonConvert.DeserializeObject<List<Game>>(data);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Get_game_by_id_not_found()
        {
            string id = "041538d2-0f6b-4d42-9902-02b179899539";
            await AuthenticateAsync();
            var response = await TestClient.GetAsync("api/game/id?id=" + id);


            var data = await response.Content.ReadAsStringAsync();
            // List<Game> game = JsonConvert.DeserializeObject<List<Game>>(data);
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
