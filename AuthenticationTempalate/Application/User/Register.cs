using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string UserName { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            public Handler(DataContext context, UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                _context = context;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                // No duplicate email (could be set in configuration for Identity)
                if(await _userManager.FindByEmailAsync(request.Email) != null)
                    throw new Exception("400:Email already exists");

                // No duplicate names
                if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
                    throw new Exception("400:Username already exists");

                var user = new AppUser
                {
                    Email = request.Email,
                    UserName=request.UserName,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new User
                    {
                        UserName = user.UserName
                    };
                }

                throw new Exception("Error registering user");
            }
        }
    }
}