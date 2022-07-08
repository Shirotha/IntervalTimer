using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Shirotha.IntervalTimer.Model
{
    public partial class Interval : ObservableValidator, IInterval
    {
        [ObservableProperty]
        private string _Name = "";

        [ObservableProperty]
        [Range(0, 600)]
        private int _Length;

        [ObservableProperty]
        private Color _Color;

        [ObservableProperty]
        private string _Notes = "";

        [ObservableProperty]
        private object? _Tag;
    }
}
