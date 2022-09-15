using System.Collections.ObjectModel;
using ScheduleApp.Services;
using ScheduleApp.Model;
namespace ScheduleApp.ViewModel;

public partial class TableViewModel : BaseViewModel
{
    LoadTable loadTable;
    public ObservableCollection<Period> Table { get; } = new();
    public Command LoadTableCommand { get; }
    public TableViewModel(LoadTable loadTable)
    {
        Title = "Table loader";
        this.loadTable = loadTable;
        LoadTableCommand = new Command(async () => await GetTableAsync());
    }
    async Task GetTableAsync()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;
            var table = await loadTable.GetTable();
            if (Table.Count() != 0)
                Table.Clear();
            foreach (var item in table)
                Table.Add(item);
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error!", $"Unable to load {e.Message}", "OK");
        }
        finally
        {

        }
    }
}