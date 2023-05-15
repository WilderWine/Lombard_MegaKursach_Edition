using System.Collections.ObjectModel;
using System.Xml.Linq;
using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Pages;




public partial class AddCommentPage : ContentPage
{
	private string clientId;
    DbManager dbManager;
    ObservableCollection<Comment> comments;
    Client client;
    string clientName;
    


    public string ClientId
    {
        get
        {
            return this.clientId;
        }
 
    }

    public AddCommentPage( string client_id)
	{
        clientId = client_id;
        dbManager = new DbManager();
        comments = new ObservableCollection<Comment>(dbManager.GetCommentsByStatus("confirmed"));
        InitializeComponent();
        commentsList.ItemsSource = comments;
        Loaded += AddCommentPage_Loaded;
    }

    private void AddCommentPage_Loaded(object sender, EventArgs e)
    {
        client = dbManager.GetClientById(ClientId);
        clientName = client.Name + "  " + client.Surname;
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void nameSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        if (nameSwitch.IsToggled)
        {
            pseudonimEntry.Text = clientName;
        }
    }

    private void pseudonimEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (pseudonimEntry.Text != clientName) nameSwitch.IsToggled = false;
        
    }

    private void onButton_Clicked(object sender, EventArgs e)
    {
        if(sender == backButton)
        {
            Navigation.PopAsync();
        }
        else if(sender == addButton)
        {
            if(pseudonimEntry.Text == null)
            {
                c_angryLabel.Text = "Name field can not be empty";

            }
            else
            {
                if(commentEditor.Text == null) 
                {
                    c_angryLabel.Text = "Comment content can not be empty";
                }
                else
                {
                    DateTime now  = DateTime.Now;
                    string id = "C" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString() +
                    (100 + now.Hour).ToString().Substring(1) + (now.Minute + 100).ToString().Substring(1) + (10000 + (new Random()).Next(1, 10000)).ToString().Substring(1);
                    dbManager.AddComment(new Comment(id, ClientId, pseudonimEntry.Text, commentEditor.Text, "offered"));

                    string notificationId = "n" + (100 + now.Day).ToString().Substring(1) + (100 + now.Month).ToString().Substring(1) + now.Year.ToString() +
                    (100 + now.Hour).ToString().Substring(1) + (now.Minute + 100).ToString().Substring(1) + (10000 + (new Random()).Next(1, 10000)).ToString().Substring(1);
                    string message = $"User {ClientId}, offered new comment with id {id}  {DateTime.Now.ToString()}";
                    dbManager.AddNotification(new Notification(notificationId, ClientId, "uadmin", message, 0));

                    c_angryLabel.Text = "Comment was sent to review";
                }
            }
        }
    }
}