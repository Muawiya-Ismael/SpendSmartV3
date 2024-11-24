namespace SpendSmartV3.Objects.Views.Error
{
    public class ErrorViewModel: AView
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
