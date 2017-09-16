using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.WorkCenter;
using MediatR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.WorkCenters
{
    public class Details
    {
        public class Query : IRequest<WorkCenterDetails>
        {
            public string PlantId { get; set; }
            public string WorkCenterId { get; set; }

            public Query(string plantId, string workCenterId)
            {
                PlantId = plantId;
                WorkCenterId = workCenterId;
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, WorkCenterDetails>
        {
            private readonly StarterDbEntities _context;

            public QueryHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public async Task<WorkCenterDetails> Handle(Query message)
            {
                var plant = await _context.Plant.SingleOrDefaultAsync(pl => pl.Id == message.PlantId);
                if (plant == null) throw new InvalidOperationException();
                var workCenter = await _context.WorkCenter
                    .Where(wc => wc.Id == message.WorkCenterId && wc.Plant == plant)
                    .ProjectToSingleOrDefaultAsync<WorkCenterDetails>();
                if (workCenter == null) throw new InvalidOperationException();
                return workCenter;
            }
        }
    }
}