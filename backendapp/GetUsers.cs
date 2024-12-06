using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backendapp
{
    public class GetUsers
    {
        private readonly ILogger _logger;
        private readonly BackendDbContext _dbContext;

        public GetUsers(ILoggerFactory loggerFactory, BackendDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<GetUsers>();
        }

        [Function("GetUsers")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req)
        {
            try
            {
                _logger.LogInformation("C# HTTP trigger function processed a request.");

                var users = await _dbContext.Users
                    .Select(
                        user => new
                        {
                            user.Id,
                            user.Username,
                            user.Lastname,
                            user.Email
                        }
                    ).ToListAsync();

                if (users == null || users.Count == 0)
                {
                    var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                    await notFoundResponse.WriteStringAsync("No users found.");
                    return notFoundResponse;
                }
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(users);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching user: {ex.Message}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("An error occurred while fetching the user.");
                return errorResponse;
            }
        }
    }
}
