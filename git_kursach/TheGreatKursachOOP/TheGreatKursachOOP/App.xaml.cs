using System.Collections.Generic;
using TheGreatKursachOOP.Pages;

namespace TheGreatKursachOOP;

public partial class App : Application
{
	public ResourceDictionary GetColors()
	{
		return ColorsDictionary;
	}
	public static Dictionary<string, ResourceDictionary> CD;
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new LogInPage());

		CD = new Dictionary<string, ResourceDictionary>();
		foreach (var dict in Application.Current.Resources.MergedDictionaries)
		{
			string key = dict.Source.OriginalString.Split(';').First().Split('/').Last().Split('.').First();
			CD.Add(key, dict);
        }
		

    }
}
