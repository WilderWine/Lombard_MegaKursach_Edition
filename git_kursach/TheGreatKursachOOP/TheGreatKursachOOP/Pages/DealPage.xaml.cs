using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Pages;

public partial class DealPage : ContentPage
{
	private Deal deal;
    private Jewelry jew;
    private DbManager dbManager;
    string[] term;

    public DealPage(Deal deal)
	{
        dbManager= new DbManager();
		this.deal = deal;
        term = deal.Termin.Split(' ');
        jew = dbManager.GetJewelryById(deal.JewelryId);
		InitializeComponent();
        Loaded += DealPage_Loaded;
	}

    private void DealPage_Loaded(object sender, EventArgs e)
    {
        jewImage.Source=jew.Image;
        jewNameLabel.Text = $"Name:  {jew.Name}";
        dealTerminLabel.Text = $"Termin:  {term[0]}d {term[1]}h {term[2]}min";
        dealLoanLabel.Text = $"Loan:  {deal.GivenMoney}";
        dealDebtLabel.Text = $"Debt:  {deal.WantedMoney}";
        startTermLabel.Text = (deal.StartTerm == null) ?"Start termin will be set as soon as you confirm the deal" : deal.StartTerm.ToString();
        endTermLabel.Text = (deal.EndTerm == null) ? "End termin will be set as soon as you confirm the deal" : deal.EndTerm.ToString();
        confirm_payButton.Text = (deal.Status == "offered") ? "confirm" : "pay debt";
        reject_leaveButton.Text = (deal.Status == "offered") ? "reject" : "leave product";
    }

    private bool ClientHasCard()
    {
        return (dbManager.GetCardsByUser(deal.ClientId).Count > 0);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        
    }

    private  void onButton_Clicked(object sender, EventArgs e)
    {
        if(sender == backButton)
        {
             Navigation.PopAsync();
        }
        else if(sender == confirm_payButton)
        {
            if(deal.Status == "offered")
            {
                if (ClientHasCard())
                {
                    DateTime now= DateTime.Now;
                    DateTime endterm = now;
                    endterm = endterm.AddDays(int.Parse(term[0]));
                    endterm = endterm.AddHours(int.Parse(term[1]));
                    endterm = endterm.AddMinutes(int.Parse(term[2]));
                    deal.StartTerm = now;
                    deal.EndTerm = endterm;
                    dbManager.ChangeDealStatusTerms(deal.ID, "confirmed", now, endterm);
                 
                    deal.Status = "confirmed";
                    dbManager.TransferMoney( dbManager.GetCardsByUser("uadmin")[0].ID, dbManager.GetCardsByUser(deal.ClientId)[0].ID, deal.GivenMoney);

                    string id = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                        + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                    string message = $"User {deal.ClientId} confirmed deal id={deal.ID} for product {jew.Name}  {DateTime.Now.ToString()}";
                    dbManager.AddNotification(new Notification(id, deal.ClientId, "uadmin", message, 0));

                    startTermLabel.Text =  deal.StartTerm.ToString();
                    endTermLabel.Text =  deal.EndTerm.ToString();
                    confirm_payButton.Text =  "pay debt";
                    reject_leaveButton.Text = "leave product";
                }
                else angryLabel.Text = "You must add card before you are able confirm deals";
            }
            else
            {
                if (ClientHasCard())
                {
                    double balance = dbManager.GetCardsByUser(deal.ClientId)[0].Balance;
                    if (balance >= deal.WantedMoney)
                    {
                        dbManager.TransferMoney(dbManager.GetCardsByUser(deal.ClientId)[0].ID, dbManager.GetCardsByUser("uadmin")[0].ID, deal.WantedMoney);
                        dbManager.ChangeDealStatus(deal.ID, "closed");
                        dbManager.ChangeJewelryStatus(jew.ID, "added");

                        DateTime now = DateTime.Now;
                        string id_1 = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                            + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                        string message = $"User {deal.ClientId} paid his debd and closed the deal. {DateTime.Now.ToString()}    Income: {deal.WantedMoney}";
                        dbManager.AddNotification(new Notification(id_1, deal.ClientId, "uadmin", message, 0));


                        string id_2 = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                            + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                        message = $"You paid your debt and now we return you following product: {jew.Name}";
                        dbManager.AddNotification(new Notification(id_2, "uadmin", deal.ClientId, message, 0));

                        Navigation.PopAsync();

                    }
                    else angryLabel.Text = "You do not have enough money on your card";
                }
                else angryLabel.Text = "You cannot pay without card added";
            }
        }
        else if(sender == reject_leaveButton)
        {
            if(deal.Status == "offered")
            {
                dbManager.RemoveDeal(deal.ID);
                dbManager.ChangeJewelryStatus(jew.ID, "offered");

                DateTime now = DateTime.Now;
                string id_1 = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                    + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                string message = $"User {deal.ClientId} rejected the deal for product {jew.Name}. {DateTime.Now.ToString()}";
                dbManager.AddNotification(new Notification(id_1, deal.ClientId, "uadmin", message, 0));


                 Navigation.PopAsync();
            }
            else
            {
                dbManager.ChangeDealStatus(deal.ID, "expired");
                dbManager.ChangeJewelryOwner(jew.ID, "uadmin");


                DateTime now = DateTime.Now;
                string id_1 = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                    + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                string message = $"You did not pay your debd, so your product {jew.Name} was teken forever. {DateTime.Now.ToString()}";
                dbManager.AddNotification(new Notification(id_1,  "uadmin", deal.ClientId, message, 0));


                 Navigation.PopAsync();
            }
        }
    }
}