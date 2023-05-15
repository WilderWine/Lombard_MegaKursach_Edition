using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;
using System.Text.RegularExpressions;

namespace TheGreatKursachOOP.Pages;

public partial class AdminMakeDealPage : ContentPage
{
	private Jewelry jew;
    private Client client;
	DbManager dbManager;
	string Image { get { return jew.Image;} }


    public AdminMakeDealPage(Jewelry jewelry_for_deal)
	{
		jew = jewelry_for_deal;
		dbManager = new DbManager();
        client = dbManager.GetClientById(jewelry_for_deal.OwnerId);
		InitializeComponent();
		
        Loaded += AdminMakeDealPage_Loaded;
	}

    private void AdminMakeDealPage_Loaded(object sender, EventArgs e)
    {
        jewImage.Source = Image;
        jewNameLabel.Text = $"    Name:  {jew.Name}";
        jewIdLabel.Text = $"    Id:  {jew.ID}";
        clientIdLabel.Text = $"    Id:   {client.ID}";
        clientNameLabel.Text = $"    Name:  {client.Name}";
        clientSurnameLabel.Text = $"    Surname:  {client.Surname}";
        clientFathernameLabel.Text = $"    Fatherame:  {client.FatherName}";
        clientPassLabel.Text = $"    Pass:  {client.Pass}";
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }


    private bool CanMakeDeal()
    {
        return (LoanEntryCorrect() && PercentageEntryCorrect() && DaysEntryCorrect() && HoursEntryCorrect() && MinutesEntryCorrect() &&
            terminAngryLabel.Text == "" && loanAngryLabel.Text == "" && percentageAngryLabel.Text == "");
    }


    private async void onButton_Clicked(object sender, EventArgs e)
    {
        if(sender == backButton)
        {
            await Navigation.PopAsync();
        }
        else if(sender == saveButton)
        {
            if (CanMakeDeal())
            {
                string termin = daysEntry.Text + " " + hoursEntry.Text + " " + minsEntry.Text;

                DateTime now = DateTime.Now;
                string id = "d" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
                   + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                double loan = Double.Parse(loanEntry.Text);
                double totalDebt = loan + loan / 100 * Double.Parse(percentageEntry.Text);

                dbManager.AddDeal(new Deal(id, client.ID, jew.ID, jew.Image, loan, totalDebt, termin, "offered"));
                dbManager.ChangeJewelryStatus(jew.ID, "handed");

                string noti_id = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString()
               + (100 + now.Hour).ToString().Substring(1) + (100 + now.Minute).ToString().Substring(1) + (10000 + new Random().Next(1, 10000)).ToString().Substring(1);
                string message = $"New deal was offered for your product  {jew.Name} (id = {jew.ID}). Termin termins will be set as soon as you confirm it. You can deny the deal if you do not like the conditions offered.";

                dbManager.AddNotification(new Notification(noti_id, "uadmin", client.ID, message, 0));
                await Navigation.PopAsync();
            }
        }
    }
    
    private bool LoanEntryCorrect()
    {
        string loan = loanEntry.Text;
        if (loan != null &&Regex.IsMatch(loan, @"^\d{2,3}$"))
        {
            if(int.Parse(loan) >= 50 && int.Parse(loan) <= 500){
                return true;
            }
            return false;
        }
        return false;
    }

    private bool PercentageEntryCorrect()
    {
        string percentage = percentageEntry.Text;
        if (percentage != null && Regex.IsMatch(percentage, @"^\d{1,3}"))
        {
            if (int.Parse(percentage) >= 10 && int.Parse(percentage) <= 200){
                return true;
            }
            return false;
        }
        return false;
    }

    private bool DaysEntryCorrect()
    {
        string days = daysEntry.Text;
        if (days != null && Regex.IsMatch(days, @"^\d{1,2}"))
        {
            if (int.Parse(days) >= 0 && int.Parse(days) <= 15)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private bool HoursEntryCorrect()
    {
        string hours = hoursEntry.Text;
        if (hours != null && Regex.IsMatch(hours, @"^\d{1,2}"))
        {
            if (int.Parse(hours) >= 0 && int.Parse(hours) <= 23)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private bool MinutesEntryCorrect()
    {
        string mins = minsEntry.Text;
        if (mins != null && Regex.IsMatch(mins, @"^\d{1,2}"))
        {
            if (int.Parse(mins) >= 0 && int.Parse(mins) <= 59)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private void entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender == loanEntry || sender == percentageEntry)
        {
            if (LoanEntryCorrect() && PercentageEntryCorrect())
            {
                loanAngryLabel.Text = "";
                percentageAngryLabel.Text = "";
                double totalDebt = Double.Parse(loanEntry.Text) + Double.Parse(loanEntry.Text) * Double.Parse(percentageEntry.Text) / 100;
                totalDebt = Math.Round(totalDebt, 2);
                totalPaymentLabel.Text = $"Debt expected:   {totalDebt}";
            }
            else
            {
                if (!LoanEntryCorrect())
                {
                    loanAngryLabel.Text = "Loan value must be integer from 50 to 500";
                }
                else
                {
                    loanAngryLabel.Text = "";
                }
                if (!PercentageEntryCorrect())
                {
                    percentageAngryLabel.Text = "Percentage value must be integer from 10 to 200";
                }
            }
        }
        else if (sender == daysEntry || sender == hoursEntry || sender == minsEntry)
        {
            if (DaysEntryCorrect() && HoursEntryCorrect() && MinutesEntryCorrect())
            { 
                if (daysEntry.Text != "0" || hoursEntry.Text != "0" || minsEntry.Text != "0")
                {
                    terminAngryLabel.Text = "";
                }
                else terminAngryLabel.Text = "at leas one field must be more than 0";
            }
            else
            {
                if (!DaysEntryCorrect())
                {
                    terminAngryLabel.Text = "days value must be integer from 0 to 15";
                }
                else if (!HoursEntryCorrect())
                {
                    terminAngryLabel.Text = "hours value must be integer from 0 to 23";
                }
                else if (!MinutesEntryCorrect())
                {
                    terminAngryLabel.Text = "minutes value must be integer from 0 to 59";
                }
            }
        }
    }
}