using CQRSExample.Data.Sql.StarterDb;
using MediatR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.MaterialNumbers.WorkCenters
{
    public class Dissociate
    {
        public class Command : IRequest
        {
            public string MaterialNumberId { get; set; }
            public string WorkCenterId { get; set; }

            public Command(string materialNumberId, string workCenterId)
            {
                MaterialNumberId = materialNumberId;
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
                var materialNumber = await _context.MaterialNumber.SingleOrDefaultAsync(mn => mn.Id == message.MaterialNumberId);
                if (materialNumber == null) throw new InvalidOperationException();
                var workCenter = materialNumber.WorkCenter.SingleOrDefault(wc => wc.Id == message.WorkCenterId);
                if (workCenter == null) throw new InvalidOperationException();
                materialNumber.WorkCenter.Remove(workCenter);
                await _context.SaveChangesAsync();
            }
        }
    }
}