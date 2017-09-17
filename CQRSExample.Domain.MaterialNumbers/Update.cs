using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.MaterialNumber;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CQRSExample.Domain.MaterialNumbers
{
    public class Update
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
            public MaterialNumberData Model { get; set; }

            public Command(string id, MaterialNumberData model)
            {
                Id = id;
                Model = model;
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
                var materialNumber = await _context.MaterialNumber
                    .SingleOrDefaultAsync(mn => mn.Id == message.Id);
                if (materialNumber == null) throw new InvalidOperationException();
                _context.Entry(materialNumber).CurrentValues.SetValues(message.Model);
                await _context.SaveChangesAsync();
            }
        }
    }
}