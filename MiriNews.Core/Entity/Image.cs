namespace MiriNews.Core.Entity
{
    public class Image
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Path { get; set; }
    }
}
