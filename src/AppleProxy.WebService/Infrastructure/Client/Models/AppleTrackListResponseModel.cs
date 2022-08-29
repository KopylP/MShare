namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class AppleTrackListResponseModel
	{
		public static AppleTrackListResponseModel Empty => new AppleTrackListResponseModel
		{
			Results = new AppleTrackResponseModel[0]
		};

        public AppleTrackResponseModel[] Results { get; set; }
	}
}

