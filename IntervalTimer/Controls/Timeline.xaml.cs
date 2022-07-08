using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shirotha.IntervalTimer.Model;

namespace Shirotha.IntervalTimer.Controls
{
    public partial class Timeline : UserControl
    {
        public Timeline()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IntervalsProperty = DependencyProperty.Register(
            nameof(Intervals),
            typeof(ObservableCollection<Interval>),
            typeof(Timeline),
            new PropertyMetadata(new ObservableCollection<Interval>(), IntervalsChanged));
        public ObservableCollection<Interval> Intervals
        {
            get => (ObservableCollection<Interval>)GetValue(IntervalsProperty);
            set => SetValue(IntervalsProperty, value);
        }
        private static void IntervalsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeline = (Timeline)d;
            var intervals = (ObservableCollection<Interval>)e.NewValue;
            timeline.FullScale = timeline.ActualWidth / intervals.Sum(i => i.Length);
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            nameof(Scale),
            typeof(double),
            typeof(Timeline),
            new PropertyMetadata(1.0));
        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public static readonly DependencyProperty OriginProperty = DependencyProperty.Register(
            nameof(Origin),
            typeof(double),
            typeof(Timeline),
            new PropertyMetadata(0.0));
        public double Origin
        {
            get => (double)GetValue(OriginProperty);
            set => SetValue(OriginProperty, value);
        }

        private static readonly DependencyPropertyKey FullScalePropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(FullScale),
            typeof(double),
            typeof(Timeline),
            new PropertyMetadata(1.0));
        public static readonly DependencyProperty FullScaleProperty = FullScalePropertyKey.DependencyProperty;
        public double FullScale
        {
            get => (double)GetValue(FullScaleProperty);
            set => SetValue(FullScalePropertyKey, value);
        }

        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(
            nameof(Progress),
            typeof(double),
            typeof(Timeline),
            new PropertyMetadata(0.0));
        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
    }
}
