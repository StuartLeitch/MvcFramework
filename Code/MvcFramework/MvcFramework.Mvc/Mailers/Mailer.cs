using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Mailer;
using System.Net.Mail;
using MvcFramework.Biz.Data.EntityFramework;

namespace MvcFramework.Mvc.Mailers
{ 
    public class Mailer : MailerBase, IMailer
    {
		public Mailer():
			base()
		{
			MasterName="_Layout";
		}

        public class RecipientModel
        {
            public string FullName { get; set; }
            public string Email { get; set; }
        }

        /// <summary>
        /// Note: These map onto the names of the cshtml mailers!
        /// </summary>
        public enum MessageType
        {
            SomeNameGoesHere,
        }

        public virtual MailMessage GetMailMessageToSend(MessageType messageType, string subject, object model, RecipientModel recipientModel)
		{
			var mailMessage = new MailMessage{Subject = subject};
            var mailAddress = new MailAddress(recipientModel.Email, recipientModel.FullName);
            mailMessage.To.Add(mailAddress);
            
            this.ViewData = new ViewDataDictionary(model);
            PopulateBody(mailMessage, viewName: messageType.ToString());

			return mailMessage;
		}
	}
}