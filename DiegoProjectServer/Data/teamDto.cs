namespace DiegoProjectServer.Data
{
    public class teamDto
    {
        public int id { get; set; }
        public string Name { get; set; } = null!;
        public string Conference { get; set; } = null!;
        public int Win { get; set; }
        public int Loss { get; set; }

    }
}
