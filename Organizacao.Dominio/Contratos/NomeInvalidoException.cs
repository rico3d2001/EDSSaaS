namespace Organizacao.Dominio.Contratos
{
    public class NomeInvalidoException : Exception
    {
        public NomeInvalidoException() { }

        public NomeInvalidoException(string nome) : base(string.Format("Nome inválido: {0}", nome)) { }
    }
}
