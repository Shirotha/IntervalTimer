using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Shirotha.IntervalTimer.Model
{
    public partial class RepeatedInterval : ObservableValidator, IInterval, IEnumerable<IInterval>
    {
        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Length))]
        private IInterval _Interval = new Interval();

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Length))]
        private IInterval? _Seperator;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Length))]
        [Range(1, 100)]
        private int _Count = 1;

        public int Length => _Interval.Length * _Count + (_Seperator?.Length ?? 0) * (_Count - 1);

        public IEnumerator<IInterval> GetEnumerator()
        {
            for (var i = 0; i < _Count; i++)
            {
                yield return _Interval;
                if (_Seperator is not null && i < _Count - 1)
                    yield return _Seperator;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
