string file = "Tickets.csv";
StreamWriter sw = new StreamWriter(file, append: true);
sw.WriteLine("TicketID, Summary, Status, Priority, Submitter, Assigned, Watching");
sw.WriteLine("1,This is a bug ticket,Open,High,Drew Kjell,Jane Doe,Drew Kjell|John Smith|Bill Jones");
sw.Close();
