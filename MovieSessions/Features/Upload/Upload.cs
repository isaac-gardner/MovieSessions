using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSessions.Features.Upload
{
    public class Upload
    {
        public class Query: IRequest<Command>
        { }


        public class Command
        {
            public IFormFile? File { get; set; } = null;
        }

        public class CommandValidator : AbstractValidator<Command> 
        { 
        }
    }
}
