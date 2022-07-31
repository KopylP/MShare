using System;
namespace Proxy.Api
{
    public class ArtistSourceDto
    {
        public string Name { get; init; }

        public static ArtistSourceDto Of(string name) => new ArtistSourceDto { Name = name };
    }
}

