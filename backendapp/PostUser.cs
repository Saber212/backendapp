using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Azure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;

namespace backendapp
{
    public class PostUser
    {
        private readonly ILogger _logger;
        private readonly BackendDbContext _dbContext;

        public PostUser(ILoggerFactory loggerFactory, BackendDbContext dbContext)
        {
            _logger = loggerFactory.CreateLogger<PostUser>();
            _dbContext = dbContext;
        }

        [Function("PostUser")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var user = JsonSerializer.Deserialize<Users>(requestBody);
            _logger.LogInformation($"user: {user}");
            if (user == null)
            {
                var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequestResponse.WriteStringAsync("Invalid user data.");
                return badRequestResponse;
            }

            if (!string.IsNullOrWhiteSpace(user.HashedPassword))
            {
                user.HashedPassword = HashedPassword(user.HashedPassword);
            }
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(user);
            return response;
        }
        private string HashedPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashed = Convert.ToBase64String(hashBytes);
            return hashed;
        }
    }
}

