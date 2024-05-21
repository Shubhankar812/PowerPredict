using CommunityToolkit.Maui.Storage;
using RemainingUsefulLife.Services;

namespace RemainingUsefulLife.Pages;

public partial class InputPage : ContentPage
{
    private string _name;
    private string _img;
    IFileSaver _fileSaver;
    
	public InputPage(string name,string img, IFileSaver fileSaver)
	{
		InitializeComponent();
        this._fileSaver = fileSaver;
        this._name = name;
        this._img = img;
		UserName.Text = $"{name}'s data";
        UserImg.IconImageSource = img;
	}

    private async void submitBtn_Clicked(object sender, EventArgs e)
    {
        double f1 = double.Parse(ft1.Text.ToString());
        double f2 = double.Parse(ft2.Text.ToString());
        double f3 = double.Parse(ft3.Text.ToString());
        double f4 = double.Parse(ft4.Text.ToString());
        double f5 = double.Parse(ft5.Text.ToString());

        var data = await APIData.FetchData(f1, f2, f3);
        data = data.ToString();
         
        if (data != null)
        {
            await Navigation.PushAsync(new Pages.ResultPage(_name, _img, f1, f2, f3, f4, f5, data,_fileSaver));
        }
        else
        {
            await DisplayAlert("Error", "some error in fetching the API", "Ok");
        }
    }

    private async void UserImg_Clicked(object sender, EventArgs e)
    {
        
    }
}