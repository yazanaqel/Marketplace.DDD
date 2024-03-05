using MediatR;

namespace Application.Commands;
internal interface ICommand<out TResult> : IRequest<TResult> {
}
