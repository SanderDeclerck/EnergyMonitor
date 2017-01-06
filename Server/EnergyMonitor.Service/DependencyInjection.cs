namespace SimpleInjector
{
    public static class DependencyInjection
    {
        public static void RegisterServiceDependencies(this Container container)
        {
            container.RegisterBusinessDependencies();
        }
    }
}