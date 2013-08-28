namespace Players.ViewModels
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [CollectionDataContract(Name = "Videos", ItemName = "Video")]
    public class ApiAssetItemListViewModel : List<ApiAssetItemViewModel>
    {
    }
}