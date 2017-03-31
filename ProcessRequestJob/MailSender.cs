using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using EasyTicket.SharedResources.Models.Tables;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ProcessRequestJob {
    public static class MailSender {
        private const string ApiKeyDecoded = "U0cuVzVHUkVPdTRROU9QMm9waERqYk85US5wSTB0RVZJUmUxUS1aZ0ZRaVFua3V6YURlOHppbHFUdlE1dE5fYTFWUWhZ";
        private const string AppName = "EasyTicket";

        public static async Task<Response> Send(Request request, string token, string sessionId) {
            string apiKey = Base64Decode(ApiKeyDecoded);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress($"{AppName}@gmail.com", AppName);
            string subject = $"Ваш билет в {request.StationToTitle} найден!";
            var to = new EmailAddress(request.PassangerEmail, request.PassangerName);
            string body = FormatMailBody(request, token, sessionId);
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            return await client.SendEmailAsync(msg);
        }

        private static string FormatMailBody(Request request, string token, string sessionId) {
            return $"<p>Уважаемый {request.PassangerSurname} {request.PassangerName}, " +
                   $@"Ваш билет на {FormatDate(request.DateTime)} со станции ""{request.StationFromTitle}"" до станции ""{request.StationToTitle}"" найден.</p>" +
                   $@"<p>Пожалуйста, пройдите по ссылке <a href=""http://easyticket.azurewebsites.net/reservation/{token}"">Бронирование</a> что бы совершить покупку</p>" +
                   $@"<p>Сессия {sessionId}</p>" +
                    "</br>" + 
                    "<p>С уважением,</br>" +
                    $" Ваш {AppName}</p>";
        }

        private static string FormatDate(DateTime dateTime) {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            try {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
                return dateTime.ToLongDateString();
            }
            finally {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }
        }

        public static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData) {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}