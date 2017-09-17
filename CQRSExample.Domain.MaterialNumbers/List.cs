using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.MaterialNumber;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSExample.Domain.MaterialNumbers
{
    public class List
    {
        public class Query : IRequest<List<MaterialNumberDetails>>
        {
        }

        public class QueryHandler : IAsyncRequestHandler<Query, List<MaterialNumberDetails>>
        {
            private readonly StarterDbEntities _context;

            public QueryHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public Task<List<MaterialNumberDetails>> Handle(Query message)
            {
                // TODO add pagination
                return _context.MaterialNumber.ProjectToListAsync<MaterialNumberDetails>();
            }
        }
    }
}