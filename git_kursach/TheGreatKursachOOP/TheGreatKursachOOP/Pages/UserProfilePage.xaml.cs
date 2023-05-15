using Microsoft.Maui.Controls;
using TheGreatKursachOOP.Services;
using TheGreatKursachOOP.Classes;
using System.Text.RegularExpressions;

namespace TheGreatKursachOOP.Pages;
//[QueryProperty(nameof(ClientId), "ID")]
public partial class UserProfilePage : ContentPage
{
	private string id;
    private DbManager dbmanager;
    private Card curCard = null;
    private string curLogin = null;
    public string ClientId
    {
        get
        {
            return this.id;
        }
        set
        {
            this.id = value;
        }

    }
    public UserProfilePage(string client_id)
	{
        this.id = client_id;
        dbmanager= new DbManager();
        
		InitializeComponent();
       
        Loaded += UserProfilePage_Loaded;
        
    }

   

    private void UserProfilePage_Loaded(object sender, EventArgs e)
    {
        List<Card> cards = dbmanager.GetCardsByUser(ClientId).ToList<Card>();
        curLogin = dbmanager.GetLoginByClient(ClientId);
        if (cards.Count != 0)
        {
            curCard = cards[0];
            balanceLabel.Text = "Current Ballance:   " + curCard.Balance.ToString();
            cardEntry.Text = curCard.Number;
        }
        loginEntry.Text= curLogin;
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();      
    }

    private async void onButton_Clicked(object sender, EventArgs e)
    {
        if(sender == backButton)
        {
            loginEntry.Text = curLogin;
            await Navigation.PopAsync();
        }
        else if (sender == logOutButton)
        {
            await Navigation.PopToRootAsync();
        }
        else if(sender == deleteButton)
        {

            if (dbmanager.GetDealsByClientId(ClientId).ToList<Deal>().Count == 0) 
            {
                dbmanager.RemoveCardByOwner(ClientId);
                dbmanager.RemoveFriendsByUser(ClientId);
                dbmanager.RemoveLogdataByUser(ClientId);
                dbmanager.RemoveNotificationsByReceiver(ClientId);
                dbmanager.RemoveJewelryByOwner(ClientId);


                dbmanager.RemoveClient(ClientId);
            }

            await Navigation.PopToRootAsync();
        }
        else if(sender == changeCardButton)
        {
            string number = cardEntry.Text;
            if (number != null && Regex.IsMatch(number, @"^\d{16}$"))
            {

                DateTime now = DateTime.Now;
                string cardid = "c" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString() +
                    (100 + now.Hour).ToString().Substring(1) + (now.Minute + 100).ToString().Substring(1) + (10000 + (new Random()).Next(1, 10000)).ToString().Substring(1);
                Card newcard = new Card(cardid, ClientId, number, (new Random()).Next(200, 701));
                if (dbmanager.GetCardsByUser(ClientId).ToList<Card>().Count == 0)
                {
                    dbmanager.AddCard(newcard);
                }
                else
                {
                    dbmanager.ChangeCard(newcard);
                }
                c_angryLabel.Text = "";
                curCard = newcard;
                balanceLabel.Text = "Current balance:   " + newcard.Balance.ToString();
            }
            else
            {
                c_angryLabel.Text = "Card number must contain 16 digits";
            }

        }
        else if(sender == changeLoginButton)
        {
            string login = loginEntry.Text;
            if (login != null && Regex.IsMatch(login, @"^[a-zA-Z]\w{4,19}$"))
            {
                if (!dbmanager.HasLogin(login))
                {
                    dbmanager.ChangeLogin(ClientId, login);
                    curLogin = login;
                    l_angryLabel.TextColor = Colors.LawnGreen;
                    l_angryLabel.Text = "login changed";
                    l_angryLabel.TextColor = Colors.Red;
                }
                else
                {
                    l_angryLabel.Text = "This login is already taken!";
                }
            }
            else
            {
                l_angryLabel.Text = "Login must contain 5-20 digits, letters, or _ (1st one is letter)";
            }
        }
        else if(sender == changePassswordButton)
        {
            string oldpassword = oldPasswordEntry.Text;
            string newpassword = newPasswordEntry.Text;
            string newpassword2 = newPasswordEntry2.Text;
            if (oldpassword != null && newpassword != null && newpassword2 != null && 
                Regex.IsMatch(oldpassword, @"^\w{8,20}$") && Regex.IsMatch(newpassword, @"^\w{8,20}$") && Regex.IsMatch(newpassword2, @"^\w{8,20}$"))
            {
               
                if (dbmanager.PasswordMatchesLogin(curLogin, oldpassword))
                {
                 
                    if(newpassword == newpassword2)
                    {
                        dbmanager.ChangePassword(ClientId, newpassword);
                        p_angryLabel.TextColor = Colors.LawnGreen;
                        p_angryLabel.Text = "password changed";
                        p_angryLabel.TextColor = Colors.Red;
                    }
                    else
                    {
                        p_angryLabel.Text = "New password repeated incorrectly!";
                    }
                    
                }
                else
                {
                    p_angryLabel.Text = "incorrect password!";
                }
                
            }
            else
            {
                p_angryLabel.Text = "Password must contain 8-20 digits, letters or _ symbols";
            }
        }
    }

    private void deleteButton_Pressed(object sender, EventArgs e)
    {
        if (dbmanager.GetDealsByClientId(ClientId).ToList<Deal>().Count != 0)
        {
            deleteAngryLabel.Text = "Cannot delete profile while having unresolved deals!";
        }
    }

    private void deleteButton_Released(object sender, EventArgs e)
    {
        deleteAngryLabel.Text = "";
    }
}