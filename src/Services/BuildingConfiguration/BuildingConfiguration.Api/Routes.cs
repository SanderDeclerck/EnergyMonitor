namespace BuildingConfiguration.Api
{
    public static class Routes
    {
        // Building endpoints
        public const string ListBuildingUri = "api/building";
        public const string CreateBuildingUri = "api/building";
        public const string GetBuildingDetailUri = "api/building/{id}";
        public const string DeleteBuildingUri = "api/building/{id}";

        // Meter endpoints
        public const string CreateMeterUri = "api/building/{buildingId}/meter";
        public const string RegisterReadingsUri = "api/building/{buildingId}/meter/{meterEanCode}/readings";
    }
}