using ITCanCook_BusinessObject.ServiceModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace ITCanCook.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReciveSMSController : TwilioController
	{
		[HttpPost("SendReply")]
		public TwiMLResult SendReply([FromForm] TwilioModel request)
		{
			var response = new MessagingResponse();
			response.Message("This is me replying from API");
			return TwiML(response);
		}
	}
}
