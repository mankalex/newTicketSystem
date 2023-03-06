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
  }
} while (choice == "1" || choice == "2");

logger.Info("Program ended");