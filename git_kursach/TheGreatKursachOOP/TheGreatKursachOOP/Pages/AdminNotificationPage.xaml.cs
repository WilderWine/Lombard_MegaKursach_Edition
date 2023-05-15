using TheGreatKursachOOP.Classes;
using System.Collections.ObjectModel;
using TheGreatKursachOOP.Services;


namespace TheGreatKursachOOP.Pages;

public partial class AdminNotificationPage : ContentPage
{
	DbManager dbManager;
	ObservableCollection<Notification> notifications;
	public AdminNotificationPage()
	{
		dbManager = new DbManager();

		notifications = new ObservableCollection<Notification>(dbManager.GetNotificationsByReceiver("uadmin"));
		InitializeComponent();
        Loaded += AdminNotificationPage_Loaded;
      
	}


    private void AdminNotificationPage_Loaded(object sender, EventArgs e)
    {
        notifications = new ObservableCollection<Notification>(dbManager.GetNotificationsByReceiver("uadmin"));
        notifsView.ItemsSource = notifications;
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if(sender == cancelButton)
        {
            notifsView.SelectedItems.Clear();
        }
        else if(sender == deleteButton)
        {

            ObservableCollection<Notification> temp = new ObservableCollection<Notification>();
            foreach(Notification notification in notifications)
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
        else if(sender == backButton)
        {
      
            await Navigation.PopAsync();
        }
        else if(sender == updateButton)
        {
            notifications = new ObservableCollection<Notification>(dbManager.GetNotificationsByReceiver("uadmin"));
            notifsView.ItemsSource = notifications;
        }
    }
}