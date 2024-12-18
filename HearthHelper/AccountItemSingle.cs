using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HearthHelper;

public class AccountItemSingle : INotifyPropertyChanged
{
    private int int_0;

    private bool bool_0;

    private int int_1;

    private int int_2;

    private int int_3;

    private int int_4;

    private string string_0;

    private int int_5;

    private int int_6;

    private string string_1;

    private int int_7;

    private int int_8;

    private string string_2;

    private string string_3;

    private int int_9;

    private bool bool_1;

    private bool bool_2;

    private bool bool_3;

    [CompilerGenerated]
    private PropertyChangedEventHandler propertyChangedEventHandler_0;

    public int Mode
    {
        get
        {
            return int_0;
        }
        set
        {
            int_0 = value;
            OnPropertyChanged("Mode");
        }
    }

    public bool Enable
    {
        get
        {
            return bool_0;
        }
        set
        {
            bool_0 = value;
            mergInfo();
            OnPropertyChanged("Enable");
        }
    }

    public int StartTimeHour
    {
        get
        {
            return int_1;
        }
        set
        {
            int_1 = ((value >= 0 && value <= 23) ? value : 0);
            mergInfo();
            OnPropertyChanged("StartTimeHour");
        }
    }

    public int StartTimeMin
    {
        get
        {
            return int_2;
        }
        set
        {
            int_2 = ((value >= 0 && value <= 59) ? value : 0);
            mergInfo();
            OnPropertyChanged("StartTimeMin");
        }
    }

    public int EndTimeHour
    {
        get
        {
            return int_3;
        }
        set
        {
            int_3 = ((value < 0 || value > 23) ? 23 : value);
            mergInfo();
            OnPropertyChanged("EndTimeHour");
        }
    }

    public int EndTimeMin
    {
        get
        {
            return int_4;
        }
        set
        {
            int_4 = ((value < 0 || value > 59) ? 59 : value);
            mergInfo();
            OnPropertyChanged("EndTimeMin");
        }
    }

    public string Info
    {
        get
        {
            return string_0;
        }
        set
        {
            string_0 = value;
            OnPropertyChanged("Info");
        }
    }

    public int NormalRule
    {
        get
        {
            return int_5;
        }
        set
        {
            int_5 = value;
            mergInfo();
            OnPropertyChanged("NormalRule");
        }
    }

    public int NormalBehavior
    {
        get
        {
            return int_6;
        }
        set
        {
            int_6 = value;
            mergInfo();
            OnPropertyChanged("NormalBehaviour");
        }
    }

    public string NormalDeck
    {
        get
        {
            return string_1;
        }
        set
        {
            string_1 = value;
            mergInfo();
            OnPropertyChanged("NormalDeck");
        }
    }

    public int MercRule
    {
        get
        {
            return int_7;
        }
        set
        {
            int_7 = value;
            mergInfo();
            OnPropertyChanged("MercRule");
        }
    }

    public int MercBehavior
    {
        get
        {
            return int_8;
        }
        set
        {
            int_8 = value;
            mergInfo();
            OnPropertyChanged("MercBehaviour");
        }
    }

    public string MercTeam
    {
        get
        {
            return string_2;
        }
        set
        {
            string_2 = value;
            mergInfo();
            OnPropertyChanged("MercTeam");
        }
    }

    public string MercMap
    {
        get
        {
            return string_3;
        }
        set
        {
            string_3 = value;
            mergInfo();
            OnPropertyChanged("MercMap");
        }
    }

    public int MercInterval
    {
        get
        {
            return int_9;
        }
        set
        {
            int_9 = ((value >= 27 || value <= 10) ? 22 : value);
            mergInfo();
            OnPropertyChanged("MercInterval");
        }
    }

    public bool MercConcede
    {
        get
        {
            return bool_1;
        }
        set
        {
            bool_1 = value;
            mergInfo();
            OnPropertyChanged("MercConcede");
        }
    }

    public bool MercCraft
    {
        get
        {
            return bool_2;
        }
        set
        {
            bool_2 = value;
            mergInfo();
            OnPropertyChanged("MercCraft");
        }
    }

    public bool MercUpdate
    {
        get
        {
            return bool_3;
        }
        set
        {
            bool_3 = value;
            mergInfo();
            OnPropertyChanged("MercUpdate");
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

    public AccountItemSingle(int myMode)
    {
        int_5 = 0;
        int_6 = 1;
        string_1 = "请配置账号";
        int_7 = 0;
        int_8 = 0;
        string_3 = "1-1";
        string_2 = "请配置队伍";
        int_9 = 22;
        bool_1 = false;
        bool_2 = true;
        bool_3 = false;
        int_0 = myMode;
        bool_0 = true;
        int_1 = 0;
        int_2 = 0;
        int_3 = 23;
        int_4 = 59;
        mergInfo();
    }

    public AccountItemSingle(int myMode, bool myEnable, int myStartTimeHour, int myStartTimeMin, int myEndTimeHour, int myEndTimeMin, string myInfo, int myNormalRule, int myNormalBehavior, string myNormalDeck, int myMercRule, int myMercBehavior, string myMercTeam, string myMercMap, int myMercInterval, bool myMercConcede, bool myMercCraft, bool myMercUpdate)
    {
        int_0 = myMode;
        bool_0 = myEnable;
        int_1 = myStartTimeHour;
        int_2 = myStartTimeMin;
        int_3 = myEndTimeHour;
        int_4 = myEndTimeMin;
        string_0 = myInfo;
        int_5 = myNormalRule;
        int_6 = myNormalBehavior;
        string_1 = myNormalDeck;
        int_7 = myMercRule;
        int_8 = myMercBehavior;
        string_2 = myMercTeam;
        string_3 = myMercMap;
        int_9 = myMercInterval;
        bool_1 = myMercConcede;
        bool_2 = myMercCraft;
        bool_3 = myMercUpdate;
    }

    public AccountItemSingle Clone()
    {
        return new AccountItemSingle(int_0, bool_0, int_1, int_2, int_3, int_4, string_0, int_5, int_6, string_1, int_7, int_8, string_2, string_3, int_9, bool_1, bool_2, bool_3);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        if (propertyChangedEventHandler_0 != null)
        {
            propertyChangedEventHandler_0(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public void mergInfo()
    {
        if (int_0 == 0)
        {
            string_0 = string.Format("[天梯]--[{0}:{1}-{2}:{3}]--[{4}]--[{5}]--[{6}]", int_1.ToString("D2"), int_2.ToString("D2"), int_3.ToString("D2"), int_4.ToString("D2"), StringConst.rule0[int_5], StringConst.behavior0[int_6], string_1);
        }
        else if (int_0 == 1)
        {
            string_0 = string.Format("[佣兵]--[{0}:{1}-{2}:{3}]--[{4}]--[{5}]--[{6}]--[{7}]", int_1.ToString("D2"), int_2.ToString("D2"), int_3.ToString("D2"), int_4.ToString("D2"), StringConst.rule1[int_7], StringConst.behavior1[int_8], string_2, string_3);
        }
    }
}
