namespace EDSCore
{
    public interface IServiceNotificationContext
    {
        bool HasErrorNotifications { get; }
        void NotifyError(string message);
        void NotifySuccess(string message);
        List<ValidationFalha> GetErrorNotifications();
    }
}
