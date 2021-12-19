using System.Threading.Tasks;
using ListingApi.Data;
using ListingApi.Model.List;
using ListingApi.Repository.ListRepo;
using Microsoft.AspNetCore.Mvc;

namespace ListingApi.Controllers.List {
    [ApiController]
    [Route ("api/[controller]")]
    public class ListController : ControllerBase {
        private readonly IListItem _listrepo;
        private readonly DataContext _context;
        public ListController (IListItem listrepo, DataContext context) {
            _listrepo = listrepo;
            _context = context;
        }
        //Register Method api/Job/AddJob
        [HttpPost ("AddList")]
        public async Task<IActionResult> AddList ([FromBody] ListItem list) {
            // Checking Duplicate Entry
            try {
                // validate request
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                var CreatedUser = await _listrepo.AddAuth (list);

                return Ok (CreatedUser);
            } catch (System.Exception ex) {
                throw new System.Exception ("Error in saving User data.", ex);
            }
        }

        [HttpPost ("AddMasterList")]
        public async Task<IActionResult> AddMasterList ([FromBody] MasterList list) {
            // Checking Duplicate Entry
            try {
                // validate request
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                var CreatedUser = await _listrepo.AddMasterList (list);

                return Ok (CreatedUser);
            } catch (System.Exception ex) {
                throw new System.Exception ("Error in saving Master List data.", ex);
            }
        }

        [HttpGet ("GetAllListItems/{userId}/{id}")]
        public async Task<IActionResult> GetList (int userId, int id) {
            var list = await _listrepo.getListByMasterListId (userId, id);
            return Ok (list);
        }

        [HttpGet ("GetListItems/{id}")]
        public async Task<IActionResult> GetListItems (int id) {
            var list = await _listrepo.getListById (id);
            return Ok (list);
        }
          [HttpGet ("GetMasterListItemsById/{id}")]
        public async Task<IActionResult> GetMasterListItemsById (int id) {
            var list = await _listrepo.getMasterListById (id);
            return Ok (list);
        }

        [HttpGet ("GetMasterListItems/{userId}")]
        public async Task<IActionResult> GetMasterListItems (int userId) {
            var list = await _listrepo.getMasterListByUserId (userId);
            
            return Ok (list);
        }

        //Register Method api/Job/AddJob
        [HttpPost ("Update/{id}")]
        public async Task<IActionResult> UpdateList (int id, [FromBody] ListItem list) {
            // Checking Duplicate Entry
            try {
                // validate request
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                var CreatedUser = await _listrepo.getListById (id);
                CreatedUser.Name = list.Name;
                CreatedUser.Hash=list.Hash;
                CreatedUser.Host=list.Host;
                CreatedUser.HostName=list.HostName;
                CreatedUser.Origin=list.Origin;
                CreatedUser.PathName=list.PathName;
                _context.listItem.Update (CreatedUser);
                await _context.SaveChangesAsync ();
                return Ok (CreatedUser);
            } catch (System.Exception ex) {
                throw new System.Exception ("Error in saving User data.", ex);
            }
        }

 
        [HttpPost ("UpdateMaster/{id}")]
        public async Task<IActionResult> UpdateMaster (int id, [FromBody] MasterList list) {
             
            try {
                // validate request
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                var CreatedUser = await _listrepo.getMasterListById (id);
                CreatedUser.Name = list.Name;
               
                _context.masterList.Update (CreatedUser);
                await _context.SaveChangesAsync ();
                return Ok (CreatedUser);
            } catch (System.Exception ex) {
                throw new System.Exception ("Error in saving User data.", ex);
            }
        }


        
        [HttpPost ("Delete/{id}")]
        public async Task<IActionResult> Delete (int id) {
            try {
                var CreatedUser = await _listrepo.getListById (id);
                _context.listItem.Remove (CreatedUser);
                await _context.SaveChangesAsync ();
                return Ok (CreatedUser);
            } catch (System.Exception ex) {
                throw new System.Exception ("Error in saving User data.", ex);
            }
        }
    
      [HttpPost ("MasterDeleteDelete/{id}")]
        public async Task<IActionResult> MasterDelete (int id) {
            try {
                var CreatedList = await _listrepo.getMasterListById (id);
                _context.masterList.Remove (CreatedList);
                await _context.SaveChangesAsync ();
                return Ok (CreatedList);
            } catch (System.Exception ex) {
                throw new System.Exception ("Error in saving User data.", ex);
            }
        }
    
    }
}