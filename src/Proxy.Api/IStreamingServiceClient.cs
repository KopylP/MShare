namespace Proxy.Api
{
    public interface IStreamingServiceClient
    {
        Task<SongResponseDto> GetTrackByUrlAsync(GetByUrlRequestDto request);
        Task<SongsResponseDto> FindSongAsync(FindSongsRequestDto request);
        Task<AlbumResponseDto> GetAlbumByUrl(GetByUrlRequestDto request);
        Task<SongResponseDto> GetTrackByIsrcAsync(GetByIsrcRequestDto request);
        Task<SongResponseDto> GetTrackByIdAsync(GetByIdRequestDto request);
    }
}