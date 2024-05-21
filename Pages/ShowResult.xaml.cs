using RemainingUsefulLife.Model;

namespace RemainingUsefulLife.Pages;

public partial class ShowResult : ContentPage
{
  private List<finalResult> final_lst;
    public ShowResult(double f1, double f2, double f3, double f4, double f5, string prediction)
	{
		InitializeComponent();
        final_lst = new List<finalResult>();
		
		finalResult newItem = new finalResult { f1 = f1, f2 = f2, f3 = f3, f4 = f4, f5 = f5, result = prediction };
        final_lst.Add(newItem);
        lst.ItemsSource = null; // Resetting ItemsSource to trigger ListView update
        lst.ItemsSource = final_lst;
    }
}