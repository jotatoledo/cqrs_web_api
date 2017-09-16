using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.Plant;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.Plants
{
    public class Details
    {
        public class Query : IRequest<PlantDetails>
        {
            public string Id { get; set; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, PlantDetails>
        {
            private readonly StarterDbEntities _context;

            public QueryHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public async Task<PlantDetails> Handle(Query message)
            {
                var plant = await _context.Plant.Where(p => p.Id == message.Id)
                    .ProjectToSingleOrDefaultAsync<PlantDetails>();
                if (plant == null) throw new InvalidOperationException();
                return plant;
            }
        }
    }
}