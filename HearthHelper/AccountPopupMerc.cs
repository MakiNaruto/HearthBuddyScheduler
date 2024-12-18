using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace HearthHelper;

public partial class AccountPopupMerc : Window, IComponentConnector
{
    public AccountItemSingle AccountItem;

    private bool bool_0;

    public AccountPopupMerc(ref AccountItemSingle item)
    {
        AccountItem = item;
        base.DataContext = AccountItem;
        InitializeComponent();
    }

    private void ConfigAccountButton1_Click(object sender, RoutedEventArgs e)
    {
        base.DialogResult = true;
        Close();
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
