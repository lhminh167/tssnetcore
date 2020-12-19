namespace Tss.Core.Interfaces
{
    public interface IEmail
    {
        string SenderEmailAddress { get; }
        string SenderName { get; }
        string RecipientEmailAddress { get; }
        string RecipientName { get; }
        string Subject { get; }
        string PlainTextContent { get; }
        string HtmlContent { get; }
    }
}
