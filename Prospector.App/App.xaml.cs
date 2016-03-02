using System.Collections.Generic;
using System.Windows;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Entities;
using StructureMap;

namespace Prospector.App
{
    public partial class App : Application
    {
        public static IContainer Container;
        public static IList<HoldingData> Holdings;

        public App()
        {
            Container = Bootstrapper.Initialize();
            Holdings = Container.GetInstance<IHoldingDataProvider>().Get();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Container.GetInstance<IHoldingDataProvider>().Save(Holdings);

            base.OnExit(e);
        }
    }
}
