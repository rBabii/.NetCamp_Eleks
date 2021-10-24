using BlogPlatform.Infrastructure.HttpServices.HttpResult;
using External.DTOs.Common.Models;
using External.DTOs.EmailService.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.HttpServices.Email
{
    public class EmailService
    {
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResult<object>> Send(SendEmailRequest sendEmailRequest)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(sendEmailRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("api/Email", body);

            if (response.IsSuccessStatusCode)
            {
                using var successResponseStream = await response.Content.ReadAsStreamAsync();

                var successResult = new HttpResult<object>(response.StatusCode)
                { };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream, JsonSerializerOptions);

            var errorResult = new HttpResult<object>(response.StatusCode)
            {
                Error = errorObject
            };

            return errorResult;
        }
    }
}
