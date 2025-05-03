namespace DiegoProjectServer.Data
{
    public class playerDto
    {
        public int id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Points { get; set; }
        public decimal Rebunds { get; set; }
        public decimal Assists { get; set; }
        public decimal Minutes { get; set;}
        public string Team { get; set; } = null!;
    }
}
