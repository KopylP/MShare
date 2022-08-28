using System;

namespace ProxyService.Client
{
	public interface IProxyServiceClient
	{
		Task<SongResponseDto> GetSongByUrlAsync(string url);
		Task<SongsResponseDto> FindAsync(string songName, string artistName, string? albumName);
	}
}