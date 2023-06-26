

public enum ViewIndex
{
    EmptyView = 0,
    LoadingView = 1,
    MainScreenView = 2,
    GameplayView = 3,
    SelectLevelView = 4,
    ShopView = 5,
    MissionView = 6,
    SpinView = 7,
    DailyView = 8
}

public class ViewParam { }

public class MainScreenViewParam : ViewParam
{
    public int totalGold;
}

public class ShopViewParam : ViewParam
{
    public int totalGold;
}

public class MissionViewParam : ViewParam
{
    public int totalGold;
}

public class ViewConfig
{
    public static ViewIndex[] viewArray = {
        ViewIndex.EmptyView,
        ViewIndex.LoadingView,
        ViewIndex.MainScreenView,
        ViewIndex.SelectLevelView,
        ViewIndex.GameplayView,
        ViewIndex.ShopView,
        ViewIndex.MissionView,
        ViewIndex.DailyView,
        ViewIndex.SpinView
    };
}