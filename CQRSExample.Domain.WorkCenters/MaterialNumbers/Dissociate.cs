using CQRSExample.Data.Sql.StarterDb;
using MediatR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.WorkCenters.MaterialNumbers
{
    public class Dissociate
    {
        public class Command : IRequest
        {
            public string PlantId { get; set; }
            public string WorkCenterId { get; set; }
            public string MaterialNumberId { get; set; }

            public Command(string plantId, string workCenterId, string materialNumberId)
            {
                PlantId = plantId;
                WorkCenterId = workCenterId;
                MaterialNumberId = materialNumberId;
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
                var workCenter = await _context.WorkCenter
                    .SingleOrDefaultAsync(wc => wc.Id == message.WorkCenterId && wc.Plant.Id == message.PlantId);
                if (workCenter == null) throw new InvalidOperationException();

                var materialNumber = workCenter.MaterialNumber
                    .SingleOrDefault(mn => mn.Id == message.MaterialNumberId);
                if (materialNumber == null) throw new InvalidOperationException();

                workCenter.MaterialNumber.Remove(materialNumber);
                await _context.SaveChangesAsync();
            }
        }
    }
}