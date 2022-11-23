using System;
using MShare.Framework.Types.Addresses;

namespace ProxyService.Client
{
	public interface IProxyServiceClient
	{
		// Song
		Task<SongResponseDto> GetSongByUrlAsync(string url);
		Task<SongResponseDto> GetSongByIdAsync(string id, CountryCode2? region = default);
        Task<SongResponseDto> GetSongByIsrcAsync(string isrc, CountryCode2? region = default);
        Task<SongsResponseDto> FindSongAsync(string songName, string artistName, string? albumName);

		// Album
        Task<AlbumResponseDto> GetAlbumByUrlAsync(string url);
    }
}