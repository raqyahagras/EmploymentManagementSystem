using EmploymentManagementSystem.Core.Entities;
using EmploymentManagementSystem.Infrastructure.Data;
using EmploymentManagementSystem.API.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EmploymentManagementSystem.API.Attribute;

    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionPolicyProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
            => Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy?>(null);

        public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var permissionExists = await dbContext.Permissions.AnyAsync(p => p.Name == policyName);
            if (!permissionExists) return null;

            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();

            return policy;
        }
    }


