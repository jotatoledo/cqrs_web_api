using CQRSExample.Data.Sql.StarterDb;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.WorkCenters.MaterialNumbers
{
    public class Associate
    {
        public class Command : IRequest
        {
            public string PlantId { get; set; }
            public string WorkCenterId { get; set; }
            public IEnumerable<string> MaterialNumberId { get; set; }

            public Command(string plantId, string workCenterId, IEnumerable<string> materialNumberId)
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
                var materialNumbers = _context.MaterialNumber.Where(mn => message.MaterialNumberId.Contains(mn.Id));
                await materialNumbers.ForEachAsync(mn => workCenter.MaterialNumber.Add(mn));
                await _context.SaveChangesAsync();
            }
        }
    }
}