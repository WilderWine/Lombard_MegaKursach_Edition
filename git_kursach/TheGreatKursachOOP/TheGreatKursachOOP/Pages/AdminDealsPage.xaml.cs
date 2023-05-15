using TheGreatKursachOOP.Services;
using TheGreatKursachOOP.Classes;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace TheGreatKursachOOP.Pages;

public partial class AdminDealsPage : ContentPage
{
	DbManager dbManager = new DbManager();
	ObservableCollection<Deal> deals;
    ObservableCollection<Jewelry> jewelries;
   
    public AdminDealsPage()
	{
		dbManager = new DbManager();
        InitializeComponent();
        
        
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        List<Deal> temp_deals = new List<Deal>(dbManager.GetDeals().Where(d => d.Status == "confirmed"));
        foreach (Deal deal in temp_deals)
        {
            if (deal.EndTerm < DateTime.Now)
            {
                dbManager.ChangeDealStatus(deal.ID, "expired");
            }
        }

        allRB.IsChecked = true;
        deals = new ObservableCollection<Deal>(dbManager.GetDeals());
        jewelries = new ObservableCollection<Jewelry>(dbManager.GetJewelriesByStatus("offered"));
        dealsCW.ItemsSource = deals;
        jewsCW.ItemsSource = jewelries;
    }

  

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }

    private void rb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        dealsCW.SelectedItem = null;
        if (allRB.IsChecked)
        {
            ConfiscateButton.IsEnabled= false;
            dealsCW.SelectionMode= SelectionMode.None;
            deals = new ObservableCollection<Deal>(dbManager.GetDeals());
            dealsCW.ItemsSource=deals;
        }
        else if(confirmedRB.IsChecked)
        {
            ConfiscateButton.IsEnabled = false;
            dealsCW.SelectionMode = SelectionMode.Multiple;
            deals = new ObservableCollection<Deal>(dbManager.GetDealsByStatus("confirmed"));
            dealsCW.ItemsSource=deals;
        }
        else if (expiredRB.IsChecked)
        {
            ConfiscateButton.IsEnabled = true;
            dealsCW.SelectionMode = SelectionMode.Multiple;
            deals = new ObservableCollection<Deal>(dbManager.GetDealsByStatus("expired"));
            dealsCW.ItemsSource = deals;
        }
    }

    private void onButton_Clicked(object sender, EventArgs e)
    {
        if(sender == ConfiscateButton)
        {
            if (dealsCW.SelectedItems == null) return;


            ObservableCollection<Deal> temp = new ObservableCollection<Deal>();
            foreach (Deal deal in deals)
            {
                temp.Add(deal);
            }
            foreach (Deal deal in dealsCW.SelectedItems)
            {

                Jewelry jew = dbManager.GetJewelryById(deal.JewelryId);
                dbManager.ChangeDealStatus(deal.ID, "closed");
                dbManager.ChangeJewelryOwner(jew.ID, "uadmin");


                
                temp.Remove(deal);

                DateTime now = DateTime.Now;
                string id = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                            + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                string message = $"You have not paid your debt so your product {jew.Name} was taken forever {DateTime.Now.ToString()}";
                dbManager.AddNotification(new Notification(id, jew.OwnerId, "uadmin", message, 0));

            }
            deals = temp;
            dealsCW.ItemsSource = deals;

           

        }
        else if(sender == rejectButton)
        {
            Jewelry jew = (jewsCW.SelectedItem as Jewelry);
            if (jew != null)
            {
                dbManager.ChangeJewelryStatus(jew.ID, "added");
                jewsCW.SelectedItems.Clear();
                jewelries.Remove(jew);

                DateTime now = DateTime.Now;
                string id = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                    + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                string message = $" Your product {jew.ID} -- {jew.Name} was rejected by the administration  {DateTime.Now.ToString()}. You can delete it now or try to offer it later.";
                dbManager.AddNotification(new Notification(id, "uadmin", jew.OwnerId, message, 0));



            }
          
        }
        else if(sender == makeDealButton)
        {
            Jewelry jew = (jewsCW.SelectedItem as Jewelry);
            if (jew != null)
            {
                Navigation.PushAsync(new AdminMakeDealPage(jew));
            }
        }
    }
}