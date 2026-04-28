using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Application.Test.Common
{
    public class BaseTestClass : IDisposable, IAsyncLifetime
    {
        protected readonly UserManager<ApplicationUser> _userManager;

        private IDbContextTransaction _transaction;

        internal readonly AppDbContext _context;
        internal readonly ITestOutputHelper _output;
        internal readonly IServiceScope _testScope;

        public BaseTestClass(DatabaseFixture fixture, ITestOutputHelper output)
        {
            if (fixture.ServiceProvider == null)
            {
                throw new ArgumentNullException(nameof(fixture.ServiceProvider));
            }
            _testScope = fixture.ServiceProvider.CreateScope();
            _context = (AppDbContext)_testScope.ServiceProvider.GetRequiredService<IDbContext>();
            _userManager = _testScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _output = output;
        }

        private string _userId;
        private string[] _roles;
        protected async Task RunAsDefaultUserAsync()
        {
            await RunAsUserAsync("simon.moreau@eai.fr");
        }

        protected async Task RunAsUserAsync(string email)
        {
            ApplicationUser? user = await _context.Users.Where(u => u.Email == email).FirstAsync();

            IList<string> roles = await _userManager.GetRolesAsync(user);

            _userId = user.Id;
            _roles = roles.ToArray();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            if (request is IAuthenticatedRequest<TResponse>)
            {
                AuthenticateRequest(request);
            }
            ISender mediator = _testScope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        private void AuthenticateRequest<TResponse>(IRequest<TResponse> request)
        {
            ((IAuthenticatedRequest<TResponse>)request).SetupRequest(_userId, _roles);
        }


        public void Dispose()
        {
            _testScope.Dispose();
        }

        public async Task InitializeAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task DisposeAsync()
        {
            await _transaction.DisposeAsync();
        }
    }
}
