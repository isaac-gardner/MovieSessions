using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSessions.Features.Upload
{
    public class UploadController : BaseController
    {
        private readonly IMediator _mediator;

        public UploadController(IMediator mediator)
            => _mediator = mediator;


        public async Task<IActionResult> UploadMovieTimes(IFormFile file)
        {
            //var result = await _mediator.Send();

            throw new NotImplementedException();
        }
    }
}
