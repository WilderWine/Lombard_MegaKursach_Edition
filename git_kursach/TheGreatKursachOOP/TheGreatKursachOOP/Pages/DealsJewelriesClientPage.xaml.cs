using System.Collections.ObjectModel;
using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Pages;

public partial class DealsJewelriesClientPage : ContentPage
{
    private string id;
    DbManager dbManager;
    ObservableCollection<Jewelry> jewelries;
    ObservableCollection<Deal> deals;
    private string ClientId
    {
        get { return id; }
    }
	public DealsJewelriesClientPage(string client_id)
	{
        this.id = client_id;
        dbManager= new DbManager();
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
        allJewRB.IsChecked = true;
        dealsCW.SelectionMode = SelectionMode.None;
        jewsCW.SelectionMode = SelectionMode.None;

        deals = new ObservableCollection<Deal>(dbManager.GetDealsByClientId(ClientId));
        jewelries = new ObservableCollection<Jewelry>(dbManager.GetJewelriesByOwnerId(ClientId));

        dealsCW.ItemsSource = deals;
        jewsCW.ItemsSource = jewelries;
    }

    private void onButton_Clicked(object sender, EventArgs e)
    {
		if(sender == deleteButton)
		{
            if(!allJewRB.IsChecked && jewsCW.SelectedItems.Count > 0)
            {
                ObservableCollection<Jewelry> temp = new ObservableCollection<Jewelry>();
                foreach (Jewelry jewelry in jewelries)
                {
                    temp.Add(jewelry);
                }
                foreach (Jewelry jewelry in jewsCW.SelectedItems)
                {
                    dbManager.RemoveJewelry(jewelry.ID);
                    temp.Remove(jewelry);
                }
                jewelries = temp;
                jewsCW.ItemsSource = jewelries;
            }
		}
		else if(sender == handoverButton)
		{
            if (!allJewRB.IsChecked && jewsCW.SelectedItems.Count > 0)
            {
                ObservableCollection<Jewelry> temp = new ObservableCollection<Jewelry>();
                foreach (Jewelry jewelry in jewelries)
                {
                    temp.Add(jewelry);
                }
                DateTime now = DateTime.Now;
                foreach (Jewelry jewelry in jewsCW.SelectedItems)
                {
                    dbManager.ChangeJewelryStatus(jewelry.ID, "offered");

                    string id = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                        + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                    string message = $"User {ClientId}, offered new thing with id {jewelry.ID}  {DateTime.Now.ToString()}";
                    dbManager.AddNotification(new Notification(id, ClientId, "uadmin", message, 0));

                    temp.Remove(jewelry);
                }
                jewelries = temp;
                jewsCW.ItemsSource = jewelries;
            }
        }
    }

    private void RB_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        dealsCW.SelectedItem = null;
        if (allRB.IsChecked)
        {
            dealsCW.SelectedItems.Clear();
            dealsCW.SelectionMode = SelectionMode.None;
            deals = new ObservableCollection<Deal>(dbManager.GetDealsByClientId(ClientId));
            dealsCW.ItemsSource = deals;
        }
        else if (offeredRB.IsChecked)
        {
            dealsCW.SelectionMode = SelectionMode.Single;
            deals = new ObservableCollection<Deal>(dbManager.GetDealsByClientId(ClientId).Where(d => d.Status == "offered"));
            dealsCW.ItemsSource = deals;
        }
        else if (confirmedRB.IsChecked)
        {
            dealsCW.SelectionMode = SelectionMode.Single;
            deals = new ObservableCollection<Deal>(dbManager.GetDealsByClientId(ClientId).Where(d => d.Status == "confirmed"));
            dealsCW.ItemsSource = deals;
        }
    }

    private void jewRB_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (allJewRB.IsChecked)
        {
            deleteButton.IsEnabled = false;
            handoverButton.IsEnabled = false;
            jewsCW.SelectedItems.Clear();
            jewsCW.SelectionMode = SelectionMode.None;
            jewelries = new ObservableCollection<Jewelry>(dbManager.GetJewelriesByOwnerId(ClientId));
            jewsCW.ItemsSource = jewelries;
        }
        else if(addedJewRB.IsChecked)
        {
            deleteButton.IsEnabled= true;
            handoverButton.IsEnabled = true;
            jewsCW.SelectionMode = SelectionMode.Multiple;
            jewelries = new ObservableCollection<Jewelry>(dbManager.GetJewelriesByOwnerId(ClientId).Where(j => j.Status == "added"));
            jewsCW.ItemsSource = jewelries;
        }
    }

    private async void dealsCW_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dealsCW.SelectedItem!= null)
        {
            Deal deal = dealsCW.SelectedItem as Deal;
            
            await Navigation.PushAsync(new DealPage(deal));
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}