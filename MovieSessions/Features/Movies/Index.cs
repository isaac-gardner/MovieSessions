using AutoMapper;
using MediatR;
using MovieSessions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieSessions.Features.Movies
{
    public class Index
    {
        public class Query : IRequest<Result>
        { }

        public class Result
        {
            public string Summary;
            public string Synopsis;
        }

        public class QueryHandler : IRequestHandler<Query, Result>
        {
            private readonly SessionDatabase _db;
            private readonly IMapper _mapper;
            public QueryHandler(SessionDatabase db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}
