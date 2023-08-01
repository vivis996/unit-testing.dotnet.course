namespace TestNinja.Mocking;

public class VideoRepository : IVideoRepository
{
    public IEnumerable<Video> GetUnprocessedVideos()
    {
        using (var context = new VideoContext())
        {
            var videos = from video in context.Videos
                         where !video.IsProcessed
                         select video;
            return videos.ToList();
        }
    }
}

public interface IVideoRepository
{
    IEnumerable<Video> GetUnprocessedVideos();
}