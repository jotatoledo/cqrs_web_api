using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.Plant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSExample.Domain.Plants
{
    public class List
    {
        public class Query : IRequest<List<PlantDetails>>
        {
        }

        public class QueryHandler : IAsyncRequestHandler<Query, List<PlantDetails>>
        {
            private readonly StarterDbEntities _context;

            public QueryHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public Task<List<PlantDetails>> Handle(Query message)
            {
                return _context.Plant.ProjectToListAsync<PlantDetails>();
            }
        }
    }
}