using Academy.CodingTracker.Data;

namespace Academy.CodingTracker;

public partial class App : Application
{
    public static CodingRepository CodingRepository { get; private set; }

    public App(CodingRepository codingRepository)
    {
        InitializeComponent();

        MainPage = new AppShell();

        CodingRepository = codingRepository;
    }
}
