using System;
using System.Windows;
using JetBrains.Lifetimes;
using LifetimeWFPLissue.MVVM;

namespace LifetimeWFPLissue
{
    /// <summary>
    ///     Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        private readonly Lifetime _lifetime;


        public ChildWindow(Lifetime lifetime)
        {
            _lifetime = lifetime;
            InitializeComponent();
        }

        private void ChildWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModelBase)
            {
                _lifetime.Bracket(() =>
                {
                    Console.Write('1');
                }, () =>
                {
                    Console.Write('2');
                });
            }
        }
    }
}