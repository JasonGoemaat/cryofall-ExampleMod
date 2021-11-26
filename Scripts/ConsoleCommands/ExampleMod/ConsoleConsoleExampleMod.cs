// ReSharper disable CanExtractXamlLocalizableStringCSharp

namespace ExampleMod.ConsoleCommands.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AtomicTorch.CBND.CoreMod.Systems.Console;
    using AtomicTorch.CBND.CoreMod.Systems.ServerModerator;
    using AtomicTorch.CBND.CoreMod.Systems.ServerOperator;
    using AtomicTorch.CBND.GameApi.Scripting;

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
            sb.AppendLine("Hello, world!");
            sb.AppendFormat("  searchCommand: '{0}'", searchCommand);
            return sb.ToString();
        }


        private static IEnumerable<string> GetCommandNameSuggestions(string startsWith)
        {
            yield return "red";
            yield return "green";
            yield return "blue";
        }
    }
}