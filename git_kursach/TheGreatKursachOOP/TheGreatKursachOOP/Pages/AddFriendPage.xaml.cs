using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Pages;

public partial class AddFriendPage : ContentPage
{
	private string id;
    DbManager dbManager;
    public string ClientId
    {
        get
        {
            return this.id;
        }
    }
    public AddFriendPage( string client_id)
	{
        id = client_id;
        dbManager= new DbManager();
		InitializeComponent();
	}


    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void onButton_Clicked(object sender, EventArgs e)
    {
        if(sender == backButton)
        {
            await Navigation.PopAsync();
        }
        else if(sender == saveButton)
        {
            if(nameEntry.Text != null && Regex.IsMatch(nameEntry.Text, @"^[A-Z][a-z]*$"))
            {
                if(surnameEntry.Text != null && Regex.IsMatch(surnameEntry.Text, @"^[A-Z][a-z]*$"))
                {
                    if (fathernameEntry.Text != null && Regex.IsMatch(fathernameEntry.Text, @"^[A-Z][a-z]*$"))
                    {
                        if (codeEntry.Text != null && Regex.IsMatch(codeEntry.Text, @"^[a-z]+$"))
                        {
                            if (dbManager.FriendCodeWordUsed(codeEntry.Text))
                            {
                                c_angry_label.Text = "this word is user, try choosing another";
                                return;
                            }

                            if(dbManager.GetCardsByUser(ClientId).Count == 0)
                            {
                                c_angry_label.Text = "you can not invite friend before you add a card";
                                return;
                            }

                            DateTime now = DateTime.Now;
                            string id = "f" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString() +
                            (100 + now.Hour).ToString().Substring(1) + (now.Minute + 100).ToString().Substring(1) + (10000 + (new Random()).Next(1, 10000)).ToString().Substring(1);
                            Friend friend = new Friend(id, ClientId, nameEntry.Text, surnameEntry.Text, fathernameEntry.Text, codeEntry.Text);
                            if (!dbManager.FriendInvited(friend))
                            {
                                dbManager.AddFriend(friend);
                               
                                c_angry_label.Text = $"Now we wait for {friend.Name}";
                            }
                            else
                            {
                                f_angry_label.Text = "You can not invite onr friend twice!";
                            }
                        }
                        else
                        {
                            c_angry_label.Text = "code word must contain 1-10 letters lowercase!";
                        }
                    }
                    else
                    {
                        f_angry_label.Text = "Fathername must contain 1-25 letters";
                    }
                }
                else 
                {
                    s_angry_label.Text = "Surname mist contain 1-25 letters";
                }
            }
            else 
            {
                n_angry_label.Text = "Name must contain 1-25 letters";
            }
        }
    }

    private void entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender == nameEntry)
        {
            n_angry_label.Text = "";
        }
        else if(sender == surnameEntry)
        {
            s_angry_label.Text = "";
        }
        else if(sender == fathernameEntry)
        {
            f_angry_label.Text = "";
        }
        else if(sender == codeEntry)
        {
            c_angry_label.Text = "";
        }
    }
}