using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.MaterialNumber;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.WorkCenters.MaterialNumbers
{
    public class List
    {
        public class Query : IRequest<List<MaterialNumberDetails>>
        {
            public string PlantId { get; set; }
            public string WorkCenterId { get; set; }

            public Query(string plantId, string workCenterId)
            {
                PlantId = plantId;
                WorkCenterId = workCenterId;
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, List<MaterialNumberDetails>>
        {
            private readonly StarterDbEntities _context;

            public QueryHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public async Task<List<MaterialNumberDetails>> Handle(Query message)
            {
                var workCenter = await _context.WorkCenter
                    .SingleOrDefaultAsync(wc => wc.Id == message.WorkCenterId && wc.Plant.Id == message.PlantId);
                if (workCenter == null) throw new InvalidOperationException();
                return await workCenter.MaterialNumber
                    .AsQueryable()
                    .ProjectToListAsync<MaterialNumberDetails>();
            }
        }
    }
}