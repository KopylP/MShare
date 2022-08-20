namespace Proxy.Api
{
    public interface IStreamingServiceClient
    {
        Task<SongResponseDto> GetByUrlAsync(GetSongByUrlRequestDto request);
        Task<SongsResponseDto> FindAsync(FindSongsRequestDto request, int limit = 5);
    }
}

