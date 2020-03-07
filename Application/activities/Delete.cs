using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //Handler Logic Goes Here
                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity == null)
                {
                    throw new Exception("Activity is not exist !!");
                }
                _context.Activities.Remove(activity);
                var success = await _context.SaveChangesAsync();
                if (success > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Problem saving changes !!");
            }
        }
    }
}