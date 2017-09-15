using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.WorkCenter;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.Domain.MaterialNumbers.WorkCenters
{
    public class List
    {
        public class Query : IRequest<IEnumerable<WorkCenterDetails>>
        {
            public string MaterialNumberId { get; set; }

            public Query(string materialNumberId)
            {
                MaterialNumberId = materialNumberId;
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, IEnumerable<WorkCenterDetails>>
        {
            private readonly StarterDbEntities _context;

            public QueryHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public async Task<IEnumerable<WorkCenterDetails>> Handle(Query message)
            {
                var materialNumber = await _context.MaterialNumber.SingleOrDefaultAsync(mn => mn.Id == message.MaterialNumberId);
                if (materialNumber == null) throw new InvalidOperationException();
                return await materialNumber.WorkCenter.AsQueryable().ProjectToListAsync<WorkCenterDetails>();
            }
        }
    }
}