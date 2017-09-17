using CQRSExample.Data.Sql.StarterDb;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.MaterialNumbers.WorkCenters
{
    public class Associate
    {
        public class Command : IRequest
        {
            public string MaterialNumberId { get; set; }
            public IEnumerable<string> WorkCenterId { get; set; }

            public Command(string materialNumberId, IEnumerable<string> workCenterId)
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
                var workCenters = await _context.WorkCenter
                    .Where(wc => message.WorkCenterId.Contains(wc.Id))
                    .ToListAsync();
                workCenters.ForEach(wc => materialNumber.WorkCenter.Add(wc));
                await _context.SaveChangesAsync();
            }
        }
    }
}