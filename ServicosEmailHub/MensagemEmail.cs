namespace ServicosEmailHub
{
    public class MensagemEmail
    {
        public MensagemEmail(string from, string to, string displayname, string subject, string content)
        {
            From = from;
            To = to;
            Displayname = displayname;
            Subject = subject;
            Content = content;
        }

        public string From { get; private set; }
        public string To { get; private set; }
        public string Displayname { get; private set; }
        public string Subject { get; private set; }
        public string Content { get; private set; }







    }
}
