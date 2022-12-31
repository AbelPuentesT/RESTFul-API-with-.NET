using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Interfaces;
using SocialMediaApi.Responses;

namespace SocialMediaApi.Controllers
{
    [Authorize(Roles =nameof(RoleType.Administrator))]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public SecurityController(ISecurityService securityService, IMapper mapper, IPasswordService passwordService)
        {
            _securityService=securityService;
            _mapper=mapper;
            _passwordService = passwordService;
    
        }
        [HttpPost]
        public async Task<IActionResult> PostPost(SecurityDTO securitytDTO)
        {
            var security = _mapper.Map<Security>(securitytDTO);
            security.Password=_passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);
            securitytDTO = _mapper.Map<SecurityDTO>(security);
            var response = new ApiResponse<SecurityDTO>(securitytDTO);
            return Ok(response);
        }
    }
}
