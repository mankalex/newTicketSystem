public class Ticket
{
  public UInt64 TicketID { get; set; }
  public string Summary { get; set; }
  public string Status { get; set; }
  public string Priority { get; set; }
  public string Submitter { get; set; }
  public string Assigned { get; set; }
  public List<string> Watching { get; set; }

}