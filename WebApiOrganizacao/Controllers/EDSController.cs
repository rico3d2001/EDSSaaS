using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiOrganizacao.Controllers
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
