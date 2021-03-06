﻿using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.MaterialNumber;
using MediatR;
using System;
using System.Threading.Tasks;

namespace CQRSExample.Domain.MaterialNumbers
{
    public class Create
    {
        public class Command : IRequest
        {
            public MaterialNumberData Model { get; set; }

            public Command(MaterialNumberData model)
            {
                Model = model;
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private StarterDbEntities _context;

            public CommandHandler(StarterDbEntities context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                _context = context;
            }

            public async Task Handle(Command message)
            {
                var materialNumber = new MaterialNumber();
                _context.MaterialNumber.Add(materialNumber);
                _context.Entry(materialNumber).CurrentValues.SetValues(message.Model);
                await _context.SaveChangesAsync();
            }
        }
    }
}