namespace it_center_naloga.Models;

public class Ticket
{
    public int sID { get; set; } 
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int TicketCategory { get; set; }
    public string TicketContent { get; set; }
}