using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Model;

namespace MinimalChatApplicationAPI.Service
{
    public interface IMessageService
    {
        public Task<ResMessageDto> UpsertMessageAsync(MessageDto messageDto, Guid senderId);
        public Task<ResMessageDto?> EditMessageAsync(string testMesage, string messageId, Guid senderId);
        public Task<Message?> DeleteMessageAsync(string messageId, Guid senderId);
        public Task<List<ResMessageDto>?> GetConversationHistoryAsync(MessageHistoryDto messageHistoryDto, Guid senderId, Guid receiverId);
    }
}
