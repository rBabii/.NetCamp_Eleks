using BlogPlatform.Infrastructure.HttpServices.HttpResult;
using External.DTOs.Auth.Models.Request;
using External.DTOs.Auth.Models.Response;
using External.DTOs.Common.Models;
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

        private JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

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
                    <RegisterResponse>(successResponseStream, JsonSerializerOptions);

                var successResult = new HttpResult<RegisterResponse>(response.StatusCode)
                {
                    ResponseObject = successObject,
                };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream, JsonSerializerOptions);

            var errorResult = new HttpResult<RegisterResponse>(response.StatusCode)
            {
                Error = errorObject
            };

            return errorResult;
        }

        public async Task<HttpResult<VerifyUserEmailResponse>> VerifyUserEmail(VerifyUserEmailRequest verifyUserEmailRequest)
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
                    <VerifyUserEmailResponse>(successResponseStream, JsonSerializerOptions);

                var successResult = new HttpResult<VerifyUserEmailResponse>(response.StatusCode)
                {
                    ResponseObject = successObject,
                };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream, JsonSerializerOptions);

            var errorResult = new HttpResult<VerifyUserEmailResponse>(response.StatusCode)
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
                    <GetResetPasswordTokenResponse>(successResponseStream, JsonSerializerOptions);

                var successResult = new HttpResult<GetResetPasswordTokenResponse>(response.StatusCode)
                {
                    ResponseObject = successObject,
                };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream, JsonSerializerOptions);

            var errorResult = new HttpResult<GetResetPasswordTokenResponse>(response.StatusCode)
            {
                Error = errorObject
            };

            return errorResult;

        }


        public async Task<HttpResult<GetEmailVerificationTokenResponse>> GetEmailVerificationToken(GetEmailVerificationTokenRequest getEmailVerificationTokenRequest)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(getEmailVerificationTokenRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("api/auth/GetEmailVerificationToken", body);

            if (response.IsSuccessStatusCode)
            {
                using var successResponseStream = await response.Content.ReadAsStreamAsync();

                var successObject = await JsonSerializer.DeserializeAsync
                    <GetEmailVerificationTokenResponse>(successResponseStream, JsonSerializerOptions);

                var successResult = new HttpResult<GetEmailVerificationTokenResponse>(response.StatusCode)
                {
                    ResponseObject = successObject,
                };

                return successResult;
            }

            using var errorResponseStream = await response.Content.ReadAsStreamAsync();
            var errorObject = await JsonSerializer.DeserializeAsync
                <Error>(errorResponseStream, JsonSerializerOptions);

            var errorResult = new HttpResult<GetEmailVerificationTokenResponse>(response.StatusCode)
            {
                Error = errorObject
            };

            return errorResult;

        }

        public async Task<HttpResult<object>> ResetPassword(ResetPasswordRequest resetPasswordRequest)
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

        public async Task<HttpResult<object>> IsValidUser(IsValidRequest isValidRequest)
        {
            var body = new StringContent(
                JsonSerializer.Serialize(isValidRequest),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("api/auth/IsValid", body);

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
