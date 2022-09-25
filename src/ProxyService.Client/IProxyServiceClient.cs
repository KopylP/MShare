using System;

namespace ProxyService.Client
{
	public interface IProxyServiceClient
	{
		// Song
		Task<SongResponseDto> GetSongByUrlAsync(string url);
		Task<SongsResponseDto> FindSongAsync(string songName, string artistName, string? albumName);

		// Album
        Task<AlbumResponseDto> GetAlbumByUrlAsync(string url);
    }
}