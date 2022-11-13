namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class AppleTrackListResponseModel : ListResponseModel<AppleTrackResponseModel>
    {
		public static AppleTrackListResponseModel Empty => new AppleTrackListResponseModel
		{
			Data = new AppleTrackResponseModel[0]
		};
	}
}

