using EDSCore;
using HubDTOs.Documentos;
using MediatR;

namespace Hubs.Dominio.Commands
{
    public class ConfirmaCustomerCommand : IRequest<Resultado<HubDOC, ValidationFalhas>>
    {
        public ConfirmaCustomerCommand(string id, string name, string email, string cPF, string phone, string foto, string status)
        {
            Id = id;
            Name = name;
            Email = email;
            CPF = cPF;
            Phone = phone;
            Foto = foto;
            Status = status;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public string Foto { get; set; }
        public string Status { get; set; }
    }
}
