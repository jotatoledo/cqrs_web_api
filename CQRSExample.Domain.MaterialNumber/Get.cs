using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.MaterialNumber;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSExample.Domain.MaterialNumbers
{
    public class Get
    {
        public class Query : IRequest<MaterialNumberDetails>
        {
            public string Id { get; set; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, MaterialNumberDetails>
        {
            private StarterDbEntities _context;

            public QueryHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public Task<MaterialNumberDetails> Handle(Query message)
            {
                return _context.MaterialNumber
                    .Where(mn => mn.Id == message.Id)
                    .ProjectToSingleOrDefaultAsync<MaterialNumberDetails>();
            }
        }
    }
}