using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using TT.Deliveries.Data.Models;
using TT.Deliveries.Data.Repositories;

namespace TT.Deliveries.Web.Api.Controllers
{
    [ApiController]
    [Route("api/deliveries")]
    public class DeliveriesController : ControllerBase
    {
        private readonly DeliveryRepository _deliveryRepository;

        public DeliveriesController(DeliveryRepository deliveryRepository)
        {
            this._deliveryRepository = deliveryRepository;
        }

        [HttpGet]
        public IActionResult GetAllDeliveries()
        {
            return Ok(_deliveryRepository.GetAllDeliveries());
        }

        [HttpGet("{id}")]
        public IActionResult GetDeliveryById(Guid id)
        {
            var delivery = _deliveryRepository.GetDeliveryById(id);
            if (delivery == null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }

        [HttpPost]
        public IActionResult CreateDelivery(Delivery delivery)
        {
            _deliveryRepository.AddDelivery(delivery);
            return CreatedAtAction(nameof(GetDeliveryById), new { id = delivery.Id }, delivery);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDelivery(Guid id, Delivery updatedDelivery)
        {
            var result = _deliveryRepository.UpdateDelivery(updatedDelivery);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDelivery(Guid id)
        {
            var result = _deliveryRepository.DeleteDelivery(id);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        [Route("approve")]

        public IActionResult ApproveDelivery(Guid id)
        {
            var result = _deliveryRepository.ApproveDelivery(id);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        [Route("complete")]

        public IActionResult CompleteDelivery(Guid id)
        {
            //Partner may complete a delivery, that is already in approved state.
            var result = _deliveryRepository.CompleteDelivery(id);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        [Route("cancel")]

        public IActionResult CancelDelivery(Guid id)
        {
            //Either the partner or the user should be able to cancel a pending delivery (in state created or approved).

            var result = _deliveryRepository.CancelDelivery(id);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }


    }

}
