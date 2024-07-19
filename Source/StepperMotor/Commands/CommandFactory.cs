using SerialCommunication.Abstraction;
using System;

namespace StepperMotor.SerialTalkers
{
    public class CommandFactory : ICommandFactory
    {
        public ISerialCommand Create(string command)
        {
            command = command.Trim('[', ']');
            string[] parts = command.Split('|');

            if (parts[0].Equals("ccw", StringComparison.OrdinalIgnoreCase))
                return new RotateCounterClockwise(1);

            if (parts[0].Equals("cw", StringComparison.OrdinalIgnoreCase))
                return new RotateClockwise(1);

            if (parts[0].Equals("location", StringComparison.OrdinalIgnoreCase))
                return LocationCommand.Parse(command);

            if (parts[0].Equals("manual", StringComparison.OrdinalIgnoreCase))
                return ManualCommand.Parse(command);

            if (parts[0].Equals("reset", StringComparison.OrdinalIgnoreCase))
                return new ResetCommand();

            if (parts[0].Equals("log", StringComparison.OrdinalIgnoreCase))
                return LogCommand.Parse(parts);

            if (parts[0].Equals("loglevel", StringComparison.OrdinalIgnoreCase))
                return LogLevelCommand.Parse(parts);

            return new UnKnownCommand(command);
        }
    }
}
