using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using it_center_naloga.Models;

namespace it_center_naloga.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string responseMessage = null)
    {
        ViewBag.ResponseMessage = responseMessage;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpPost]
    public async Task<ActionResult> Index(Ticket ticket)
    {
        using (var client = new HttpClient())
        {
            var values = new Dictionary<string, string>
            {
                { "sID",  "838"},
                { "Name", ticket.Name },
                { "Surname", ticket.Surname },
                { "Address", ticket.Address },
                { "Email", ticket.Email },
                { "Phone", ticket.Phone },
                { "TicketCategory", ticket.TicketCategory.ToString() },
                { "TicketContent", ticket.TicketContent }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://cys-online.com/api/client/SendTicketPostWithoutClient", content);
            var responseString = await response.Content.ReadAsStringAsync();

            responseString = responseString.Trim('"');

            // Success, ce je uspesno (= 1), drugace pa vrne error response string
            if (responseString == "1")
            {
                responseString = "Success";
            }
            
            ViewBag.Response = responseString;
        }

        return View("Index");
    }
}