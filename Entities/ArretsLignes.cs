namespace melkikerapostgrescrud.Entities
{
    public class ArretsLigne
    {
        public string id { get; set; } = string.Empty;
        public string route_long_name { get; set; } = string.Empty;
        public string stop_id { get; set; } = string.Empty;
        public string stop_name { get; set; } = string.Empty;
        public string stop_lon { get; set; } = string.Empty;
        public string stop_lat { get; set; } = string.Empty;
        public string operatorname { get; set; } = string.Empty;
        public string shortname { get; set; } = string.Empty;
        public string mode { get; set; } = string.Empty;
        public Pointgeo pointgeo { get; set; } = new Pointgeo();
        public string nom_commune { get; set; } = string.Empty;
        public string code_insee { get; set; } = string.Empty;
    }
}
