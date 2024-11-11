using VocabHero.CQRS;

namespace VocabHero.Modules.UserModule.Commands
{
    public record LoginUserCommand(string UserName, string Password): ICommand<LoginUserResult>;

    public record LoginUserResult(string Jwt);
}
