// ReSharper disable CanExtractXamlLocalizableStringCSharp

namespace ExampleMod.ConsoleCommands.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AtomicTorch.CBND.CoreMod.Items.Food;
    using AtomicTorch.CBND.CoreMod.Systems.Console;
    using AtomicTorch.CBND.CoreMod.Systems.ServerModerator;
    using AtomicTorch.CBND.CoreMod.Systems.ServerOperator;
    using AtomicTorch.CBND.GameApi.Data;
    using AtomicTorch.CBND.GameApi.Scripting;
    using ExampleMod.Scripts.Models;

    public class ConsoleConsoleExampleMod : BaseConsoleCommand
    {
        public override string Alias => "em";

        public override string Description => "ExampleMod";

        public override ConsoleCommandKinds Kind => ConsoleCommandKinds.ClientAndServerOperatorOnly;

        public override string Name => "em.default";

        public string Execute(
            [CustomSuggestions(nameof(GetCommandNameSuggestions))]
            string searchCommand = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();

            if (searchCommand != null && searchCommand.StartsWith("hello"))
            {
                DoHello(sb, searchCommand);
                return sb.ToString();
            }

            if (searchCommand != null && searchCommand.StartsWith("json"))
            {
                DoJson(sb, searchCommand);
                return sb.ToString();
            }

            sb.AppendLine();
            sb.AppendLine("Usage:");
            sb.AppendLine("    em hello - say hello");
            sb.AppendLine("    em json - log json to chat log for your use");

            return sb.ToString();
        }

        /// <summary>
        /// This is from CNEI - EntityViewModelsManager.cs
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string GetNameWithoutGenericArity(string s)
        {
            int index = s.IndexOf('`');
            return index == -1 ? s : s.Substring(0, index);
        }


        private static IEnumerable<string> GetCommandNameSuggestions(string startsWith)
        {
            string[] commands = { "hello", "json" };
            bool found = false;
            foreach (string command in commands)
            {
                if (command.StartsWith(startsWith))
                {
                    yield return command;
                }
            }

            yield return startsWith;
        }

        private static void DoHello(StringBuilder sb, string searchCommand)
        {
            sb.AppendLine("Hello, world!");
            sb.AppendFormat("  searchCommand: '{0}'", searchCommand);
            sb.AppendLine();
        }

        private static void DoJson(StringBuilder sb, string searchCommand)
        {
            // from looking at CNEI EntityViewModelsManager
            List<IProtoEntity> allEntitiesList = Api.FindProtoEntities<IProtoEntity>().ToList();
            // var entities = allEntitiesList.Where(x => x.Name.ToLowerInvariant().Contains("pepper")).ToList();
            var entities = allEntitiesList;
            bool first = true;

            List<ICustomSerializable> foods = new List<ICustomSerializable>();

            foreach (var entity in entities)
            {
                if (entity is ProtoItemFood)
                {
                    foods.Add(new FoodModel(entity));

                    //FoodModel fm = new FoodModel(entity);
                    //var json = JsonSerializer.Serialize(fm, new JsonSerializerOptions() { WriteIndented = true });
                    //sb.AppendLine(json);
                }
            }

            sb.AppendLine("<---------- BEGIN SERIALIZATION ---------->");
            sb.AppendLine("{");

            // foods!
            CustomSerializer.SerializeArray(sb, "Foods", foods);

            sb.AppendLine("}");
            sb.AppendLine("<---------- END SERIALIZATION ---------->");
        }
    }
}