using Infra.CrossCutting.Util.Notifications.Interface;
using NotificationsModel = Infra.CrossCutting.Util.Notifications.Model.Notifications;

namespace Infra.CrossCutting.Util.Notifications.Implementation;

public class Notify : INotify
{
    private List<NotificationsModel> _notifications;

    public Notify(List<NotificationsModel> notifications)
    {
        _notifications = new List<NotificationsModel>();
    }

    public IEnumerable<NotificationsModel> GetNotifications()
    {
        return _notifications.Where(not => not.GetType() == typeof(NotificationsModel)).ToList();
    }

    public bool HasNotifications()
    {
        return GetNotifications().Any();
    }

    public void NewNotification(string key, string message)
    {
        _notifications.Add(new NotificationsModel(key, message));
    }
}