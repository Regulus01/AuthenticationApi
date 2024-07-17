namespace Infra.CrossCutting.Util.Notifications.Interface;

using NotificationModel = Model.Notifications;

public interface INotify
{
    bool HasNotifications();
    IEnumerable<NotificationModel> GetNotifications();
    void NewNotification(string key, string message);
}