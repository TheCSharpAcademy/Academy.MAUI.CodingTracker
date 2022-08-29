using Academy.CodingTracker.Models;

namespace Academy.CodingTracker;

public partial class MainPage : ContentPage
{
    private TimeOnly time = new();
    private bool isRunning;

    private int MinutesToAdjust = 0;
    private int HoursToAdjust = 0;
    private DateTime codingDate = DateTime.Today;

    List<CodingDay> codingDayList;

    public MainPage()
    {
        codingDayList = App.CodingRepository.GetAll();
        InitializeComponent();
        codingList.ItemsSource = codingDayList;

        PopulateTotals();
    }

    private void PopulateTotals()
    {
        weekTotal.Text = App.CodingService.GetTotalTime(codingDayList, "week").ToString();
        monthTotal.Text = App.CodingService.GetTotalTime(codingDayList, "month").ToString();
        yearTotal.Text = App.CodingService.GetTotalTime(codingDayList, "year").ToString();
    }

    #region Timer Controls

    private async void OnStartStop(object sender, EventArgs e)
    {
        isRunning = !isRunning;
        startStopButton.Source = isRunning ? "pause.png" : "play.png";

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

    #endregion

    private void OnSaveDay(object sender, EventArgs e)
    {
        if (App.CodingRepository.GetAll().FirstOrDefault(x => x.Date == codingDate) != null)
        {
            errorMessageLabel.Text = "Can't have two records with the same date";
            return;
        };

        errorMessageLabel.Text = "";

        App.CodingRepository.Add(new CodingDay
        {
            Duration = time.ToTimeSpan(),
            Date = codingDate
        });

        codingList.ItemsSource = App.CodingRepository.GetAll();
        PopulateTotals();

        time = new TimeOnly();
        isRunning = !isRunning;
        SetTime();
    }

    private void OnDateSelected(object sender, EventArgs e)
    {
        DatePicker datePicker = (DatePicker)sender;
        codingDate = datePicker.Date;
    }

    #region Adjust Timer
    private void OnAdjustClock(object sender, EventArgs e)
    {
        errorMessageLabel.Text = "";
        adjustTimeArea.IsVisible = !adjustTimeArea.IsVisible;
        mainButtonsArea.IsVisible = !mainButtonsArea.IsVisible;

        adjustTimerBtn.Text = adjustTimeArea.IsVisible ? "Cancel" : "Adjust Time";

        if (adjustTimeArea.IsVisible)
        {
            MinutesToAdjust = 0;
            HoursToAdjust = 0;
            hoursToAdjustLabel.Text = HoursToAdjust.ToString();
            minutesToAdjustLabel.Text = MinutesToAdjust.ToString();
        }
    }

    private void OnAddHours(object sender, EventArgs e)
    {
        if (HoursToAdjust + 1 > 24)
            return;

        HoursToAdjust += 1;
        hoursToAdjustLabel.Text = HoursToAdjust.ToString();
    }

    private void OnSubtractHours(object sender, EventArgs e)
    {
        if (HoursToAdjust - 1 < -23)
            return;

        HoursToAdjust -= 1;
        hoursToAdjustLabel.Text = HoursToAdjust.ToString();
    }

    private void OnAddMinutes(object sender, EventArgs e)
    {
        if (MinutesToAdjust + 1 > 59)
            return;

        MinutesToAdjust += 1;
        minutesToAdjustLabel.Text = MinutesToAdjust.ToString();
    }

    private void OnSubtractMinutes(object sender, EventArgs e)
    {
        if (MinutesToAdjust - 1 < -59)
            return;

        MinutesToAdjust -= 1;
        minutesToAdjustLabel.Text = MinutesToAdjust.ToString();
    }

    private async void OnSaveAdjustTimer(object sender, EventArgs e)
    {
        time = time.Add(TimeSpan.FromHours(HoursToAdjust) + TimeSpan.FromMinutes(MinutesToAdjust));
        SetTime();

        MinutesToAdjust = 0;
        HoursToAdjust = 0;
        hoursToAdjustLabel.Text = HoursToAdjust.ToString();
        minutesToAdjustLabel.Text = MinutesToAdjust.ToString();
        adjustTimeArea.IsVisible = false;
        mainButtonsArea.IsVisible = true;
        adjustTimerBtn.Text = "Adjust Timer";
    }

    #endregion Adjust Timer

    private void OnDelete(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;

        App.CodingRepository.Delete((int)button.BindingContext);

        codingList.ItemsSource = App.CodingRepository.GetAll();
    }
}

