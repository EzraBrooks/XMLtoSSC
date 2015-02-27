using System;

namespace EzraBrooks.XMLtoSSC
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                usage();
                return;
            }
            try
            {
                CommandReader mainReader = new CommandReader(args[0]);
                mainReader.run();
            }
            catch (CommandReaderException e)
            {
                string source = e.GetType().Name;
                if (e.InnerException != null)
                {
                    source += " from " + e.InnerException.GetType().Name;
                }
                Console.WriteLine(e.Message + " (" + source + ")");
                Environment.Exit(255);
            }
            Console.WriteLine();
        }
        static void usage()
        {
            Console.WriteLine("XMLtoSSC - Control an SSC-32 with XML.");
            Console.WriteLine("Usage - XMLtoSSC.exe [control file]");
        }
    }
}
