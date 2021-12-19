using System.Threading.Tasks;
using AutoMapper;
using ListingApi.Dtos;
using ListingApi.Model.Auth;
using ListingApi.Model.List;
using ListingApi.Reponces.Auth;
using ListingApi.Repository.AuthenticationRepo;
using ListingApi.Repository.ListRepo;
using Microsoft.AspNetCore.Mvc;

namespace ListingApi.Controllers.Authentication {
    [ApiController]
    [Route ("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IAuthRepo _authrepo;
        private readonly IListItem _listrepo;
        private AuthResponces _responces;
        private readonly IMapper _mapper;
        public AuthController (IAuthRepo authrepo, IListItem listrepo, AuthResponces responces, IMapper mapper) {
            _authrepo = authrepo;
            _responces = responces;
            _listrepo = listrepo;
            _mapper = mapper;
        }
        //Register Method api/Job/AddJob
        [HttpPost ("AddAuthUser")]
        public async Task<IActionResult> AddAuthUser ([FromBody] AuthUser authUser) {
            // Checking Duplicate Entry
            try {
                if (await _authrepo.IsAuthUserExist (authUser.Email)) {
                    var authUsers = await _authrepo.getAuthUserByEmail (authUser.Email);
                    if (authUsers == null)
                        return NoContent ();

                    //Getting User Role In Application

                    _responces.Status = 200;
                    _responces.Success = true;
                    _responces.Status_Message = "User with this email already exist!";
                    var userDtos = new UserDtos () {
                        Id = authUsers.Id,
                        Email = authUsers.Email,
                        Name = authUsers.Name,
                        UserName = authUsers.UserName,

                        ImageUrl = authUsers.ImageUrl
                    };
                    _responces.data = userDtos;

                    return Ok (_responces);
                }
                // validate request
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                var CreatedUser = await _authrepo.AddAuth (authUser);

                //Add Master List for this User
                MasterList msl = new MasterList ();
                msl.Name = "My List 1";
                msl.UserId = CreatedUser.Id;
                msl.CanDelete=false;
               await _listrepo.AddMasterList(msl);


                _responces.Status = 200;
                _responces.Success = true;
                _responces.Status_Message = "User created successfully!";
                _responces.data = _mapper.Map<UserDtos> (authUser);
                return Ok (_responces);
            } catch (System.Exception ex) {
                throw new System.Exception ("Error in saving User data.", ex);
            }
        }

        // Get Profile Data By Id

        [HttpGet ("GetUserProfile/{userId}")]
        public async Task<ActionResult> GetUserProfile (int userId) {
            var authUsers = await _authrepo.getAuthById (userId);

            //Getting User Role In Application

            var returnData = new UserDtos () {
                Id = authUsers.Id,
                Email = authUsers.Email,

                Name = authUsers.Name,
                UserName = authUsers.UserName,
                ImageUrl = authUsers.ImageUrl
            };
            return Ok (returnData);
        }

    }
}