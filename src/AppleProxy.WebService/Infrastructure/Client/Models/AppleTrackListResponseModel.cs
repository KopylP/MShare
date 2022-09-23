namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class AppleTrackListResponseModel : ListResponseModel<AppleTrackResponseModel>
    {
		public static AppleTrackListResponseModel Empty => new AppleTrackListResponseModel
		{
			Results = new AppleTrackResponseModel[0]
		};
	}
}

