using System.Collections.ObjectModel;
using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Pages;

[QueryProperty(nameof(ClientId), "ID")]
public partial class NotificationsPage : ContentPage
{
	private string id;
    private DbManager dbManager;
    ObservableCollection<Notification> notifications;
    public string ClientId
    {
        get
        {
            return this.id;
        }
    }
    public NotificationsPage(string client_id)
	{
        id = client_id;
        dbManager= new DbManager();
        Loaded += NotificationsPage_Loaded;
		InitializeComponent();
       
	}



    private void NotificationsPage_Loaded(object sender, EventArgs e)
    {
        notifications = new ObservableCollection<Notification>(dbManager.GetNotificationsByReceiver(ClientId));
        notifsView.ItemsSource = notifications;
        foreach(Notification notification in notifications)
        {
            dbManager.ChangeNotificationToRead(notification.ID);
        }
    }
   
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
   
    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (sender == cancelButton)
        {
            notifsView.SelectedItems.Clear();
        }
        else if (sender == deleteButton)
        {

            ObservableCollection<Notification> temp = new ObservableCollection<Notification>();
            foreach (Notification notification in notifications)
            {
                temp.Add(notification);
            }
            foreach (Notification notification in notifsView.SelectedItems)
            {
                dbManager.RemoveNotification(notification.ID);
                temp.Remove(notification);
            }
            notifications = temp;
            notifsView.ItemsSource = notifications;
        }
        else if (sender == backButton)
        {
            await Navigation.PopAsync();
        }
        else if (sender == updateButton)
        {
            notifications = new ObservableCollection<Notification>(dbManager.GetNotificationsByReceiver(ClientId));
            notifsView.ItemsSource = notifications;
        }
    }
}