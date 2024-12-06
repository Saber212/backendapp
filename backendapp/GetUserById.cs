using System.Data.Common;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace backendapp
{
    public class GetUserById
    {
        private readonly ILogger _logger;
        private readonly BackendDbContext _dbcontext;

        public GetUserById(ILoggerFactory loggerFactory, BackendDbContext dbcontext)
        {
            _logger = loggerFactory.CreateLogger<GetUserById>();
            _dbcontext = dbcontext;
        }

        [Function("GetUserById")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")]
        HttpRequestData req, int id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var user = await _dbcontext.Users.FindAsync(id);

                if (user == null)
                {
                    var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                    await notFoundResponse.WriteStringAsync("User not found.");
                    return notFoundResponse;
                }
                var response = req.CreateResponse(HttpStatusCode.OK);

                await response.WriteAsJsonAsync(user);

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
