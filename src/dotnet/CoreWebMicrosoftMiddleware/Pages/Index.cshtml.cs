using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWebMicrosoftMiddleware.Pages
{
	public class IndexModel : PageModel
	{

		public string Id { get; set; }
		
		public void OnGet()
		{
			if (User.Identity.IsAuthenticated)
			{
				Id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			}
		}
	}
}
