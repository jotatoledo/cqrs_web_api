using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.WorkCenter;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.WorkCenters
{
    public class List
    {
        public class Query : IRequest<List<WorkCenterDetails>>
        {
        }

        public class QueryHandler : IAsyncRequestHandler<Query, List<WorkCenterDetails>>
        {
            private readonly StarterDbEntities _context;

            public QueryHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public Task<List<WorkCenterDetails>> Handle(Query message)
            {
                return _context.WorkCenter.ProjectToListAsync<WorkCenterDetails>();
            }
        }

        public class QueryFromPlant : IRequest<List<WorkCenterDetails>>
        {
            public string PlantId { get; set; }

            public QueryFromPlant(string plantId)
            {
                PlantId = plantId;
            }
        }

        public class QueryFromPlantHandler : IAsyncRequestHandler<QueryFromPlant, List<WorkCenterDetails>>
        {
            private readonly StarterDbEntities _context;

            public QueryFromPlantHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public async Task<List<WorkCenterDetails>> Handle(QueryFromPlant message)
            {
                var plant = await _context.Plant.SingleOrDefaultAsync(pl => pl.Id == message.PlantId);
                if (plant == null) throw new InvalidOperationException();
                return await _context.WorkCenter
                    .Where(wc => wc.Plant.Id == message.PlantId)
                    .ProjectToListAsync<WorkCenterDetails>();
            }
        }
    }
}