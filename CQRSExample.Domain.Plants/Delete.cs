using CQRSExample.Data.Sql.StarterDb;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CQRSExample.Domain.Plants
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Id { get; set; }

            public Command(string id)
            {
                Id = id;
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private readonly StarterDbEntities _context;

            public CommandHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public async Task Handle(Command message)
            {
                var plant = await _context.Plant.SingleOrDefaultAsync(p => p.Id == message.Id);
                if (plant == null) throw new InvalidOperationException();
                _context.Plant.Remove(plant);
                await _context.SaveChangesAsync();
            }
        }
    }
}