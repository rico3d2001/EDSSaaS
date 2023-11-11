namespace ServicosEmailHub
{
    public interface IEmailSender
    {
        void SendEmail(MensagemEmail message);
    }
}
