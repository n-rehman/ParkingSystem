using Microsoft.AspNetCore.Mvc;
using ParkingSystem.ApplicationLayer.Interfaces;
using ParkingSystem.Common.Requests;
using System.Collections.Generic;
using System.Text;

namespace ParkingSystemAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ParkingController : ControllerBase
	{
		private readonly IParkingService _parkingService;

		public ParkingController(IParkingService parkingService)
		{
			_parkingService = parkingService;
		}

		[HttpGet("GetParkingSpaceAvailablity")]
		public ActionResult GetParkingSpaceAvailablity([FromQuery] string dateFrom, string dateTo)
		{
			AvailableReservationRequest request = new AvailableReservationRequest { DateFrom = Convert.ToDateTime(dateFrom), DateTo = Convert.ToDateTime(dateTo) };

			var availaiblityResult = _parkingService.GetParkingSpaceAvailablity(request);


			return Ok(new
			{
				value = availaiblityResult
			});
		}

		[HttpPost("BookReservation")]
		public ActionResult BookReservation(BookReservationRequest bookingRequest)
		{
			var data = _parkingService.BookReservation(bookingRequest);
			return Ok(data);
		}

		[HttpPost("CancelReservation")]
		public ActionResult CancelReservation(CancelReservationRequest cancellationRequest)
		{
			var data = _parkingService.CancelReservation(cancellationRequest);
			return Ok(data);
		}

		[HttpPost("EditReservation")]
		public ActionResult AmendReservation(EditReservationRequest editRequest)
		{
			var data = _parkingService.EditReservation(editRequest);
			return Ok(data);
		}

		[HttpGet("GetReservationPrice")]
		public ActionResult PriceDetails([FromQuery] string Datefrom, string DateTo)
		{
			var data = _parkingService.GetParkingCharge(Convert.ToDateTime(Datefrom), Convert.ToDateTime(DateTo));
			return Ok(data);
		}
	}
}
