using CQRSExample.Data.Sql.StarterDb;
using MediatR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.WorkCenters
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string PlantId { get; set; }
            public string WorkCenterId { get; set; }

            public Command(string plantId, string workCenterId)
            {
                PlantId = plantId;
                WorkCenterId = workCenterId;
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
                var plant = await _context.Plant.SingleOrDefaultAsync(pl => pl.Id == message.PlantId);
                if (plant == null) throw new InvalidOperationException();
                var workCenter = plant.WorkCenter.SingleOrDefault(wc => wc.Id == message.WorkCenterId);
                if (workCenter == null) throw new InvalidOperationException();
                _context.WorkCenter.Remove(workCenter);
                await _context.SaveChangesAsync();
            }
        }
    }
}