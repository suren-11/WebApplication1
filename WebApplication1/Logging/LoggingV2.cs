namespace WebApplication1.Logging
{
    public class LoggingV2 : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("error : " + message);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (type == "warnning")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("danger : " + message);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
