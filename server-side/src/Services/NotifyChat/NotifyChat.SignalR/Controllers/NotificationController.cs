using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Settings;

namespace NotifyChat.SignalR.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMongoCollection<Notification<object>> _notificationsCollection;

        public NotificationController(IMongoDbFactory mongoDbFactory)
        {
            _notificationsCollection = mongoDbFactory.GetCollection<Notification<object>>("notifications");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Checked(Guid id)
        {
            var filter = Builders<Notification<object>>.Filter.Eq(e => e.Id, id);
            var notification = await (await _notificationsCollection.FindAsync(filter)).FirstOrDefaultAsync();

            notification.SetChecked();
            await _notificationsCollection.ReplaceOneAsync(filter, notification);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Clear(List<Guid> ids)
        {
            var filter = Builders<Notification<object>>.Filter.In(e => e.Id, ids);
            await _notificationsCollection.DeleteManyAsync(filter);

            return Ok();
        }

    }
}
