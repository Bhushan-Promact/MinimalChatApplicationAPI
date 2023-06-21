using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalChatApplicationAPI.CustomExceptions;
using MinimalChatApplicationAPI.Data;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Model;
using MinimalChatApplicationAPI.Utils;
using MinimalChatApplicationAPI.Utils.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalChatApplicationAPI.Service
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<ResUserRegistrationDto>?> GetUsersAsync(string userID)
        {
            User? user = await _context.Users.FindAsync(userID);
            if (user != null)
            {
                List<User> userList = await _context.Users.ToListAsync();
                if (userList.IsListNullOrEmpty())
                {
                    throw new NoContentException("List is Empty");
                }
                else
                {
                    var userDto = _mapper.Map<List<ResUserRegistrationDto>>(userList);
                    return userDto;
                }                
            }
            else
            {
                throw new UnauthorizedException("Unauthorized Access");
            }
        }

        public async Task<ResUserRegistrationDto?> UpsertUser(UserRegistrationDto userDto)
        {
            var isEmailExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);
            if (isEmailExist == null)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                userDto.Password = passwordHash;
                var mapUser = _mapper.Map<User>(userDto);
                var response = await _context.Users.AddAsync(mapUser);
                await _context.SaveChangesAsync();
                return _mapper.Map<ResUserRegistrationDto>(response.Entity);
            }
            else
            {
                throw new ConflictException("Registration failed because the email is already registered");
            }
        }

        public async Task<ResUserLoginDto?> LoginUserAsync(UserLoginDto userLoginDto)
        {
            ResUserLoginDto resUser = new();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userLoginDto.Email);
            if (user == null)
            {
                throw new BadDataException("User Not Found");
            }
            if (user == null && (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user!.Password)))
            {
                throw new UnauthorizedException("Unauthorized Access");
            }
            var mapUser = _mapper.Map<ResUserRegistrationDto>(user);
            string token = CreateToken(mapUser);
            resUser.Token = token;
            resUser.User = mapUser;
            return resUser;
        }

        private string CreateToken(ResUserRegistrationDto resUserRegistrationDto)
        {
            byte[] key = Encoding.ASCII.GetBytes(_configuration["UserSettings:Jwt:Key"]!);
            string expiryInMinutes = _configuration["UserSettings:Jwt:ExpiryInMinutes"] ?? "120";
            DateTime expiryDateTimeInUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(expiryInMinutes));
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new List<Claim>() { new Claim(JWTClaimTypes.Id.ToString(), resUserRegistrationDto.UserId) }),
                Expires = expiryDateTimeInUtc,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
