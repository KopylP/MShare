using Newtonsoft.Json;

namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class AppleTrackResponseModel
	{
        // track
        public string Id { get; set; }
        public AttributesData Attributes { get; set; }
        public RelationshipsData Relationships { get; set; }

        public class AttributesData
        {
            public string Name { get; set; }
            public string AlbumName { get; set; }
            public string ArtistName { get; set; }
            public string Isrc { get; set; }
            public string Url { get; set; }
            public Artwork Artwork { get; set; }
        }

        public class Artwork
        {
            public string Url { get; set; }
        }

        public class RelationshipsData
        {
            public ListResponseModel<RelationshipData> Artists { get; set; }
            public ListResponseModel<RelationshipData> Albums { get; set; }
        }

        public class RelationshipData
        {
            public string Id { get; set; }
        }
    }
}

