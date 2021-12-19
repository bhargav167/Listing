using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListingApi.Data;
using ListingApi.Model.List;
using Microsoft.EntityFrameworkCore;

namespace ListingApi.Repository.ListRepo {
    public class ListItems : IListItem {
        private readonly DataContext _context;
        public ListItems (DataContext context) {
            _context = context;
        }
        public async Task<ListItem> AddAuth (ListItem list) {
            await _context.listItem.AddAsync (list);
            await _context.SaveChangesAsync ();

            return list;
        }

        public async Task<MasterList> AddMasterList (MasterList list) {
            await _context.masterList.AddAsync (list);
            await _context.SaveChangesAsync ();

            return list;
        }

        public async Task<ListItem> getListById (int Id) {
            var item = await _context.listItem.Where (c => c.Id == Id).FirstOrDefaultAsync ();
            return item;
        }

        public async Task<List<ListItem>> getListByMasterListId (int userId, int id) {
            var item = await _context.listItem.Where (c => c.MasterListId == id && c.UserId == userId).ToListAsync ();
            return (item);
        }

        public async Task<MasterList> getMasterListById (int Id) {
            var item = await _context.masterList.Where (c => c.Id == Id).FirstOrDefaultAsync ();
             item.DateFormat = string.Format("{0:dd MMM yyyy}", item.CreatedDate);
            return item;
        }

        public async Task<List<MasterList>> getMasterListByUserId (int userId) {
            var masterList = await _context.masterList.Where (x => x.UserId == userId).ToListAsync ();
            foreach (var item in masterList) {
                item.DateFormat = string.Format("{0:dd MMM yyyy}", item.CreatedDate);
            }

            return masterList;
        }
    }
}