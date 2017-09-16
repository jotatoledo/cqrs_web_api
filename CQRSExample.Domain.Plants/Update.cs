using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.Plant;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CQRSExample.Domain.Plants
{
    public class Update
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
            public PlantData Model { get; set; }

            public Command(string id, PlantData model)
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
                var plant = await _context.Plant.SingleOrDefaultAsync(pl => pl.Id == message.Id);
                if (plant == null) throw new InvalidOperationException();
                _context.Entry(plant).CurrentValues.SetValues(message.Model);
                await _context.SaveChangesAsync();
            }
        }
    }
}