using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class Login
{
    public class Query : IRequest<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Handler : IRequestHandler<Query, User>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<User> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new Exception("400:Wrong username or password");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                return new User
                {
                    UserName = user.UserName,
                    Token = _jwtGenerator.CreateToken(user)
                };
            }
            throw new Exception("400:Wrong username or password");
        }
    }
}