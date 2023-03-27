using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
string ticketFilePath = Directory.GetCurrentDirectory() + "\\Tickets.csv";

logger.Info("Program started");

TicketFile ticketFile = new TicketFile(ticketFilePath);

string choice = "";
do
{
  // display choices to user
  Console.WriteLine("1) Add Ticket");
  Console.WriteLine("2) Display All Tickets");
  Console.WriteLine("3) Find Ticket");
  Console.WriteLine("Enter to quit");
  // input selection
  choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);
  if (choice == "1")
  {
    Ticket ticket = new Ticket();
    // ask user to input ticket summary
    Console.WriteLine("Enter ticket summary");
    // input summary
    ticket.Summary = Console.ReadLine();
    // ask user to input ticket status
    Console.WriteLine("Enter ticket status");
    // input status
    ticket.Status = Console.ReadLine();
    // ask user to input ticket priority
    Console.WriteLine("Enter ticket priotity");
    // input priority
    ticket.Priority = Console.ReadLine();
    // ask user to input ticket Submitter
    Console.WriteLine("Enter ticket submitter");
    // input submitter
    ticket.Submitter = Console.ReadLine();
    // ask user to input ticket assigned
    Console.WriteLine("Enter ticket assigned");
    // input assigned
    ticket.Assigned = Console.ReadLine();
    string input;
      do
      {
        // ask user to enter watching
        Console.WriteLine("Enter watching (or done to quit)");
        // input watching
        input = Console.ReadLine();
        // if user enters "done"
        // or does not enter a genre do not add it to list
        if (input != "done" && input.Length > 0)
        {
          ticket.Watching.Add(input);
        }
      } while (input != "done");
      // specify if no watching are entered
      if (ticket.Watching.Count == 0)
      {
        ticket.Watching.Add("(no genres listed)");
      }
      // add movie
      ticketFile.AddTicket(ticket);
  } else if (choice == "2")
  {
    // Display All Tickets
    foreach(Ticket m in ticketFile.Tickets)
    {
      Console.WriteLine(m.Display());
    }
  } else if (choice == "3")
  {
    // asks user for input
    Console.WriteLine("1) Status");
    Console.WriteLine("2) Priority");
    Console.WriteLine("3) Submitter");
    string tInput = Console.ReadLine();

    if(tInput == "1")
    {
      //asks user for input
      Console.WriteLine("Enter status");
      var i1 = Console.ReadLine();
      // LINQ - Where filter operator & Select projection operator & Contains quantifier operator
      var status = ticketFile.Tickets.Where(m => m.Status.Contains(i1)).Select(m => m.Status);
      // LINQ - Count aggregation method
      Console.WriteLine($"There are {status.Count()} tickets with \"{i1}\" in the status:");
      foreach(string t in status)
      {
          Console.WriteLine($"  {status}");
      }

    }
    if(tInput == "2")
    {
      //asks user for input
      Console.WriteLine("Enter priority");
      var i2 = Console.ReadLine();
      // LINQ - Where filter operator & Select projection operator & Contains quantifier operator
      var priority = ticketFile.Tickets.Where(m => m.Priority.Contains(i2)).Select(m => m.Priority);
      // LINQ - Count aggregation method
      Console.WriteLine($"There are {priority.Count()} tickets with \"{i2}\" in the priority:");
      foreach(string t in priority)
      {
          Console.WriteLine($"  {priority}");
      }

    }
    if(tInput == "3")
    {
      //asks user for input
      Console.WriteLine("Enter submitter");
      var i3 = Console.ReadLine();
      // LINQ - Where filter operator & Select projection operator & Contains quantifier operator
      var subm = ticketFile.Tickets.Where(m => m.Submitter.Contains(i3)).Select(m => m.Submitter);
      // LINQ - Count aggregation method
      Console.WriteLine($"There are {subm.Count()} tickets with \"{i3}\" in the submitter:");
      foreach(string t in subm)
      {
          Console.WriteLine($"  {subm}");
      }

    }
  }
} while (choice == "1" || choice == "2" || choice == "3");

logger.Info("Program ended");