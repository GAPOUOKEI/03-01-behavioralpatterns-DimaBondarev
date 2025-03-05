using System;

namespace NotificationSender
{
    // Бондарев Дима 3пк2 Вар 5, задание 1: Напишите приложение, которое отправляет уведомления различными способами
    // (по электронной почте, SMS, через push-уведомления), используя шаблонный метод для общего алгоритма отправки.
    public abstract class NotificationSender
    {
        public void SendNotification(string recipient, string message)
        {
            if (!ValidateRecipient(recipient))
            {
                Console.WriteLine("Invalid recipient.");
                return;
            }

            PrepareMessage(message);
            DeliverMessage(recipient, message);
            LogDelivery(recipient, message);
        }

        protected abstract bool ValidateRecipient(string recipient);
        protected abstract void PrepareMessage(string message);
        protected abstract void DeliverMessage(string recipient, string message);

        private void LogDelivery(string recipient, string message)
        {
            Console.WriteLine($"Notification sent to {recipient}: {message}");
        }
    }

    public class EmailNotificationSender : NotificationSender
    {
        protected override bool ValidateRecipient(string recipient)
        {
            return recipient.Contains("@"); // Простая проверка email
        }

        protected override void PrepareMessage(string message)
        {
            Console.WriteLine("Formatting email content...");
        }

        protected override void DeliverMessage(string recipient, string message)
        {
            Console.WriteLine($"Sending email to {recipient}: {message}");
        }
    }

    public class SmsNotificationSender : NotificationSender
    {
        protected override bool ValidateRecipient(string recipient)
        {
            return recipient.Length == 10; // Простая проверка номера
        }

        protected override void PrepareMessage(string message)
        {
            Console.WriteLine("Trimming SMS message...");
        }

        protected override void DeliverMessage(string recipient, string message)
        {
            Console.WriteLine($"Sending SMS to {recipient}: {message}");
        }
    }

    public class PushNotificationSender : NotificationSender
    {
        protected override bool ValidateRecipient(string recipient)
        {
            return !string.IsNullOrEmpty(recipient);
        }

        protected override void PrepareMessage(string message)
        {
            Console.WriteLine("Encrypting push message...");
        }

        protected override void DeliverMessage(string recipient, string message)
        {
            Console.WriteLine($"Sending push notification to {recipient}: {message}");
        }
    }

    class Program
    {
        static void Main()
        {
            NotificationSender emailSender = new EmailNotificationSender();
            emailSender.SendNotification("user156@gmail.com", "Hello via Email!");

            NotificationSender smsSender = new SmsNotificationSender();
            smsSender.SendNotification("89225304565", "Hello via SMS!");

            NotificationSender pushSender = new PushNotificationSender();
            pushSender.SendNotification("device156", "Hello via Push!");
        }
    }
}
