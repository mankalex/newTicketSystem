using NLog;
public class TicketFile
{
  // public property
  public string filePath { get; set; }
  public List<Ticket> Tickets { get; set; }
  private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();


  // constructor is a special method that is invoked
  // when an instance of a class is created
  public TicketFile(string ticketFilePath)
  {
    filePath = ticketFilePath;
    // create instance of Logger
    

    Tickets = new List<Ticket>();

    // to populate the list with data, read from the data file
    try
    {
      StreamReader sr = new StreamReader(filePath);
      // first line contains column headers
      sr.ReadLine();
      while (!sr.EndOfStream)
      {
        // create instance of Movie class
        Ticket ticket = new Ticket();
        string line = sr.ReadLine();
        // first look for quote(") in string
        // this indicates a comma(,) in ticket title
        int idx = line.IndexOf('"');
        if (idx == -1)
        {
          // no quote = no comma in ticket title
          // title details are separated with comma(,)
          string[] ticketDetails = line.Split(',');
          ticket.TicketID = UInt64.Parse(ticketDetails[0]);
          ticket.Summary = ticketDetails[1];
          ticket.Status = ticketDetails[2];
          ticket.Priority = ticketDetails[3];
          ticket.Submitter = ticketDetails[4];
          ticket.Assigned = ticketDetails[5];
          ticket.Watching = ticketDetails[6].Split('|').ToList();
        }
        else
        {
          // quote = comma in ticket title
          // extract the ticketId
          ticket.TicketID = UInt64.Parse(line.Substring(0, idx - 1));
          // remove movieId and first quote from string
          line = line.Substring(idx + 1);
          // find the next quote
          idx = line.IndexOf('"');
          // extract the summary
          ticket.Summary = line.Substring(0, idx);
          // remove summary and last comma from the string
          line = line.Substring(idx + 2);

          idx = line.IndexOf('"');
          // extract the status
          ticket.Status = line.Substring(0, idx);
          // remove status and last comma from the string
          line = line.Substring(idx + 3);

          idx = line.IndexOf('"');
          // extract the summary
          ticket.Priority = line.Substring(0, idx);
          // remove summary and last comma from the string
          line = line.Substring(idx + 4);

          idx = line.IndexOf('"');
          // extract the summary
          ticket.Submitter = line.Substring(0, idx);
          // remove summary and last comma from the string
          line = line.Substring(idx + 5);

          idx = line.IndexOf('"');
          // extract the summary
          ticket.Assigned = line.Substring(0, idx);
          // remove summary and last comma from the string
          line = line.Substring(idx + 6);
          ticket.Watching = line.Split('|').ToList();
        }
        Tickets.Add(ticket);
      }
      // close file when done
      sr.Close();
      logger.Info("Tickets in file {Count}", Tickets.Count);
    }
    catch (Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
  public void AddTicket(Ticket ticket)
  {
    try
    {
      // first generate movie id
      ticket.TicketID = Tickets.Max(m => m.TicketID) + 1;
      StreamWriter sw = new StreamWriter(filePath, true);
      sw.WriteLine($"{ticket.TicketID},{ticket.Summary},{ticket.Status},{ticket.Priority},{ticket.Submitter},{ticket.Assigned},{string.Join("|", ticket.Watching)}");
      sw.Close();
      // add movie details to Lists
      Tickets.Add(ticket);
      // log transaction
      logger.Info("Ticket id {Id} added", ticket.TicketID);
    } 
    catch(Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
}