namespace HearthHelper;

public class StringConst
{
    public static string[] rule0;

    public static string[] behavior0;

    public static string[] rule1;

    public static string[] behavior1;

    static StringConst()
    {
        rule0 = new string[5] { "狂野模式", "标准模式", "经典模式", "休闲模式", "幻变模式" };
        behavior0 = new string[13]
        {
            "丨通用丨不设惩罚", "丨通用丨暗牧", "丨通用丨酸鱼人萨", "丨标准丨酸快攻德", "丨标准丨元素法", "丨标准丨元素萨", "丨狂野丨奥秘法", "丨狂野丨剑鱼贼", "丨狂野丨偶数萨", "丨狂野丨暗龙牧",
            "丨狂野丨快攻暗牧", "丨狂野丨锁喉剑鱼贼", "丨过时丨任务海盗战"
        };
        rule1 = new string[8] { "刷全图", "刷碎片", "自动解锁地图", "自动佣兵任务", "自动主线任务", "自动解锁装备", "真人PVP", "挂机收菜" };
        behavior1 = new string[2] { "PVE通用", "PVP通用" };
    }
}
