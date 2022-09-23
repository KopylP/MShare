namespace Proxy.Api
{
    public interface IStreamingServiceClient
    {
        Task<SongResponseDto> GetSongByUrlAsync(GetByUrlRequestDto request);
        Task<SongsResponseDto> FindSongAsync(FindSongsRequestDto request, int limit = 5);
        Task<AlbumResponseDto> GetAlbumByUrl(GetByUrlRequestDto request);
    }
}