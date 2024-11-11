using Carter;
using Mapster;
using MediatR;
using VocabHero.Modules.UserModule.Commands;

namespace VocabHero.Modules.UserModule.Endpoints
{
    public record LoginUserRequest(string UserName, string Password);

    public record LoginUserResponse(bool IsSuccess);

    public class LoginUserEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (LoginUserRequest request, ISender sender) =>
            {
                var command = request.Adapt<LoginUserCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<LoginUserResponse>();

                return Results.Ok(response);
            })
            .WithName("LoginUser")
            .Produces<LoginUserResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Login User")
            .WithDescription("Login User");
        }
    }
}