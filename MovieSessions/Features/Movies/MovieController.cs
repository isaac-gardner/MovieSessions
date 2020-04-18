using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSessions.Features.Movies
{
    public class MovieController : BaseController
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
            => _mediator = mediator;

        public async Task<IActionResult> Add()
            => View(nameof(Add), await _mediator.Send(new Add.Query()));


    }
}
