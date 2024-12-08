using FitnessWorkoutMgmnt.Models;
using FitnessWorkoutMgmnt.Repository;

namespace FitnessWorkoutMgmnt.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<Message>> GetMessagesBetweenUsers(int senderId)
        {
            return await _messageRepository.GetMessagesBetweenUsers(senderId);
        }

        public async Task<Message> SendMessage(Message message)
        {
            return await _messageRepository.SendMessage(message);
        }
    }
}
