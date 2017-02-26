using System.Threading.Tasks;
using EasyTicket.SharedResources.Models.Tables;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ProcessRequestJob {
    public static class MailSender {
        public static async Task<Response> Send(Request request) {
            var client = new SendGridClient("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");
            var from = new EmailAddress("test@example.com", "Example User");
            string subject = "EasyTicket. Found your ticket!";
            var to = new EmailAddress(request.PassangerEmail, request.PassangerName);
            string text = request.ToString();
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, text, text);
            return await client.SendEmailAsync(msg);
        }
    }
}