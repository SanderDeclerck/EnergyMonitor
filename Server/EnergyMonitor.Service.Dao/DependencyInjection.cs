using EnergyMonitor.Service.Dao;

namespace SimpleInjector
{
    public static class DependencyInjection
    {
        public static void RegisterDaoDependencies(this Container container)
        {
            container.Register<IReadingsDao, ReadingsDao>();
        }
    }
}