using System;
using System.Collections.Generic;

namespace Shirotha.IntervalTimer.Model
{
    public interface IInterval
    {
        int Length { get; }
    }

    public static class IntervalExtensions
    {
        public static IEnumerable<Interval> Flatten(this IInterval interval)
        {
            if (interval is Interval result)
                yield return result;
            else if (interval is IEnumerable<IInterval> iter)
                foreach (var item in iter)
                    foreach (var flatten in Flatten(item))
                        yield return flatten;
            else
                throw new ArgumentException("Unknown IInterval implementation.", nameof(interval));
        }

        public static TimeSpan GetTotalTime(this IInterval interval) =>
            TimeSpan.FromSeconds(interval.Length);
    }
}
