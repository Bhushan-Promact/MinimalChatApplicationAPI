using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalChatApplicationAPI.CustomExceptions;
using MinimalChatApplicationAPI.Data;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Model;

namespace MinimalChatApplicationAPI.Service
{
    public class MessageService : IMessageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessageService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }

        public async Task<ResMessageDto> UpsertMessageAsync(MessageDto messageDto, Guid senderId)
        {
            ResMessageDto resMessage = new()
            {
                SenderId = senderId,
                ReceiverId = messageDto.ReceiverId,
                TextMessage = messageDto.TextMessage,
                TimeStamp = DateTime.UtcNow
            };

            var mapMessage = _mapper.Map<Message>(resMessage);
            var res = await _context.Messages.AddAsync(mapMessage);
            await _context.SaveChangesAsync();
            var mapResMessage = _mapper.Map<ResMessageDto>(res.Entity);

            return mapResMessage;
        }

        public async Task<ResMessageDto?> EditMessageAsync(string messageId, string textMesage, Guid senderId)
        {
            Message? message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageId.ToString() == messageId && x.SenderId == senderId);
            if (message != null)
            {
                message.TextMessage = textMesage;
                var res = _context.Messages.Update(message);
                await _context.SaveChangesAsync();
                var mapResMessage = _mapper.Map<ResMessageDto>(res.Entity);
                return mapResMessage;
            }
            else
                throw new NotFoundException("Message not found");
        }

        public async Task<Message?> DeleteMessageAsync(string messageId, Guid senderId)
        {
            Message? message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageId.ToString() == messageId && x.SenderId == senderId);
            if (message != null)
            {
                var res = _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            else
                throw new NotFoundException("Message not found");
        }

        public async Task<List<ResMessageDto>?> GetConversationHistoryAsync(MessageHistoryDto messageHistoryDto, Guid senderId, Guid receiverId)
        {
            List<Message> msgList = await _context.Messages.Where(x => (
            (x.SenderId == senderId && x.ReceiverId == receiverId) ||
            (x.SenderId == receiverId && x.ReceiverId == senderId)) && (x.TimeStamp < messageHistoryDto.Before)).ToListAsync();

            if (msgList.Count == 0)
            {
                throw new NotFoundException("User or conversation not found");
            }
            else
            {
                if (messageHistoryDto.Sort.ToLower() == "asc")
                {
                    var res = msgList.OrderBy(x => x.TimeStamp).Take(messageHistoryDto.Count);
                    return _mapper.Map<List<ResMessageDto>>(res);
                }
                else if (messageHistoryDto.Sort.ToLower() == "desc")
                {
                    var res = msgList.OrderByDescending(x => x.TimeStamp).Take(messageHistoryDto.Count);
                    return _mapper.Map<List<ResMessageDto>>(res);
                }
                throw new BadDataException("Invalid request parameters");
            }
        }
    }
}
