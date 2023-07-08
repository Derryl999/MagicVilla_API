namespace MagicVilla_VillaAPI.Logging
{
    public class Logging : ILogging
    {
        public void Log(string type , string message)
        {
            if(type == "Error")
            {
                Console.WriteLine("Error " + message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
