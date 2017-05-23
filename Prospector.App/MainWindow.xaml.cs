using System;
using System.Windows;
using Prospector.App.Controls;
using Prospector.Domain.IoC;
using Prospector.Infrastructure.IoC;
using Prospector.Presentation.IoC;
using StructureMap;

namespace Prospector.App
{
    public partial class MainWindow : Window
    {
        private readonly IContainer _container;

        public MainWindow()
        {
            _container = new Container(cfg =>
            {
                cfg.AddRegistry<PresentationRegistry>();
                cfg.AddRegistry<DomainRegistry>();
                cfg.AddRegistry<InfrastructureRegistry>();

                cfg.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            });

            InitializeComponent();
        }

        private void CalculatorButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentPanel.Children.Clear();
            ContentPanel.Children.Add(_container.GetInstance<CalculatorControl>());
        }

        private void HoldingsButton_OnClick(Object sender, RoutedEventArgs e)
        {
            ContentPanel.Children.Clear();
            ContentPanel.Children.Add(_container.GetInstance<HoldingsControl>());
        }

        private void DashboardButton_OnClick(Object sender, RoutedEventArgs e)
        {
            ContentPanel.Children.Clear();
            ContentPanel.Children.Add(new DashboardControl());
        }
    }
}
