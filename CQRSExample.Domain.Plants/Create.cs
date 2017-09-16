using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.Plant;
using MediatR;
using System;
using System.Threading.Tasks;

namespace CQRSExample.Domain.Plants
{
    public class Create
    {
        public class Command : IRequest
        {
            public PlantData Model { get; set; }

            public Command(PlantData model)
            {
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
                var plant = new Plant();
                _context.Plant.Add(plant);
                _context.Entry(plant).CurrentValues.SetValues(message.Model);
                await _context.SaveChangesAsync();
            }
        }
    }
}