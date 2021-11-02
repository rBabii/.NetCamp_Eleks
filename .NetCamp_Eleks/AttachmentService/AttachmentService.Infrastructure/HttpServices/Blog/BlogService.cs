using AttachmentService.Infrastructure.HttpServices.HttpResult;
using External.DTOs.BlogPlatform.Models.Request;
using External.DTOs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AttachmentService.Infrastructure.HttpServices.Blog
{
    public class BlogService
    {
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public BlogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<HttpResult<object>> SaveUserImage(SaveUserImageRequest saveUserImageRequest)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(saveUserImageRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("api/user/SaveUserImage", body);

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
