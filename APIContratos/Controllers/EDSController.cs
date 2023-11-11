using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIContratos.Controllers
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
