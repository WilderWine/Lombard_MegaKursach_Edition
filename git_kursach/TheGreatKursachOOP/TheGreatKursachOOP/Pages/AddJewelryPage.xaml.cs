using TheGreatKursachOOP.Services;
using TheGreatKursachOOP.Classes;
using System.IO;

namespace TheGreatKursachOOP.Pages;


public partial class AddJewelryPage : ContentPage
{
    DbManager dbManager;
    string path = null;
	private string id;
    public string ClientId
    {
        get
        {
            return this.id;
        }
    }
    public AddJewelryPage(string client_id)
	{
        id = client_id;

        dbManager= new DbManager();
		InitializeComponent();
	}

    private async void image_Clicked(object sender, EventArgs e)
    {
        var customFileType = new FilePickerFileType(
               new Dictionary<DevicePlatform, IEnumerable<string>>
               {
                    
                    { DevicePlatform.Android, new[] { ".png", ".jpg" } }, 
                    { DevicePlatform.WinUI, new[] { ".jpg", ".png" } }, 
                   
               });
        PickOptions options = new()
        {
            PickerTitle = "Please, load photo of your thing!",
            FileTypes = customFileType,
        };

        var result = await FilePicker.Default.PickAsync(options);
        if (result != null)
        {
            if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
            {
                using var stream = await result.OpenReadAsync();
                var im = ImageSource.FromStream(() => stream);
            }
        }

        if (result == null)
        {
            return;
        }

        path = result.FullPath;
        imageimage.Source = result.FullPath;

    }

    private async void button_Clicked(object sender, EventArgs e)
    {
        if ( sender == backButton)
        {
            await Navigation.PopAsync();
        }
        else if(sender == addButton)
        {
            if (path != null && NameEnty.Text != null)
            {
                string Id = "j" + (DateTime.Now.Day + 100).ToString().Substring(1) + (DateTime.Now.Month + 100).ToString().Substring(1) + (DateTime.Now.Year).ToString() +
                    (DateTime.Now.Hour + 100).ToString().Substring(1) + (DateTime.Now.Minute + 100).ToString().Substring(1)+
                    (10000 + new Random().Next(1,10000)).ToString().Substring(1);
                string Name = NameEnty.Text;
                string extension = path.Substring(path.Length - 4);
                string fname=Id+Name+extension;
         
                string curdir = Directory.GetCurrentDirectory();
               
               Directory.CreateDirectory("C:\\Goods\\" + ClientId);
                string newpath = Path.Combine("C:\\Goods\\" + ClientId, fname);
               File.Copy(path, newpath);

                Jewelry jew = new Jewelry(Id, ClientId, Name, newpath, "added");
                dbManager.AddJewelry(jew);

                await Navigation.PopAsync();
            }
        }
    }
}