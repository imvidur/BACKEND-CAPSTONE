using FitnessWorkoutMgmnt.Data;
using FitnessWorkoutMgmnt.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessWorkoutMgmnt.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly FitnessDbContext _context;

        public MessageRepository(FitnessDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetMessagesBetweenUsers(int senderId)
        {
            return await _context.Messages
            .Where(m => m.SenderId == senderId)
            .OrderByDescending(m => m.DateSent)
            .ToListAsync();
        }

        public async Task<Message> SendMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }
}
