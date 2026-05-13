using Microsoft.AspNetCore.Mvc;
using Clayton_CustomerMicroservice.Data;
using Clayton_CustomerMicroservice.Models;
using Clayton_CustomerMicroservice.DTOs;

namespace Clayton_CustomerMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // REGISTER
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var existingUser = _context.Users
                .FirstOrDefault(x => x.Email == dto.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already exists.");
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = dto.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

        // LOGIN
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(x =>
                    x.Email == dto.Email &&
                    x.Password == dto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(user);
        }

        // GET USER DETAILS
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        // GET USER NOTIFICATIONS
        [HttpGet("{id}/notifications")]
        public IActionResult GetNotifications(int id)
        {
            var notifications = _context.Notifications
                .Where(x => x.UserId == id)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return Ok(notifications);
        }

        // ADD NOTIFICATION
        [HttpPost("{id}/notifications")]
        public IActionResult AddNotification(int id, Notification notification)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            notification.UserId = id;
            notification.CreatedAt = DateTime.Now;

            _context.Notifications.Add(notification);
            _context.SaveChanges();

            return Ok("Notification added.");
        }
    }
}