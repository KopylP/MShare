using System;
namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class AppleAlbumListResponseModel : ListResponseModel<AppleAlbumResponseModel>
	{
        public static AppleAlbumListResponseModel Empty => new AppleAlbumListResponseModel
        {
            Results = new AppleAlbumResponseModel[0]
        };
    }
}