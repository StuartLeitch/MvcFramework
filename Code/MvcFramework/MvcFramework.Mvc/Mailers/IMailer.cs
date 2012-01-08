using System.Net.Mail;

namespace MvcFramework.Mvc.Mailers
{
    public interface IMailer {
        MailMessage GetMailMessageToSend(Mailer.MessageType messageType, string subject, object model, Mailer.RecipientModel recipientModel);
    }
}