using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.WorkCenter;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CQRSExample.Domain.WorkCenters
{
    public class Create
    {
        public class Command : IRequest
        {
            public string PlantId { get; set; }
            public WorkCenterData Model { get; set; }

            public Command(string plantId, WorkCenterData model)
            {
                PlantId = plantId;
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

            public Task Handle(Command message)
            {
                throw new NotImplementedException();
            }

            public async Task privateHandle(Command message)
            {
                var plant = await _context.Plant.SingleOrDefaultAsync(pl => pl.Id == message.PlantId);
                if (plant == null) throw new InvalidOperationException();
                var workCenter = new WorkCenter
                {
                    Plant = plant
                };
                _context.WorkCenter.Add(workCenter);
                _context.Entry(workCenter).CurrentValues.SetValues(message.Model);
                await _context.SaveChangesAsync();
            }
        }
    }
}