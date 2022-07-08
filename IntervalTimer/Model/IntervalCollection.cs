using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Shirotha.IntervalTimer.Model
{
    public partial class IntervalCollection : ObservableValidator, IInterval, IEnumerable<IInterval>
    {
        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Count), new string[] { nameof(Length) })]
        private ObservableCollection<IInterval> _Intervals = new();

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Count), new string[] { nameof(Length) })]
        private IInterval? _Seperator;

        public int Count => _Intervals.Count + (_Seperator is null ? 0 : _Intervals.Count - 1);

        public int Length => _Intervals.Sum(i => i.Length) + (_Seperator?.Length ?? 0) * (_Intervals.Count - 1);

        public IEnumerator<IInterval> GetEnumerator()
        {
            if (_Seperator is null)
            {
                foreach (var interval in _Intervals)
                    yield return interval;

                yield break;
            }

            var first = true;
            foreach (var interval in _Intervals)
            {
                if (first)
                    first = false;
                else
                    yield return _Seperator;

                yield return interval;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 
    }
}
