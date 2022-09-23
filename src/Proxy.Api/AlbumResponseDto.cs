namespace Proxy.Api
{
	public class AlbumResponseDto
	{
		public AlbumSourceDto Album { get; init; }
		public ArtistSourceDto[] Artists { get; init; }
	}
}

