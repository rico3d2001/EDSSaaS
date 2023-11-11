using EDSCore;
using HubDTOs.Documentos;
using MediatR;

namespace Hubs.Dominio.Commands
{
    public class IniciaHubCommand : IRequest<Resultado<HubDOC, ValidationFalhas>>
    {
       
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}


//public class IniciaCustomerCommand
//{
//    public string Email { get; set; } = string.Empty;
//    public string NomeUsuario { get; set; } = string.Empty;
//    public string Foto { get; set; } = string.Empty;
//    public string Status { get; set; } = string.Empty;
//}