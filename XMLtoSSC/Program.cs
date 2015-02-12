using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.IO.Ports;
using SSCSharp;

namespace SSCSharpSequencer
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
            //only starts at COM0 so it will compile - no way it will get past poorly-written XML
            ServoController controller = new ServoController("COM0");
            XmlReader commandReader = XmlReader.Create("commands.xml");
            commandReader.Read();
            if (commandReader.GetAttribute("port") == null)
            {
                Console.WriteLine("Please specify a port in your top-level servocontrol element.");
                Environment.Exit(0);
            }
            else if (SerialPort.GetPortNames().Contains(commandReader.GetAttribute("port")))
            {
                controller = new ServoController(commandReader.GetAttribute("port"));
            }
            else
            {
                Console.WriteLine("The serial port you've specified is not connected.");
                Environment.Exit(0);
            }
            while (commandReader.Read())
            {
                if (commandReader.NodeType == XmlNodeType.Element)
                {
                    if(commandReader.Name == "movement"){
                        if (commandReader.HasAttributes)
                        {
                            int position = Convert.ToInt32(commandReader.GetAttribute("position"));
                            int time = 0;
                            if (commandReader.GetAttribute("time") != null)
                            {
                                time = Convert.ToInt32(commandReader.GetAttribute("time"));
                            }
                            if (commandReader.GetAttribute("servo") == "all")
                            {
                                controller.setServoPositions(new int[] { 0, 1, 2, 3, 4, 5 }, position);
                            }
                            else
                            {
                                int servoNumber = Convert.ToInt32(commandReader.GetAttribute("servo"));
                                controller.setServoPosition(servoNumber, position, time);
                            }
                        }
                    }
                    else if (commandReader.Name == "wait")
                    {
                        if (commandReader.HasAttributes)
                        {
                            int milliseconds = Convert.ToInt32(commandReader.GetAttribute("milliseconds"));
                            System.Threading.Thread.Sleep(milliseconds);
                        }
                    }
                    else if (commandReader.Name == "print")
                    {
                        Console.WriteLine(commandReader.ReadString());
                    }
                }
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
