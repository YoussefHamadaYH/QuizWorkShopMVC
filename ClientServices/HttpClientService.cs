using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizWorkShopMVC.DTO;
using System.Net.Http;
using System.Text;

namespace QuizWorkShopMVC.ClientServices
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7022/");
        }
        public async Task<List<QuizVM>> GetAllQuizzesAsync()
        {
            var response = await _httpClient.GetAsync("api/Quiz");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + data);
                try
                {
                    // Deserialize the response to a list of QuizDTO
                    return JsonConvert.DeserializeObject<List<QuizVM>>(data);
                }
                catch (JsonReaderException ex)
                {
                    Console.WriteLine("JSON Deserialization Error: " + ex.Message);
                    return new List<QuizVM>();
                }
            }
            return new List<QuizVM>();
        }
        public async Task<bool> InsertQuizAsync(QuizVM quizVM)
        {
            var content = new StringContent(JsonConvert.SerializeObject(quizVM), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Quiz", content);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateQuizAsync(QuizVM quiz)
        {
            var content = new StringContent(JsonConvert.SerializeObject(quiz), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Quiz/{quiz.QuizId}", content);

            return response.IsSuccessStatusCode;
        }
        public async Task<QuizVM> GetQuizByIdAsync(int quizId)
        {
            var response = await _httpClient.GetAsync($"api/Quiz/{quizId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<QuizVM>(data);
            }
            return null;
        }
    }
}
