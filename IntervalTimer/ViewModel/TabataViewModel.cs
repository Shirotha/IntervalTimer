using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Shirotha.IntervalTimer.Model;

namespace Shirotha.IntervalTimer.ViewModel
{
    public partial class TabataViewModel : ObservableValidator
    {
        private static readonly Uri ChangingWarningSound = new(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Assets/warning.wav"));
        private static readonly Uri ChangeSound = new(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Assets/change.wav"));

        public TabataViewModel()
        {
            _IntervalChangingWarningPlayer.Open(ChangingWarningSound);
            _IntervalChangedPlayer.Open(ChangeSound);
        }

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Intervals))]
        [Range(0, 600)]
        private int _Warmup;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Intervals))]
        [Range(1, 64)]
        private int _Rounds;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Intervals))]
        [Range(0, 100)]
        private int _Exercise;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Intervals))]
        [Range(0, 100)]
        private int _Break;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Intervals))]
        [Range(0, 300)]
        private int _LongBreak;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Intervals))]
        [Range(0, 600)]
        private int _Cooldown;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Intervals))]
        private ObservableCollection<string> _Excercises = new();

        public ObservableCollection<Interval> Intervals =>
            new(new IntervalCollection
            {
                Intervals = {
                        new Interval
                        {
                            Name = "Warmup",
                            Length = _Warmup,
                            Color = Colors.Yellow
                        },
                        new IntervalCollection
                        {
                            Seperator = new Interval
                            {
                                Name = "Break",
                                Length = _LongBreak,
                                Color = Colors.Red
                            },
                            Intervals = new ObservableCollection<IInterval>(_Excercises.Select(name =>
                                new RepeatedInterval
                                {
                                    Seperator = new Interval
                                    {
                                        Name = "Break",
                                        Length = _Break,
                                        Color = Colors.Red
                                    },
                                    Interval = new Interval
                                    {
                                        Name = name,
                                        Length = _Exercise,
                                        Color = Colors.Green
                                    },
                                    Count = _Rounds
                                }))
                        },
                        new Interval
                        {
                            Name = "Cooldown",
                            Length = _Cooldown,
                            Color = Colors.Yellow
                        }
                    }
            }.Flatten());

        public int Count => Intervals.Count;
        [ObservableProperty]
        private int _Index;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(CurrentTask), new string[] { nameof(NextTask) })]
        private Interval? _CurrentInterval;

        public string CurrentTask => _CurrentInterval?.Name ?? "";
        public string NextTask => _CurrentInterval is null ? Intervals.First().Name : Intervals.Skip(_Index)?.FirstOrDefault((Interval?)null)?.Name ?? "Done";

        private DateTime? _SessionStartTime;
        private DateTime? _IntervalStartTime;
        public TimeSpan? SessionTime => DateTime.Now - _SessionStartTime;
        public TimeSpan? SessionTimeLeft => _SessionStartTime is null ? null : TimeSpan.FromSeconds(Intervals.Sum(i => i.Length)) - SessionTime;
        public TimeSpan? IntervalTime => DateTime.Now - _IntervalStartTime;
        public TimeSpan? IntervalTimeLeft => _CurrentInterval is null ? null : TimeSpan.FromSeconds(_CurrentInterval.Length) - IntervalTime;

        [ICommand]
        public async Task Run(CancellationToken token)
        {
            _SessionStartTime = DateTime.Now;
            Index = 0;
            foreach (var interval in Intervals)
            {
                Index++;
                CurrentInterval = interval;
                _IntervalStartTime = DateTime.Now;
                while (IntervalTimeLeft!.Value.TotalSeconds > 0)
                {
                    OnPropertyChanged(nameof(IntervalTimeLeft));
                    OnPropertyChanged(nameof(IntervalTime));
                    OnPropertyChanged(nameof(SessionTimeLeft));
                    OnPropertyChanged(nameof(SessionTime));
                    await Task.Delay(100, token);
                }
            }
            CurrentInterval = null;
        }

        private readonly MediaPlayer _IntervalChangingWarningPlayer = new();
        private readonly MediaPlayer _IntervalChangedPlayer = new();

        [ICommand]
        public void IntervalChangeWarning()
        {
            _IntervalChangedPlayer.Stop();
            _IntervalChangingWarningPlayer.Play();
        }

        [ICommand]
        public void IntervalChanged()
        {
            _IntervalChangingWarningPlayer.Stop();
            _IntervalChangedPlayer.Play();
        }
    }
}
