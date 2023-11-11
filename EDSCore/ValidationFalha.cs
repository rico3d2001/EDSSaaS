namespace EDSCore
{
    public class ValidationFalha
    {
        public ValidationFalha(string tipo, string message)
        {
            Tipo = tipo;
            Message = message;
        }

        public string Tipo { get; private set; }
        public string Message { get; private set; }
    }
}
