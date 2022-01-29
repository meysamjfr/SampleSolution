using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Project.DTOs.ExtendedUser;
using Project.Entities;
using Project.Exceptions;
using Project.Helpers;
using Project.Repositories;

namespace Project.Services
{
    public class ExtendedUserService : IExtendedUserService
    {
        private readonly SignInManager<ExtendedUser> _signInManager;
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public ExtendedUserService(SignInManager<ExtendedUser> signInManager, UserManager<ExtendedUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ExtendedUser> Login(LoginExtendedUserDTO user)
        {
            var findUser = await _userManager.FindByNameAsync(user.Username);

            var signInResult = await _signInManager.PasswordSignInAsync(findUser, user.Password, user.RememberMe, false);

            if (signInResult.Succeeded)
            {
                findUser.LastLogin = DateTime.Now;
                await _userManager.UpdateAsync(findUser);
                return findUser;
            }

            throw new BadRequestException();
        }

        public async Task<ExtendedUser> Register(RegisterExtendedUserDTO user)
        {
            if (await _userManager.FindByNameAsync(user.UserName) != null)
            {
                throw new BadRequestException($"نام کاربری {user.UserName} از قبل وجود دارد");
            }
            if (await _userManager.FindByEmailAsync(user.Email) != null)
            {
                throw new BadRequestException($"ایمیل {user.Email} از قبل وجود دارد");
            }
            var newUser = _mapper.Map<ExtendedUser>(user);

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                foreach (var role in user.Roles)
                {
                    if (await _roleManager.RoleExistsAsync(role) == false)
                    {
                        await _roleManager.CreateAsync(new IdentityRole { Name = role });
                    }
                    if (await _userManager.IsInRoleAsync(newUser, role) == false)
                    {
                        await _userManager.AddToRoleAsync(newUser, role);
                    }
                }

                return newUser;
            }

            throw new BadRequestException();
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
