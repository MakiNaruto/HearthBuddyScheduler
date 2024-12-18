using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace HearthHelper;

public partial class AccountPopupAdd : Window, IComponentConnector
{
    private string string_0 = "";

    private bool bool_0;

    public AccountPopupAdd()
    {
        InitializeComponent();
    }

    public string GetAccount()
    {
        return string_0;
    }

    private void ConfigAccountButtonSave_Click(object sender, RoutedEventArgs e)
    {
        string_0 = ConfigAccountName.Text;
        base.DialogResult = true;
    }

    private void method_0(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void method_1(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = false;
        e.Handled = true;
    }
}
