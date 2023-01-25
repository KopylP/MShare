namespace Proxy.Api
{
    public interface IStreamingServiceClient
    {
        Task<SongResponseDto> GetTrackByUrlAsync(GetByUrlRequestDto request);
        Task<SongsResponseDto> FindTrackAsync(FindSongsRequestDto request);
        Task<AlbumResponseDto> GetAlbumByUrl(GetByUrlRequestDto request);
        Task<AlbumResponseDto> GetAlbumById(GetByIdRequestDto request);
        Task<AlbumResponseDto> GetAlbumByUpc(GetByUpcRequestDto request);
        Task<SongResponseDto> GetTrackByIsrcAsync(GetByIsrcRequestDto request);
        Task<SongResponseDto> GetTrackByIdAsync(GetByIdRequestDto request);
    }
}