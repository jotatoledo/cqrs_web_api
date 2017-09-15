using CQRSExample.Data.Sql.StarterDb;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CQRSExample.Domain.MaterialNumbers
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
            private StarterDbEntities _context;

            public CommandHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public async Task Handle(Command message)
            {
                var materialNumber = await _context.MaterialNumber.SingleOrDefaultAsync(mn => mn.Id == message.Id);
                if (materialNumber == null) throw new InvalidOperationException();
                _context.MaterialNumber.Remove(materialNumber);
                await _context.SaveChangesAsync();
            }
        }
    }
}