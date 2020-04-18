

namespace MovieSessions.Features.Movies
{
    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using FluentValidation;
    using MediatR;
    using MovieSessions.Data;
    using MovieSessions.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class Add
    {

        public class Query : IRequest<Command>
        {  }

        public class QueryHandler : RequestHandler<Query, Command>
        {
            protected override Command Handle(Query request)
            {
                return new Command();
            }
        }
        public class Command : IRequest<Guid>
        {
            public string Year { get; set; } = "";
            public string Title { get; set; } = "";
            public Rating Rating { get; set; } = Rating.Unknown;
            public int Runtime { get; set; }
            public string Director { get; set; } = "";
            public string Synopsis { get; set; } = "";

            [Ignore]
            public List<string> RatingOptions { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(m => m.Title).NotNull().NotEmpty();
                RuleFor(m => m.Rating).NotNull().NotEmpty();
                RuleFor(m => m.Runtime).GreaterThan(0).LessThan(250);
                
            }
        }

        public class CommandHandler : IRequestHandler<Command, Guid>
        {
            private readonly SessionDatabase _db;
            private readonly IMapper _mapper;

            public CommandHandler(SessionDatabase db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var movie = _mapper.Map<Movie>(request);
                                  
                _db.Movies.Add(movie);
                await _db.SaveChangesAsync();
                return movie.Id;
            }
        }


    }
}
