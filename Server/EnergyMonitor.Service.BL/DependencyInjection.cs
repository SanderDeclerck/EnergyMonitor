using EnergyMonitor.Service.BL;

namespace SimpleInjector
{
    public static class DependencyInjection
    {
        public static void RegisterBusinessDependencies(this Container container)
        {
            container.RegisterDaoDependencies();

            container.Register<IReadingsLogicController, ReadingsLogicController>();
            container.Register<IMeterLogicController, MeterLogicController>();
        }
    }
}