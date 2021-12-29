using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Error;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Iterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signInMnager;
        public ITokenService _tokenService { get; }
        public IMapper _mapper { get; }
        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInMnager, IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _signInMnager = signInMnager;
            _usermanager = usermanager;
            _mapper = mapper;

        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUSer()
        {

            var user = await _usermanager.FindByEmailClaimsPrincipale(HttpContext.User);
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                UserName = user.DisplayName,
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _usermanager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
        {
            var user = await _usermanager.findUSerByEmailWithAddress(HttpContext.User);

            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto adress)
        {
            var user = await _usermanager.findUSerByEmailWithAddress(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(adress);
            var result = await _usermanager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest("error");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _usermanager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInMnager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return new UserDto
            {
                Email = user.Email,
                UserName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };
            var result = await _usermanager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return new UserDto
            {
                Email = user.Email,
                UserName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}