using System;
namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class AppleAlbumListResponseModel : ListResponseModel<AppleAlbumResponseModel>
	{
        public static AppleAlbumListResponseModel Empty => new AppleAlbumListResponseModel
        {
            Data = new AppleAlbumResponseModel[0]
        };
    }
}