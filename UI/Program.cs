//Initialize logger and start of program
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(@"..\DL\logFile.txt")
    .CreateLogger();
MenuFactory.GetMenu("login").Start();