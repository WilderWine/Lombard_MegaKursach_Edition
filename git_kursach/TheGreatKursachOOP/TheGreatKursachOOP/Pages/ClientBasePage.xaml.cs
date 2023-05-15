

using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Web;
using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Pages;


//[QueryProperty(nameof(ClientId), "ID")]
public partial class ClientBasePage : ContentPage
{
    DbManager dbManager;
	private string id;
	public ObservableCollection<HappyHuman> hhl = new ObservableCollection<HappyHuman>
        {
            new HappyHuman("Romanovski family", "happyfamily1.jpg","Chose new house over old gold"),
            new HappyHuman("Bogush family", "happyfamily2.jpg","Saw a sea for the first time in life "),
            new HappyHuman("Meiers family", "happyfamily3.jpg","Succeeded to pay off the mortgage debt and have a baby"),
            new HappyHuman("Victor, 34", "infogyps1.jpg","Founded his own company and earns $25,000 per month"),
            new HappyHuman("Aristarch, 19", "infogyps2.jpg","Not only paid his debts, but got knowlege of getting money to share with others"),
            new HappyHuman("Amina, 25", "infogyps.jpg","Proved that one pair earrings can make any woman a business-lady")

        };
    public ObservableCollection<Comment> comments;
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

    public ClientBasePage(string client_id)
    {
        id = client_id;
        dbManager= new DbManager();
        
        
        comments = new ObservableCollection<Comment>(dbManager.GetCommentsByStatus("confirmed"));
        InitializeComponent();
        propagandaCarousel.ItemsSource = hhl;
        commentsList.ItemsSource = comments;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        notifLabel.Text = dbManager.GetNotificationsByReceiver(ClientId).ToList<Notification>().Where(n => n.IsRead == 0).Count().ToString();
    }
    

    private async void Button_Clicked(object sender, EventArgs e)
    {
	    if(sender == myJewsButton)
        {
            await Navigation.PushAsync(new DealsJewelriesClientPage(ClientId));
        }
        else if (sender == addJewButton)
        {
            await Navigation.PushAsync(new AddJewelryPage(ClientId));
        }
        else if(sender == myDealsButton)
        {
            await Navigation.PushAsync(new DealsJewelriesClientPage(ClientId));
        }
        else if (sender == inviteFriendButton)
        {
            await Navigation.PushAsync(new AddFriendPage(ClientId));
        }
        else if(sender == leaveCommButton)
        {
            await Navigation.PushAsync(new AddCommentPage(ClientId));
        }
        else if(sender == notificationsButton)
        {
            await Navigation.PushAsync(new NotificationsPage(ClientId));
            
        }
        else if(sender == profileButton)
        {

            await Navigation.PushAsync(new UserProfilePage(ClientId));

        }
    }   
}