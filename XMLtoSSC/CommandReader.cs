using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO.Ports;
using EzraBrooks.SSCSharp;

namespace EzraBrooks.XMLtoSSC
{
    /// <summary>
    /// Class for interpreting XML to SSCSharp commands
    /// </summary>
    class CommandReader
    {
        private XmlReader commandFile;
        private ServoController controller;
        /// <summary>
        /// Constructor for CommandReader object
        /// </summary>
        /// <param name="filename">XML file to read</param>
        public CommandReader(string filename)
        {
            try
            {
                commandFile = XmlReader.Create(filename);
            }catch (System.IO.FileNotFoundException e){
                throw new CommandReaderException("The XML file you tried to open cannot be found.", e);
            }
            commandFile.Read();
            if (commandFile.GetAttribute("port") == null)
            {
                throw new CommandReaderException("Please specify a port in your top-level servocontrol element.");
            }
            else if (SerialPort.GetPortNames().Contains(commandFile.GetAttribute("port")))
            {
                controller = new ServoController(commandFile.GetAttribute("port"));
            }
        }

        /// <summary>
        /// Run XmlReader over the XML command file, sending commands.
        /// </summary>
        public void run()
        {
            while (commandFile.Read())
            {
                if (commandFile.NodeType == XmlNodeType.Element)
                {
                    if (commandFile.Name == "movement")
                    {
                        if (commandFile.HasAttributes)
                        {
                            int position = Convert.ToInt32(commandFile.GetAttribute("position"));
                            int time = 0;
                            if (commandFile.GetAttribute("time") != null)
                            {
                                time = Convert.ToInt32(commandFile.GetAttribute("time"));
                            }
                            if (commandFile.GetAttribute("servo") == "all")
                            {
                                controller.setServoPositions(new int[] { 0, 1, 2, 3, 4, 5 }, position);
                            }
                            else
                            {
                                int servoNumber = Convert.ToInt32(commandFile.GetAttribute("servo"));
                                controller.setServoPosition(servoNumber, position, time);
                            }
                        }
                    }
                    else if (commandFile.Name == "wait")
                    {
                        if (commandFile.HasAttributes)
                        {
                            int milliseconds = Convert.ToInt32(commandFile.GetAttribute("milliseconds"));
                            System.Threading.Thread.Sleep(milliseconds);
                        }
                    }
                    else if (commandFile.Name == "print")
                    {
                        Console.WriteLine(commandFile.ReadString());
                    }
                }
            }
        }
    }
}