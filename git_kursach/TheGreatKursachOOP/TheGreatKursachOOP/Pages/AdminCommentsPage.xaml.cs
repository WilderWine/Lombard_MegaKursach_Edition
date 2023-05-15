using System.Collections.ObjectModel;
using TheGreatKursachOOP.Classes;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Pages;

public partial class AdminCommentsPage : ContentPage
{

    ObservableCollection<Comment> comments;
    DbManager dbManager;


	public AdminCommentsPage()
	{
        dbManager= new DbManager();
		InitializeComponent();
        comments = new ObservableCollection<Comment>(dbManager.GetCommentsByStatus("offered"));
        Loaded += AdminCommentsPage_Loaded;
	}

    private void AdminCommentsPage_Loaded(object sender, EventArgs e)
    {
        commentsView.ItemsSource= comments;
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (sender == cancelButton)
        {
            commentsView.SelectedItems.Clear();
        }
        else if (sender == BackButton)
        {
            commentsView.SelectedItems.Clear();
            await Navigation.PopAsync();
        }
        else if(sender == publishButton)
        {
            ObservableCollection<Comment> temp = new ObservableCollection<Comment>();
            foreach (Comment comment in comments)
            {
                temp.Add(comment);
            }
            foreach (Comment comment in commentsView.SelectedItems)
            {
                dbManager.ChangeCommentStatus(comment.Id, "confirmed");
                temp.Remove(comment);
            }
            comments = temp;
            commentsView.ItemsSource = comments;

        }
        else if(sender == deleteButton)
        {
            ObservableCollection<Comment> temp = new ObservableCollection<Comment>();
            foreach (Comment comment in comments)
            {
                temp.Add(comment);
            }
            foreach (Comment comment in commentsView.SelectedItems)
            {
                dbManager.RemoveComment(comment.Id);
                temp.Remove(comment);
            }
            comments = temp;
            commentsView.ItemsSource = comments;
        }
        else if(sender == updateButton)
        {
            comments = new ObservableCollection<Comment>(dbManager.GetCommentsByStatus("offered"));
            commentsView.ItemsSource = comments;

        }
    }
}