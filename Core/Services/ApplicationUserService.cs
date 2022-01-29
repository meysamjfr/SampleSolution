using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Project.Data;
using Project.DTOs.ApplicationUsers;
using Project.DTOs.ApplicationUsers.Validators;
using Project.DTOs.Datatable.ApplicationUsers;
using Project.DTOs.Datatable.Base;
using Project.Entities;
using Project.Exceptions;
using Project.Extentions;
using Project.Helpers;
using Project.Models;
using Project.Repositories;

namespace Project.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ApplicationUserService(ApplicationDbContext dbContext, IOptions<JwtSettings> jwtSettings, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationUserDTO Current()
        {
            return (ApplicationUserDTO)_httpContextAccessor.HttpContext.Items["ApplicationUser"];
        }

        public async Task<ApplicationUserDTO> GetById(int id)
        {
            var findUser = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(f => f.Id == id);
            if (findUser == null)
            {
                return null;
            }
            return _mapper.Map<ApplicationUserDTO>(findUser);
        }

        public async Task<bool> Login(LoginApplicationUserDTO login)
        {
            var findUser = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(f => f.Phone == login.Phone);
            if (findUser == null)
            {
                findUser = _mapper.Map<ApplicationUser>(login);
                findUser.Token = GenerateToken(findUser);

                await _dbContext.AddAsync(findUser);
            }

            findUser.VerificationCode = PublicHelper.GetRandomInt();

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ApplicationUserDTO> Verify(VerifyApplicationUserDTO verify)
        {
            var findUser = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(f => f.Phone == verify.Phone && f.VerificationCode == verify.VerificationCode);
            if (findUser == null)
            {
                throw new BadRequestException("اطلاعات کاربری اشتباه است");
            }
            else
            {
                findUser.Token = GenerateToken(findUser);
                findUser.LastLogin = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return _mapper.Map<ApplicationUserDTO>(findUser);
            }
        }

        public async Task<ApplicationUserDTO> EditProfile(EditProfileApplicationUserDTO editProfile)
        {
            var validator = new EditProfileApplicationUserDtoValidator();
            var validationResult = validator.Validate(editProfile);
            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var findUser = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(f => f.Id == editProfile.Id);

            if (findUser == null)
            {
                throw new NotFoundException("کاربر پیدا نشد");
            }

            findUser = _mapper.Map<ApplicationUser>(editProfile);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ApplicationUserDTO>(findUser);

        }

        public async Task<DatatableResponse<ApplicationUserDTO>> Datatable(ApplicationUserDatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _dbContext.ApplicationUsers
                .GetData(input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.GetNickName().ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower()) ||
                    w.Email.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower()) ||
                    w.Phone.ToString().Contains(filtersFromRequest.SearchValue.Trim().ToLower())
                );
            }

            if (!string.IsNullOrEmpty(input.Name))
            {
                data = data.Where(w => w.GetNickName().ToLower().Contains(input.Name.Trim().ToLower()));
            }

            return await _mapper.Map<IQueryable<ApplicationUserDTO>>(data).ToDataTableAsync(totalRecords, filtersFromRequest);

        }

        private string GenerateToken(ApplicationUser user)
        {
            var securityTokenHandler = new JwtSecurityTokenHandler();
            //var userClaims = await _userManager.GetClaimsAsync(user);
            //var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            //for (int i = 0; i < roles.Count; i++)
            //{
            //    roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            //}

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.GetNickName()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Phone),
                new Claim("uid", user.Id.ToString())
            }
            //.Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return securityTokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
