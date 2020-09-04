using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Tracking.Models
{
    public class InputData
    {
        private const byte MinArgsLength = 3;
        private const string KeyPointer = "-k";
        private const string KeyValue = "12345";

        private static readonly IList<CommandSpecification> CommandSpecificationList = 
            new List<CommandSpecification>()
            {
                new CommandSpecification{Command = Commands.Add, ParametersNumber = 0},
                new CommandSpecification{Command = Commands.Read, ParametersNumber = 0},
                new CommandSpecification{Command = Commands.Find, ParametersNumber = 1},

            };
        public Commands CommandValue { get; }
        public string[] Parameters { get;  }
        public string Key { get; }

        public InputData(Commands action, string[] parameters, string key)
        {
            CommandValue = action;
            Parameters = parameters;
            Key = key;
        }

        public static InputData Parse(params string[] args)
        {
            var availableActions = Enum.GetNames(typeof(Commands))
                .Select(a => a.ToLower()).ToArray();

            var message = $"Command line arguments:{Environment.NewLine}" +
                          $" Command [Parameters]  -  command name. Command name list: {string.Join(", ", availableActions)};{Environment.NewLine}" +
                          "  -k KeyValue  -  password;";

            if (args.Length != MinArgsLength) throw new Exception(message);

           if (!Enum.TryParse<Commands>(args[0], true, out _))
                throw new Exception(
                    $"{args[1]} - invalid command. Command list: {string.Join(", ", availableActions)}");

            Enum.TryParse(args[0], out Commands command);

            var parametersNumber = CommandSpecificationList
                .Where(i => i.Command == command)
                .Select(i => i.ParametersNumber).FirstOrDefault();

            if (args.Length != MinArgsLength + parametersNumber || !string.Equals(args[args.Length - 2], KeyPointer))
                throw new Exception(message);

            if (!string.Equals(args[args.Length - 1], KeyValue))
                throw new Exception(
                    "Password is incorrect");

            var parameters = new string[parametersNumber];
            for (var i = 0; i < parametersNumber; i++)
            {
                parameters[i] = args[i + 1];
            }

            return new InputData(command, parameters, args[args.Length-1]);
        }

        
    }
}
