namespace ProductQRCodeReadWithDynamic.Services.Abstractions
{
    public interface IMailService
    {
        Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendMessageWithImageAsync(string[] tos, string subject, string body, bool isBodyHtml, string imagePath);
    }
}
