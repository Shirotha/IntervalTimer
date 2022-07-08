using System.Windows;
using Shirotha.IntervalTimer.ViewModel;

namespace Shirotha.IntervalTimer.View
{
    public partial class TabataTimer : Window
    {
        public TabataTimer()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is TabataViewModel vm)
            {
                vm.PropertyChanged += TabataPropertyChanged;
                vm.RunCommand.ExecuteAsync(null);
            }
        }

        private bool HasWarned;

        private void TabataPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = (TabataViewModel)DataContext;
            if (!HasWarned && e.PropertyName == nameof(TabataViewModel.IntervalTimeLeft) && vm.IntervalTimeLeft!.Value.TotalSeconds < 5)
            { 
                HasWarned = true;
                vm.IntervalChangeWarningCommand.Execute(null);
            }

            if (e.PropertyName == nameof(TabataViewModel.CurrentInterval))
            {
                HasWarned = false;
                vm.IntervalChangedCommand.Execute(null);
            }
        }
    }
}
