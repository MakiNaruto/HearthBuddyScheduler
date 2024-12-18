using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace HearthHelper;

public partial class AccountPopupList : Window, IComponentConnector, IStyleConnector
{
    public AccountItemWhole AccountItemWhole;

    private bool bool_0;

    public AccountPopupList(ref AccountItemWhole whole)
    {
        AccountItemWhole = whole;
        base.DataContext = AccountItemWhole;
        InitializeComponent();
        ItemListBox.ItemsSource = (AccountItemWhole.itemList = new ObservableCollection<AccountItemSingle>(AccountItemWhole.itemList.OrderBy((AccountItemSingle item) => item.StartTimeHour)));
    }

    private void ConfigAccountButton0_Click(object sender, RoutedEventArgs e)
    {
        AccountItemSingle item2 = new AccountItemSingle(0);
        AccountPopupNormal accountPopupNormal = new AccountPopupNormal(ref item2);
        accountPopupNormal.Left = base.Left + (base.Width - accountPopupNormal.Width) / 2.0;
        accountPopupNormal.Top = base.Top + (base.Height - accountPopupNormal.Height) / 2.0;
        if (accountPopupNormal.ShowDialog().Value)
        {
            AccountItemWhole.itemList.Add(item2);
            ItemListBox.ItemsSource = (AccountItemWhole.itemList = new ObservableCollection<AccountItemSingle>(AccountItemWhole.itemList.OrderBy((AccountItemSingle item) => item.StartTimeHour)));
        }
    }

    private void ConfigAccountButton1_Click(object sender, RoutedEventArgs e)
    {
        AccountItemSingle item2 = new AccountItemSingle(1);
        AccountPopupMerc accountPopupMerc = new AccountPopupMerc(ref item2);
        accountPopupMerc.Left = base.Left + (base.Width - accountPopupMerc.Width) / 2.0;
        accountPopupMerc.Top = base.Top + (base.Height - accountPopupMerc.Height) / 2.0;
        if (accountPopupMerc.ShowDialog().Value)
        {
            AccountItemWhole.itemList.Add(item2);
            ItemListBox.ItemsSource = (AccountItemWhole.itemList = new ObservableCollection<AccountItemSingle>(AccountItemWhole.itemList.OrderBy((AccountItemSingle item) => item.StartTimeHour)));
        }
    }

    private void ConfigAccountButton2_Click(object sender, RoutedEventArgs e)
    {
    }

    private void method_0(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            bool flag = false;
            AccountItemSingle item2 = button.Tag as AccountItemSingle;
            AccountItemSingle accountItemSingle = item2.Clone();
            if (item2.Mode == 0)
            {
                AccountPopupNormal accountPopupNormal = new AccountPopupNormal(ref item2);
                accountPopupNormal.Left = base.Left + (base.Width - accountPopupNormal.Width) / 2.0;
                accountPopupNormal.Top = base.Top + (base.Height - accountPopupNormal.Height) / 2.0;
                flag = accountPopupNormal.ShowDialog().Value;
            }
            else if (item2.Mode == 1)
            {
                AccountPopupMerc accountPopupMerc = new AccountPopupMerc(ref item2);
                accountPopupMerc.Left = base.Left + (base.Width - accountPopupMerc.Width) / 2.0;
                accountPopupMerc.Top = base.Top + (base.Height - accountPopupMerc.Height) / 2.0;
                flag = accountPopupMerc.ShowDialog().Value;
            }
            AccountItemWhole.itemList.Remove(item2);
            AccountItemWhole.itemList.Add((!flag) ? accountItemSingle : item2);
            ItemListBox.ItemsSource = (AccountItemWhole.itemList = new ObservableCollection<AccountItemSingle>(AccountItemWhole.itemList.OrderBy((AccountItemSingle item) => item.StartTimeHour)));
        }
    }

    private void method_1(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && MessageBox.Show("确定删除此模式？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
        {
            AccountItemSingle item2 = button.Tag as AccountItemSingle;
            AccountItemWhole.itemList.Remove(item2);
            ItemListBox.ItemsSource = (AccountItemWhole.itemList = new ObservableCollection<AccountItemSingle>(AccountItemWhole.itemList.OrderBy((AccountItemSingle item) => item.StartTimeHour)));
        }
    }

    private void method_2(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void method_3(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = false;
        e.Handled = true;
    }
}
