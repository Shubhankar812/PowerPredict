using CommunityToolkit.Maui.Storage;
using RemainingUsefulLife.Model;
using System.Text;
using System.Text.Json.Nodes;

namespace RemainingUsefulLife.Pages;

public partial class ResultPage : ContentPage
{
    readonly IFileSaver _fileSaver;
    private List<Double> input;
    private string output;
    private List<finalResult> finalList;
    private int Percent;
    public ResultPage(string name, string image, double f1, double f2, double f3, double f4, double f5, string result, IFileSaver fileSaver)
    {
        InitializeComponent();
        input = new List<Double>();
        this._fileSaver = fileSaver;
        UserImg.IconImageSource = image;


        try {
            usrName.Text = $"{name}'s data";

            first.Text = "Max. Voltage Discharge (V) : " + f1.ToString();
            second.Text = "Min. Voltage Charge (V) : " + f2.ToString();
            third.Text = "Time at 4.15V (s): " + f3.ToString();
            fourth.Text = "Time constant current (s) : " + f4.ToString();
            fifth.Text = "Charging time (s) : " + f5.ToString();

            answer.Text = "Remaining Useful Life : " + result;

            int final_data = result.Length;

            int percent = final_data / 2000;
            percent *= 100;

            
            Percent = percent;
           // percent_answer.Text = "RUL in % is " + $"{percent}";

            input.Add(f1);
            input.Add(f2);
            input.Add(f3);
            input.Add(f4);
            input.Add(f5);


            output = result.ToString();

            finalList = new List<finalResult>();

            // Create and add finalResult object to finalList
            finalResult finalItem = new finalResult();
            finalItem.f1 = f1;
            finalItem.f2 = f2;
            finalItem.f3 = f3;
            finalItem.f4 = f4;
            finalItem.f5 = f5;
            finalItem.result = result.ToString();
            finalList.Add(finalItem);

        }
        catch (Exception ex)
        {
           Console.WriteLine(ex.ToString());
        }
    }

    private async void save_Clicked(object sender, EventArgs e)
    {
        
        var data = new JsonObject
        {
            ["input"] = new JsonArray { input },
            ["output"] = output
        };

        var jsonString = data.ToJsonString();
        var dataStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        await _fileSaver.SaveAsync("", dataStream,CancellationToken.None);

       
       
    }
}