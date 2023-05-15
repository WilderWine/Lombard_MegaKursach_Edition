using TheGreatKursachOOP;
using System.Text.RegularExpressions;

using static System.Net.Mime.MediaTypeNames;
using TheGreatKursachOOP.Services;
using TheGreatKursachOOP.Classes;


namespace TheGreatKursachOOP.Pages;


public partial class LogInPage : ContentPage
{

    DbManager db;

    private bool EntriesAreCorrect()
    {
        return loginEntry.Text != null && Regex.IsMatch(loginEntry.Text, @"^[a-zA-Z]\w{4,19}$") &&
            passwordEntry.Text != null && Regex.IsMatch(passwordEntry.Text, @"^\w{8,20}$");
    }

	public LogInPage()
	{
        db = new DbManager();
		InitializeComponent();
       
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        
        base.OnNavigatedTo(args);
        List<Deal> deals = new List<Deal>(db.GetDeals().Where(d => d.Status == "confirmed"));
        foreach(Deal deal in deals)
        {
            if(deal.EndTerm < DateTime.Now)
            {
                db.ChangeDealStatus(deal.ID, "expired");
            }
        }
    }


    private void entryFocused(object sender, FocusEventArgs e)
    {

		if(sender == loginEntry)
		{
			loginEntry.PlaceholderColor = Colors.MediumPurple;
			loginEntry.TextColor = Colors.MediumPurple;
            loginEntry.BackgroundColor = Colors.DarkGray;
		}
		else if(sender == passwordEntry)
		{
            passwordEntry.PlaceholderColor = Colors.MediumPurple;
            passwordEntry.TextColor = Colors.MediumPurple;
            passwordEntry.BackgroundColor = Colors.DarkGray;
        }
    }
    private void entryUnfocused(object sender, FocusEventArgs e)
    {
        if (sender == loginEntry)
        {
            if(loginEntry.Text != null && Regex.IsMatch(loginEntry.Text, @"^[a-zA-Z]\w{4,19}$"))
            {
                loginEntry.PlaceholderColor = Colors.Gray;
                loginEntry.FontSize = 14;
                loginEntry.TextColor = (Color)App.CD["Colors"]["GoodPurple"];
                loginEntry.BackgroundColor = Colors.Black;
            }
            else
            {
                loginEntry.PlaceholderColor = Colors.Red;
                loginEntry.TextColor = Colors.Red;
                loginEntry.FontSize = 16;
                loginEntry.BackgroundColor = Colors.Black;
            }
        }
        else if (sender == passwordEntry)
        {
            if (passwordEntry.Text != null && Regex.IsMatch(passwordEntry.Text, @"^\w{8,20}$"))
            {
                passwordEntry.PlaceholderColor = Colors.Gray;
                passwordEntry.FontSize = 14;
                passwordEntry.TextColor = (Color)App.CD["Colors"]["GoodPurple"];
                passwordEntry.BackgroundColor = Colors.Black;
            }
            else
            {
                passwordEntry.FontSize= 16;
                passwordEntry.PlaceholderColor = Colors.Red;
                passwordEntry.TextColor = Colors.Red;
                passwordEntry.BackgroundColor = Colors.Black;
            }
            
        }
    }
    private void buttonPressed(object sender, EventArgs e)
    {
        if (sender == logInButton)
        {
            logInButton.BackgroundColor = (Color)App.CD["Colors"]["Gray600"];
        }
        else if (sender == signUpButton)
        {
            signUpButton.BackgroundColor = (Color)App.CD["Colors"]["Gray600"];
        }
    }
    private void buttonReleased(object sender, EventArgs e)
    {
        if (sender == logInButton)
        {
            logInButton.BackgroundColor = (Color)App.CD["Colors"]["Gray950"];
        }
        else if (sender == signUpButton)
        {
            signUpButton.BackgroundColor = (Color)App.CD["Colors"]["Gray950"];
        }
    }
    private async void onButtonClicked(object sender, EventArgs e)
    {
        if (sender == logInButton)
        {
            if (EntriesAreCorrect())
            {
                // проверка на админа. ≈сли админ, то переход на страницу админа
                // пытаемс€ найти пользовател€ с логином. если логин есть, провер€ем соответствие на пароль. ≈сли все норм, берем Id клиента и идем на страницу клиента

                if (loginEntry.Text.ToLower() == "admin4ik" && passwordEntry.Text == "admin123".ToLower())
                {
                    loginEntry.Text = "";
                    passwordEntry.Text = "";
                    await Navigation.PushAsync(new AdminBasePage());
                }
                else
                {
                    
                    if (db.HasLogin(loginEntry.Text))
                    {
                        if (db.PasswordMatchesLogin(loginEntry.Text, passwordEntry.Text))
                        {
                            string id = db.GetClientByLogin(loginEntry.Text);
                            loginEntry.Text = "";
                            passwordEntry.Text = "";

                            await Navigation.PushAsync(new ClientBasePage(id));
                        }
                        else
                        {
                            angryLabel.Text = "Incorrect password!";
                            angryLabel.Margin = new Thickness(50, -5, 0, 20);

                        }
                    }
                    else
                    {
                        angryLabel.Text = "No user with such login!";
                        angryLabel.Margin = new Thickness(50, -5, 0, 20);
                    }
                }
            }
            else
            {
                angryLabel.Text = "Invalid login and / or password pattern!";
                angryLabel.Margin = new Thickness(50, -5, 0, 20);
            }
        
        }
        else if (sender == signUpButton)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }

    private void entryTextChanged(object sender, TextChangedEventArgs e)
    {
        angryLabel.Margin = new Thickness(0, 0, 0, 0);
        angryLabel.Text = "";
    }
}