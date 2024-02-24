using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Services;

namespace CL.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService) => _clientService = clientService;

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clientService.GetAllClients());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientById(id);

            if (client == null)
            {
                return NotFound($"Client Not Found Client id: {id}");
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(Client client)
        {
            return Ok(await _clientService.AddClient(client));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateClient(Client client)
        {
            return Ok(await _clientService.UpdateClient(client));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            return Ok(await _clientService.DeleteClient(id));
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterClientsByEmail(string email)
        {
            return Ok(await _clientService.FiltersClientsByEmail(email));
        }
    }
}
