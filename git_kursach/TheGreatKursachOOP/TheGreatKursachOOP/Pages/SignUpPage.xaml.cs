using System.Text.RegularExpressions;
using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Pages;

public partial class SignUpPage : ContentPage
{

    private bool EntriesAreCorrect()
    {
        return loginEntry.Text != null && Regex.IsMatch(loginEntry.Text, @"^[a-zA-Z]\w{4,19}$") &&
            passwordEntry.Text != null && Regex.IsMatch(passwordEntry.Text, @"^\w{8,20}$") &&
            nameEntry.Text != null && Regex.IsMatch(nameEntry.Text, @"^[A-Z][a-z]{0,19}$") &&
            surnameEntry.Text != null && Regex.IsMatch(surnameEntry.Text, @"^[A-Z][a-z]{0,19}$") &&
            fathernameEntry.Text != null && Regex.IsMatch(fathernameEntry.Text, @"^[A-Z][a-z]{0,19}$") &&
            passEntry.Text != null && Regex.IsMatch(passEntry.Text, @"^[A-Z]{2}\d{7}$");
    }

    public SignUpPage()
	{
		InitializeComponent();
	}

    private void entryFocused(object sender, FocusEventArgs e)
    {
        angryLabel.TextColor = Colors.Red;
        angryLabel.Text = "";
        angryLabel.Margin = new Thickness(0, 0, 0, 0);


        if (sender == loginEntry)
        {
            loginEntry.PlaceholderColor = Colors.MediumPurple;
            loginEntry.TextColor = Colors.MediumPurple;
            loginEntry.BackgroundColor = Colors.DarkGray;
        }
        else if (sender == passwordEntry)
        {
            passwordEntry.PlaceholderColor = Colors.MediumPurple;
            passwordEntry.TextColor = Colors.MediumPurple;
            passwordEntry.BackgroundColor = Colors.DarkGray;
        }
        else if (sender == nameEntry)
        {
            nameEntry.PlaceholderColor = Colors.MediumPurple;
            nameEntry.TextColor = Colors.MediumPurple;
            nameEntry.BackgroundColor = Colors.DarkGray;
        }
        else if (sender == surnameEntry)
        {
            surnameEntry.PlaceholderColor = Colors.MediumPurple;
            surnameEntry.TextColor = Colors.MediumPurple;
            surnameEntry.BackgroundColor = Colors.DarkGray;
        }
        else if (sender == fathernameEntry)
        {
            fathernameEntry.PlaceholderColor = Colors.MediumPurple;
            fathernameEntry.TextColor = Colors.MediumPurple;
            fathernameEntry.BackgroundColor = Colors.DarkGray;
        }
        else if (sender == passEntry)
        {
            passEntry.PlaceholderColor = Colors.MediumPurple;
            passEntry.TextColor = Colors.MediumPurple;
            passEntry.BackgroundColor = Colors.DarkGray;
        }
        else if (sender == codewordEntry)
        {
            codewordEntry.PlaceholderColor = Colors.MediumPurple;
            codewordEntry.TextColor = Colors.MediumPurple;
            codewordEntry.BackgroundColor = Colors.DarkGray;
        }
    }
    private void entryUnfocused(object sender, FocusEventArgs e)
    {
        if (sender == nameEntry)
        {
            if (nameEntry.Text != null && Regex.IsMatch(nameEntry.Text, @"^[A-Z][a-z]{0,19}$"))
            {
                nameEntry.PlaceholderColor = Colors.Gray;
                nameEntry.FontSize = 14;
                nameEntry.TextColor = (Color)App.CD["Colors"]["GoodPurple"];
                nameEntry.BackgroundColor = Colors.Black;
            }
            else
            {
                nameEntry.PlaceholderColor = Colors.Red;
                nameEntry.TextColor = Colors.Red;
                nameEntry.FontSize = 16;
                nameEntry.BackgroundColor = Colors.Black;
            }
        }
        else if (sender == surnameEntry)
        {
            if (surnameEntry.Text != null && Regex.IsMatch(surnameEntry.Text, @"^[A-Z][a-z]{0,19}$"))
            {
                surnameEntry.PlaceholderColor = Colors.Gray;
                surnameEntry.FontSize = 14;
                surnameEntry.TextColor = (Color)App.CD["Colors"]["GoodPurple"];
                surnameEntry.BackgroundColor = Colors.Black;
            }
            else
            {
                surnameEntry.FontSize = 16;
                surnameEntry.PlaceholderColor = Colors.Red;
                surnameEntry.TextColor = Colors.Red;
                surnameEntry.BackgroundColor = Colors.Black;
            }

        }
        else if (sender == fathernameEntry)
        {
            if (fathernameEntry.Text != null && Regex.IsMatch(fathernameEntry.Text, @"^[A-Z][a-z]{0,19}$"))
            {
                fathernameEntry.PlaceholderColor = Colors.Gray;
                fathernameEntry.FontSize = 14;
                fathernameEntry.TextColor = (Color)App.CD["Colors"]["GoodPurple"];
                fathernameEntry.BackgroundColor = Colors.Black;
            }
            else
            {
                fathernameEntry.PlaceholderColor = Colors.Red;
                fathernameEntry.TextColor = Colors.Red;
                fathernameEntry.FontSize = 16;
                fathernameEntry.BackgroundColor = Colors.Black;
            }
        }
        else if (sender == passEntry)
        {
            if (passEntry.Text != null && Regex.IsMatch(passEntry.Text, @"^[A-Z]{2}\d{7}$"))
            {
                passEntry.PlaceholderColor = Colors.Gray;
                passEntry.FontSize = 14;
                passEntry.TextColor = (Color)App.CD["Colors"]["GoodPurple"];
                passEntry.BackgroundColor = Colors.Black;
            }
            else
            {
                passEntry.FontSize = 16;
                passEntry.PlaceholderColor = Colors.Red;
                passEntry.TextColor = Colors.Red;
                passEntry.BackgroundColor = Colors.Black;
            }

        }
        else if (sender == loginEntry)
        {
            if (loginEntry.Text != null && Regex.IsMatch(loginEntry.Text, @"^[a-zA-Z]\w{4,19}$"))
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
                passwordEntry.FontSize = 16;
                passwordEntry.PlaceholderColor = Colors.Red;
                passwordEntry.TextColor = Colors.Red;
                passwordEntry.BackgroundColor = Colors.Black;
            }

        }
        else if (sender == codewordEntry)
        {
            if (codewordEntry.Text != null && Regex.IsMatch(codewordEntry.Text, @"[a-z]+$"))
            {
                codewordEntry.PlaceholderColor = Colors.Gray;
                codewordEntry.FontSize = 14;
                codewordEntry.TextColor = (Color)App.CD["Colors"]["GoodPurple"];
                codewordEntry.BackgroundColor = Colors.Black;
            }
            else
            {
                codewordEntry.FontSize = 16;
                codewordEntry.PlaceholderColor = Colors.Red;
                codewordEntry.TextColor = Colors.Red;
                codewordEntry.BackgroundColor = Colors.Black;
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
            await Navigation.PopAsync();
        }
        else if (sender == signUpButton)
        {
            DbManager db = new DbManager();
            if (EntriesAreCorrect())
            {
                // поиск логина в базе. Если логина нет в базе, то создаем клиента, его Id и добавляем в БД
                if (!db.HasLogin(loginEntry.Text))
                {
                    if (!friendCheckBox.IsChecked)
                    {
                        string Id = "u" + (100 + DateTime.Today.Day).ToString().Substring(1) + (100 + DateTime.Today.Month).ToString().Substring(1) + DateTime.Today.Year.ToString() + (100 + DateTime.Now.Hour).ToString().Substring(1) +
                            (100 + DateTime.Now.Minute).ToString().Substring(1) + (10000 + (new Random()).Next(1, 10000)).ToString().Substring(1);
                        Client client = new Client(Id, nameEntry.Text, surnameEntry.Text, fathernameEntry.Text, passEntry.Text);
                        LogData logData = new LogData(Id, loginEntry.Text, passwordEntry.Text);
                        db.AddClient(client);
                        db.AddLogData(logData);



                        angryLabel.Text = $"User {loginEntry.Text} successfully added!";
                        angryLabel.TextColor = Colors.LawnGreen;
                    }
                    else
                    {
                        if (codewordEntry.Text != null && Regex.IsMatch(codewordEntry.Text, @"^[a-z]+"))
                        {
                            if (!db.FriendCodeWordUsed(codewordEntry.Text))
                            {
                                angryLabel.Text = "wrong word!";
                                return;
                            }
                            Friend? me = db.GetFriendInvitedWithWord(nameEntry.Text, surnameEntry.Text, fathernameEntry.Text, codewordEntry.Text);
                            if(me == null)
                            {
                                angryLabel.Text = "wrong word or you were not invited!";
                                return;
                            }
                            string Id = "u" + (100 + DateTime.Today.Day).ToString().Substring(1) + (100 + DateTime.Today.Month).ToString().Substring(1) + DateTime.Today.Year.ToString() + (100 + DateTime.Now.Hour).ToString().Substring(1) +
                            (100 + DateTime.Now.Minute).ToString().Substring(1) + (10000 + (new Random()).Next(1, 10000)).ToString().Substring(1);
                            Client client = new Client(Id, nameEntry.Text, surnameEntry.Text, fathernameEntry.Text, passEntry.Text);
                            LogData logData = new LogData(Id, loginEntry.Text, passwordEntry.Text);
                            if(db.GetCardsByUser(me.UserId).ToList<Card>().Count > 0) db.GetMoneyFromAir(db.GetCardsByUser(me.UserId)[0].ID, 200);
                            db.RemoveFriend(me.Id);
                            db.AddClient(client);
                            db.AddLogData(logData);

                            angryLabel.Text = $"User {loginEntry.Text} successfully added!";
                            angryLabel.TextColor = Colors.LawnGreen;



                            string not_id = "n" + (100+ DateTime.Today.Day).ToString().Substring(1) + (100+DateTime.Today.Month).ToString().Substring(1) + DateTime.Today.Year.ToString() +( 100 + DateTime.Now.Hour).ToString().Substring(1) +
                            (100 + DateTime.Now.Minute).ToString().Substring(1) + (10000 + (new Random()).Next(1, 10000)).ToString().Substring(1);

                            string message = $"Congrats! Your friend {me.Name} came to our place! Now you get you 200m bonus!";

                            Notification notification = new Notification(not_id, "uadmin", me.UserId, message, 0);

                            db.AddNotification(notification);

                        }
                        else
                        {
                            angryLabel.Text = "codeword must contain 1-10 letters lowercase!";
                        }
                    }
                }
                else
                {
                    angryLabel.Text = "User whis this login already exists!";
                    angryLabel.Margin = new Thickness(50, -5, 0, 20);
                }
            }
            else
            {
                angryLabel.Text = "Invalid data! Please, correct rows that are red!";
                angryLabel.Margin = new Thickness(50, -5, 0, 20);
            }
        }
    }

    private void friendCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(friendCheckBox.IsChecked== true)
        {
            codewordEntry.IsEnabled = true;
        }
        else
        {
            codewordEntry.IsEnabled = false;
            codewordEntry.Text = "";
        }
    }
}