using System;
namespace AppleProxy.WebService.Infrastructure.Client.Models
{
    public class AppleAlbumResponseModel
    {
        public string Id { get; set; }
        public AttributesData Attributes { get; set; }
        public RelationshipsData Relationships { get; set; }

        public class AttributesData
        {
            public string Name { get; set; }
            public string ArtistName { get; set; }
            public string Upc { get; set; }
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
        }

        public class RelationshipData
        {
            public string Id { get; set; }
        }
    }
}

