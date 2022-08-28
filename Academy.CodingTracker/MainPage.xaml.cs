namespace Academy.CodingTracker;

public partial class MainPage : ContentPage
{
    private TimeOnly time = new();

    private bool isRunning;
    private bool isEditing;

    private int MinutesToAdjust = 0;
    private int HoursToAdjust = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnStartStop(object sender, EventArgs e)
    {
        isRunning = !isRunning;
        startStopButton.Text = isRunning ? "Pause" : "Play";

        while (isRunning)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            SetTime();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    private void SetTime()
    {
        var seconds = time.Second > 9 ? time.Second.ToString() : $"0{time.Second}";
        var minutes = time.Minute > 9 ? time.Minute.ToString() : $"0{time.Minute}";
        var hours = time.Hour > 9 ? time.Hour.ToString() : $"0{time.Hour}";

        timeLabel.Text = $"{hours}:{minutes}:{seconds}";
    }

    private void OnReset(object sender, EventArgs e)
    {
        time = new TimeOnly();
        SetTime();
    }

    private void OnAdjustClock(object sender, EventArgs e)
    {
        isEditing = true;
    }

    private void OnAddHours(object sender, EventArgs e)
    {
        HoursToAdjust += 1;
        hoursToAdjustLabel.Text = HoursToAdjust.ToString();
    }

    private void OnSubtractHours(object sender, EventArgs e)
    {
        HoursToAdjust -= 1;
        hoursToAdjustLabel.Text = HoursToAdjust.ToString();
    }

    private void OnAddMinutes(object sender, EventArgs e)
    {
        MinutesToAdjust += 1;
        minutesToAdjustLabel.Text = MinutesToAdjust.ToString();
    }

    private void OnSubtractMinutes(object sender, EventArgs e)
    {
        MinutesToAdjust -= 1;
        minutesToAdjustLabel.Text = MinutesToAdjust.ToString();
    }
}

