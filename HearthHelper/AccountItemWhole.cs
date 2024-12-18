using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HearthHelper;

public class AccountItemWhole : INotifyPropertyChanged
{
    private bool bool_0;

    private string string_0;

    private string string_1;

    private bool bool_1;

    private int int_0;

    public AccountItemSingle currItem;

    public ObservableCollection<AccountItemSingle> itemList = new ObservableCollection<AccountItemSingle>();

    [CompilerGenerated]
    private PropertyChangedEventHandler propertyChangedEventHandler_0;

    public bool Selected
    {
        get
        {
            return bool_0;
        }
        set
        {
            bool_0 = value;
            OnPropertyChanged("Selected");
        }
    }

    public string Email
    {
        get
        {
            return string_0;
        }
        set
        {
            string_0 = value;
            OnPropertyChanged("Email");
            EmailShow = UtilsCom.ReplaceWithSpecialChar(value);
        }
    }

    public string EmailShow
    {
        get
        {
            return string_1;
        }
        set
        {
            string_1 = value;
            OnPropertyChanged("EmailShow");
        }
    }

    public bool Running
    {
        get
        {
            return bool_1;
        }
        set
        {
            bool_1 = value;
            OnPropertyChanged("Running");
        }
    }

    public int StonePid
    {
        get
        {
            return int_0;
        }
        set
        {
            int_0 = value;
            OnPropertyChanged("StonePid");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged
    {
        [CompilerGenerated]
        add
        {
            PropertyChangedEventHandler propertyChangedEventHandler = propertyChangedEventHandler_0;
            PropertyChangedEventHandler propertyChangedEventHandler2;
            do
            {
                propertyChangedEventHandler2 = propertyChangedEventHandler;
                PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
                propertyChangedEventHandler = Interlocked.CompareExchange(ref propertyChangedEventHandler_0, value2, propertyChangedEventHandler2);
            }
            while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
        }
        [CompilerGenerated]
        remove
        {
            PropertyChangedEventHandler propertyChangedEventHandler = propertyChangedEventHandler_0;
            PropertyChangedEventHandler propertyChangedEventHandler2;
            do
            {
                propertyChangedEventHandler2 = propertyChangedEventHandler;
                PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
                propertyChangedEventHandler = Interlocked.CompareExchange(ref propertyChangedEventHandler_0, value2, propertyChangedEventHandler2);
            }
            while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
        }
    }

    public AccountItemWhole(bool isSelected, string whichEmail)
    {
        bool_0 = isSelected;
        string_0 = whichEmail;
        string_1 = UtilsCom.ReplaceWithSpecialChar(whichEmail);
        bool_1 = false;
        int_0 = 0;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        if (propertyChangedEventHandler_0 != null)
        {
            propertyChangedEventHandler_0(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
