using FitnessWorkoutMgmnt.Services;
using FitnessWorkoutMgmnt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitnessWorkoutMgmnt.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FitnessWorkoutMgmnt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        private readonly FitnessDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(IConfiguration configuration, JwtService jwtService, FitnessDbContext context)
        {
            //_configuration = configuration;
            _context = context;
            _jwtService = jwtService;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username && u.IsActive);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token, Role = user.Role, Success=true, clientId=user.UserId });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role,
                PhoneNumber=registerDto.PhoneNumber,
                Email = registerDto.Email,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpGet("getData")]
        public IActionResult GetDashboardData()
        {
            var totalUsers = _context.Users.Count(u=>u.Role=="Client");
            var totalTrainers = _context.Users.Count(u => u.Role == "Trainer");
            var totalNutritionists = _context.Users.Count(u => u.Role == "Nutritionist");

            var data = new
            {
                TotalUsers = totalUsers,
                TotalTrainers = totalTrainers,
                TotalNutritionists = totalNutritionists,
                WorkingHours = "6 AM - 10 PM",
                IsGymActive = true
            };

            return Ok(data);
        }

    }
}

