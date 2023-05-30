using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace N5Permission.Infrastructure.Persistance
{
    public class N5PermissionContextInitializer
    {
        private readonly ILogger<N5PermissionContextInitializer> _logger;
        private readonly N5PermissionContext _context;

        public N5PermissionContextInitializer(ILogger<N5PermissionContextInitializer> logger,N5PermissionContext context)
        {
            _logger = logger;
            _context = context;   
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                    _logger.LogInformation("Migration finished");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }
    }
}
