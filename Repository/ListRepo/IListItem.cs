using System.Collections.Generic;
using System.Threading.Tasks;
using ListingApi.Model.List;

namespace ListingApi.Repository.ListRepo {
    public interface IListItem {
        Task<ListItem> AddAuth (ListItem list);
        Task<MasterList> AddMasterList (MasterList list);
        Task<List<ListItem>> getListByMasterListId (int userId, int id);
        Task<ListItem> getListById (int Id);
         Task<MasterList> getMasterListById (int Id);
        Task<List<MasterList>> getMasterListByUserId (int userId); 
    }
}