using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContasAPI.Controllers
{
    public class EDSController : ControllerBase
    {
        protected IMediator _mediator;

        public EDSController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
