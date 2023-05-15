

namespace TheGreatKursachOOP.Pages;

public partial class AdminBasePage : ContentPage
{
	public string id
	{
		get
		{
			return "uadmin";
		}
	}
	public AdminBasePage()
	{
		InitializeComponent();
	}

    private async void onButton_Clicked(object sender, EventArgs e)
    {
		if(sender == dealsButton)
		{
			await Navigation.PushAsync(new AdminDealsPage());
			
		}
		else if (sender == jewsButton)
		{
			await Navigation.PushAsync(new AdminJewsPage());
         
        }
		else if (sender == notificationsButton)
		{
			await Navigation.PushAsync(new AdminNotificationPage());
        }
        else if (sender == commentsButton)
		{
			await Navigation.PushAsync(new AdminCommentsPage());
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopToRootAsync();
    }
}