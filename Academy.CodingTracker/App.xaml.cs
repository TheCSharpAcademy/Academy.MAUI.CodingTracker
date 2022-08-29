using Academy.CodingTracker.Data;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace Academy.CodingTracker;

public partial class App : Application
{
    public static CodingRepository CodingRepository { get; private set; }
    public static CodingService CodingService { get; private set; }

    public App(CodingRepository codingRepository, CodingService codingService)
    {
        InitializeComponent();

        MainPage = new AppShell();

        CodingRepository = codingRepository;
        CodingService = codingService;

    }
}
