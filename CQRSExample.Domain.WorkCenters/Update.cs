using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.WorkCenter;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CQRSExample.Domain.WorkCenters
{
    public class Update
    {
        public class Command : IRequest
        {
            public string PlantId { get; set; }
            public string WorkCenterId { get; set; }
            public WorkCenterData Model { get; set; }

            public Command(string plantId, string workCenterId, WorkCenterData model)
            {
                PlantId = plantId;
                WorkCenterId = workCenterId;
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
                var workCenter = await _context.WorkCenter
                    .SingleOrDefaultAsync(wc => wc.Id == message.WorkCenterId && wc.Plant.Id == message.PlantId);
                if (workCenter == null) throw new InvalidOperationException();
                _context.Entry(workCenter).CurrentValues.SetValues(message.Model);
                await _context.SaveChangesAsync();
            }
        }
    }
}