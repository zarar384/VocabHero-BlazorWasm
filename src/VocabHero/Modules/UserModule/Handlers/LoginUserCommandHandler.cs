using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VocabHero.Data.Repositories;
using VocabHero.Domain.Entities;
using VocabHero.Modules.UserModule.Commands;

namespace VocabHero.Modules.UserModule.Handlers
{
    public class LoginUserCommandHandler
        (IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IConfiguration configuration) 
        : IRequestHandler<LoginUserCommand, LoginUserResult>
    {
        public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByUserNameAsync(request.UserName);

            if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid UserName.");
            }

            var verificationResult = passwordHasher.VerifyHashedPassword(user, request.Password, user.PasswordHash);

            if(verificationResult != PasswordVerificationResult.Success)
            {
                throw new UnauthorizedAccessException("Invalid Password.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                //add any additional claims
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.WriteToken(token);

            return new LoginUserResult(jwtToken);
        }
    }
}
