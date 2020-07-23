namespace EC_Website.Web.ViewModels
{
    public enum AlertType
    {
        Success,
        Error,
        Warning,
        Info,
        Primary
    }

    public class AlertBoxViewModel
    {
        public AlertBoxViewModel(string message)
        {
            Message = message;
        }

        public AlertType AlertType { get; set; } = AlertType.Success;
        public string Message { get; set; }
        public bool IsDismissible { get; set; } = true;
    }
}
