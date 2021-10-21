using BlogPlatform.Infrastructure.HttpServices.HttpResult;
using DTOs.Auth.Models.Request;
using DTOs.Auth.Models.Response;
using DTOs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.HttpServices.Auth
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResult<RegisterResponse>> Register(RegisterRequest registerRequest)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(registerRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("api/auth/register", body);

            if (response.IsSuccessStatusCode)
            {
                using var successResponseStream = await response.Content.ReadAsStreamAsync();

                var successObject = await JsonSerializer.DeserializeAsync
                    <RegisterResponse>(successResponseStream);

                var successResult = new HttpResult<RegisterResponse>(response.StatusCode)
                {
                    ResponseObject = successObject,
                };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream);

            var errorResult = new HttpResult<RegisterResponse>(response.StatusCode)
            {
                Error = errorObject
            };

            return errorResult;
        }

        public async Task<HttpResult<string>> VerifyUserEmail(VerifyUserEmailRequest verifyUserEmailRequest)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(verifyUserEmailRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("api/auth/VerifyUser", body);

            if (response.IsSuccessStatusCode)
            {
                using var successResponseStream = await response.Content.ReadAsStreamAsync();

                var successObject = await JsonSerializer.DeserializeAsync
                    <string>(successResponseStream);

                var successResult = new HttpResult<string>(response.StatusCode)
                {
                    ResponseObject = successObject,
                };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream);

            var errorResult = new HttpResult<string>(response.StatusCode)
            {
                Error = errorObject
            };

            return errorResult;
        }

        public async Task<HttpResult<GetResetPasswordTokenResponse>> GetResetPasswordToken(GetResetPasswordTokenRequest getResetPasswordTokenRequest)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(getResetPasswordTokenRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("api/auth/GetResetPasswordToken", body);

            if (response.IsSuccessStatusCode)
            {
                using var successResponseStream = await response.Content.ReadAsStreamAsync();

                var successObject = await JsonSerializer.DeserializeAsync
                    <GetResetPasswordTokenResponse>(successResponseStream);

                var successResult = new HttpResult<GetResetPasswordTokenResponse>(response.StatusCode)
                {
                    ResponseObject = successObject,
                };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream);

            var errorResult = new HttpResult<GetResetPasswordTokenResponse>(response.StatusCode)
            {
                Error = errorObject
            };

            return errorResult;

        }

        public async Task<HttpResult<string>> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(resetPasswordRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("api/auth/ResetPassword", body);

            if (response.IsSuccessStatusCode)
            {
                using var successResponseStream = await response.Content.ReadAsStreamAsync();

                var successObject = await JsonSerializer.DeserializeAsync
                    <string>(successResponseStream);

                var successResult = new HttpResult<string>(response.StatusCode)
                {
                    ResponseObject = successObject,
                };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream);

            var errorResult = new HttpResult<string>(response.StatusCode)
            {
                Error = errorObject
            };

            return errorResult;
        }
    }
}
