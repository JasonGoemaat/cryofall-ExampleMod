// ReSharper disable CanExtractXamlLocalizableStringCSharp

namespace ExampleMod.ConsoleCommands.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AtomicTorch.CBND.CoreMod.Items.Ammo;
    using AtomicTorch.CBND.CoreMod.Items.DataLogs.Base;
    using AtomicTorch.CBND.CoreMod.Items.Devices;
    using AtomicTorch.CBND.CoreMod.Items.Drones;
    using AtomicTorch.CBND.CoreMod.Items.Equipment;
    using AtomicTorch.CBND.CoreMod.Items.Food;
    using AtomicTorch.CBND.CoreMod.Systems.Console;
    using AtomicTorch.CBND.CoreMod.Systems.ServerModerator;
    using AtomicTorch.CBND.CoreMod.Systems.ServerOperator;
    using AtomicTorch.CBND.GameApi.Data;
    using AtomicTorch.CBND.GameApi.Data.Items;
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
                DoSimpleJson(sb, searchCommand);
                return sb.ToString();
            }

            if (searchCommand != null && searchCommand.StartsWith("ids"))
            {
                DoIds(sb, searchCommand);
                return sb.ToString();
            }

            sb.AppendLine();
            sb.AppendLine("Usage:");
            sb.AppendLine("    em hello - say hello");
            sb.AppendLine("    em json - log json to chat log for your use");
            sb.AppendLine("    em ids - json array of all short ids");

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
            string[] commands = { "hello", "json", "ids" };
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

            sb.AppendLine("<---------- BEGIN JSON ---------->");
            sb.AppendLine("{");

            // foods!
            CustomSerializer.SerializeArray(sb, "Foods", foods);
            sb.Length -= 3; // remove CR/LF/comma

            sb.AppendLine("}");
            sb.AppendLine("<---------- END JSON ---------->");
        }

        /// <summary>
        /// Do all json in here for all objects
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="searchCommand"></param>
        private static void DoSimpleJson(StringBuilder sb, string searchCommand)
        {
            sb.AppendLine("<---------- BEGIN ENTITIES ---------->");
            sb.AppendLine("{");

            sb.AppendLine("  \"entities\": [");

            List<IProtoEntity> allEntitiesList = Api.FindProtoEntities<IProtoEntity>().ToList();
            foreach (var entity in allEntitiesList)
            {
                sb.AppendLine("    {");

                CustomSerializer.Serialize(sb, "id", entity.Id);
                CustomSerializer.Serialize(sb, "shortId", entity.ShortId);
                CustomSerializer.Serialize(sb, "name", entity.Name);

                if (entity is IProtoItem) {
                    if (entity is IProtoItemAmmo)
                    {
                        var ammo = entity as IProtoItemAmmo;
                        CustomSerializer.Serialize(sb, "proto", "IProtoItemAmmo");
                        CustomSerializer.Serialize(sb, "isReferenceAmmo", ammo.IsReferenceAmmo);
                        CustomSerializer.Serialize(sb, "isSuppressWeaponSpecialEffect", ammo.IsSuppressWeaponSpecialEffect);
                        //CustomSerializer.Serialize(sb, "armorPiercingCoef", ammo.DamageDescription.ArmorPiercingCoef);
                        //                    CustomSerializer.Serialize(sb, "armorPiercingCoef", ammo.DamageDescription.DamageProportions[0].);
                        CustomSerializer.Serialize(sb, "damageValue", ammo.DamageDescription.DamageValue);
                        CustomSerializer.Serialize(sb, "finalDamageMultiplier", ammo.DamageDescription.FinalDamageMultiplier);
                        CustomSerializer.Serialize(sb, "rangeMax", ammo.DamageDescription.RangeMax);
                    }

                    if (entity is IProtoItemDataLog)
                    {
                        var dataLog = entity as IProtoItemDataLog;
                        CustomSerializer.Serialize(sb, "proto", "IProtoItemDataLog");
                        CustomSerializer.Serialize(sb, "text", dataLog.Text);
                    }

                    if (entity is IProtoItemPowerBank)
                    {
                        var powerBank = entity as IProtoItemPowerBank;
                        CustomSerializer.Serialize(sb, "proto", "IProtoItemPowerBank");
                        CustomSerializer.Serialize(sb, "durabilityMax", powerBank.DurabilityMax);
                        CustomSerializer.Serialize(sb, "energyCapacity", powerBank.EnergyCapacity);
                        CustomSerializer.Serialize(sb, "equipmentType", powerBank.EquipmentType);
                    }

                    //if (entity is IProtoItemEquipment)
                    //{
                    //    var item = entity as IProtoItemEquipment;
                    //    CustomSerializer.Serialize(sb, "proto", "IProtoItemEquipment");
                    //    CustomSerializer.Serialize(sb, "compatibleContainerSlotsIds", string.Join(',', item.CompatibleContainerSlotsIds));
                    //    CustomSerializer.Serialize(sb, "durabilityMax", item.DurabilityMax);
                    //    CustomSerializer.Serialize(sb, "equipmentType", item.EquipmentType);
                    //    CustomSerializer.Serialize(sb, "isRepairable", item.IsRepairable);
                    //    //CustomSerializer.Serialize(sb, "description", item.Description);
                    //    //CustomSerializer.Serialize(sb, "isStackable", item.IsStackable);
                    //    //CustomSerializer.Serialize(sb, "maxItemsPerStack", item.MaxItemsPerStack);

                    //    // CustomSerializer.Serialize(sb, "protoEffects", string.Join(',', item.ProtoEffects.Sources.List[0].); // percent, source, value, ...

                    //    if (entity is IProtoItemPowerBank)
                    //    {
                    //        var powerBank = entity as IProtoItemPowerBank;
                    //        CustomSerializer.Serialize(sb, "isPowerBank", true);
                    //        CustomSerializer.Serialize(sb, "maxItemsPerStack", powerBank.EnergyCapacity);
                    //    }
                    //}

                    if (entity is IProtoItemDrone)
                    {
                        var item = entity as IProtoItemDrone;
                        CustomSerializer.Serialize(sb, "proto", "IProtoItemDrone");
                        CustomSerializer.Serialize(sb, "durabilityToStructurePointsConversionCoefficient", item.DurabilityToStructurePointsConversionCoefficient);
                    }
                }

                sb.Length--; // get rid of last comma

                sb.AppendLine("    },");
            }

            sb.Length -= 3; // back up to last comma
            sb.AppendLine();

            sb.AppendLine("  ]");

            sb.AppendLine("}");
            sb.AppendLine("<---------- END ENTITIES ---------->");
        }
        public void DoIds(StringBuilder sb, string searchCommand)
        {
            List<IProtoEntity> allEntitiesList = Api.FindProtoEntities<IProtoEntity>().ToList();

            //var shortIds = allEntitiesList.Select(entity => entity.ShortId).ToList();
            //var shortIdsJson = shortIds.Select(shortId => string.Format("\"{0}\"", shortId)).ToList();
            //var joinedString = string.Join(",", shortIdsJson);
            //sb.AppendFormat("[{0}]", joinedString);
            //sb.AppendLine();

            sb.AppendLine("<---------- BEGIN IDS ---------->");
            var ids = allEntitiesList.Select(entity => entity.Id).ToList();
            var idsJson = ids.Select(id => string.Format("\"{0}\"", id)).ToList();
            var joinedString = string.Join(",", idsJson);
            sb.AppendFormat("[{0}]", joinedString);
            sb.AppendLine();
            sb.AppendLine("<---------- END IDS ---------->");
            sb.AppendLine();
        }
    }
}