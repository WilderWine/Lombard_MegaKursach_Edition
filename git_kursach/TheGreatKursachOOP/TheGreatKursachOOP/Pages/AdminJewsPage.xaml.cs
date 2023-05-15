using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;
using System.Collections.ObjectModel;


namespace TheGreatKursachOOP.Pages;

public partial class AdminJewsPage : ContentPage
{
	
    DbManager dbManager;
   
    ObservableCollection<Jewelry> jewelries;
    public AdminJewsPage()
    {
        dbManager= new DbManager();
        jewelries = new ObservableCollection<Jewelry>(dbManager.GetJewelriesByOwnerId("uadmin"));
        Loaded += AdminJewsPage_Loaded;
        InitializeComponent();
    }

    private void AdminJewsPage_Loaded(object sender, EventArgs e)
    {
        jewsView.ItemsSource = jewelries;
        balanceLabel.Text = "Current balance:  " + dbManager.GetCardsByUser("uadmin")[0].Balance.ToString();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (sender == cancelButton)
        {
            jewsView.SelectedItems.Clear();
        }
        else if (sender == sellButton)
        {

            ObservableCollection<Jewelry> temp = new ObservableCollection<Jewelry>();
            foreach (Jewelry jewelry in jewelries)
            {
                temp.Add(jewelry);
            }
            foreach (Jewelry jewelry in jewsView.SelectedItems)
            {
                dbManager.GetMoneyFromAir(dbManager.GetCardsByUser("uadmin")[0].ID, (new Random().Next(800, 1500)));
                dbManager.RemoveJewelry(jewelry.ID);
                temp.Remove(jewelry);
            }
            jewelries = temp;
            jewsView.ItemsSource = jewelries;
            balanceLabel.Text = "Current balance:  " + dbManager.GetCardsByUser("uadmin")[0].Balance.ToString();
        }
        else if (sender == backButton)
        {

            await Navigation.PopAsync();
        }
    }
}