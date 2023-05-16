using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotifyChat.SignalR.Persistence.Repositories;

namespace NotifyChat.SignalR.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Checked(Guid id)
        {
            var notification = await _notificationRepository.GetById(id);
            notification.SetChecked();
            await _notificationRepository.Update(notification);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Clear(List<Guid> ids)
        {
            await _notificationRepository.Delete(ids);
            return Ok();
        }

    }
}
