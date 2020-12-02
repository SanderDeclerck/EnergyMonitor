namespace BuildingConfiguration.Api
{
    public static class Routes
    {
        public const string BuildingUri = "api/building";
        public const string BuildingDetailUri = "api/building/{id}";
        public const string MeterUri = "api/building/{buildingId}/meter";
        public const string RegisterReadingsUri = "api/building/{buildingId}/meter/{meterEanCode}/readings";
    }
}