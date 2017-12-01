using System;
using System.Linq;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Denby.Common;
using Denby.Common.Commands;
using Denby.Contracts;
using Denby.Contracts.EventArguments;
using Denby.MarsRover.Core;

namespace Denby.MarsRover
{
    internal class Program
    {
        private const int MaximumNumberOfCommands = 5; // this value should come from a config file
        
        private static void Main(string[] args)
        {
            var container = WindsorContainerFactory.Create(MaximumNumberOfCommands);

            var roverController = container.Resolve<MarsRoverController>();
            roverController.RaiseLocationReport += controller_RaiseLocationReport;

            DisplayControlInstructions();
            ReceiveCommands(roverController);
        }

        static void controller_RaiseLocationReport(object sender, LocationHeadingEventArg e)
        {
            Console.WriteLine(string.Format("{0}{1} {2}", e.Location.Y, e.Location.X, e.Heading).ToLower());
        }

        private static void DisplayControlInstructions()
        {
            Console.WriteLine("To move the rover enter a number followed by the letter 'm'");
            Console.WriteLine("To turn the rover to the right enter - Right");
            Console.WriteLine("To turn the rover to the left enter - Left");
        }

        private static void ReceiveCommands(IRoverController controller)
        {
            while (true)
            {
                var instruction = GetInput();

                ICommand command = StringCommandParser(controller.Rover, instruction);
                if (command != null)
                {
                    controller.AddCommand(command);
                }
            }
        }

        private static string GetInput()
        {
            return Console.ReadLine().Trim();
        }

        private static ICommand StringCommandParser(IRover rover, string input)
        {
            switch (input)
            {
                case "Left":
                    return new RotateCommand(rover) {Rotation = Rotate.Left};
                case "Right":
                    return new RotateCommand(rover) {Rotation = Rotate.Right};
                default:
                {
                    if (input.EndsWith("m") && input.Count(o=>char.IsLetter(o))==1)
                    {
                        int distance = int.Parse(input.Substring(0, input.Length - 1));
                        return new MoveCommand(rover) {Distance = distance};
                    }
                    return null;
                }
            }
        }
    }
}
