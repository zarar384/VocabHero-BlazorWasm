using MediatR;

namespace VocabHero.CQRS
{
    public interface IQuery<out TResponse>: IRequest<TResponse> 
        where TResponse : notnull
    {
    }
}
