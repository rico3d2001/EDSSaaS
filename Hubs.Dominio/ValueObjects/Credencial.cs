using EDSCore;

namespace Hubs.Dominio.ValueObjects
{
    public record Credencial : ValueObject
    {
        public Credencial(string email, string nomeUsuario, string foto)
        {
            Email = email;
            NomeUsuario = nomeUsuario;
            Foto = foto;
        }

        public string Email { get; private set; }
        public string NomeUsuario { get; private set; }
        public string Foto { get; private set; }
    }
}
