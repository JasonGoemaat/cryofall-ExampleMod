using AtomicTorch.CBND.CoreMod.CharacterStatusEffects;
using AtomicTorch.CBND.CoreMod.Items;
using AtomicTorch.CBND.CoreMod.Items.Food;
using AtomicTorch.CBND.GameApi.Data;
using AtomicTorch.CBND.GameApi.Data.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMod.Scripts.Models
{
    public class FoodModel : ItemModel, ICustomSerializable
    {
        public FoodModel(IProtoEntity protoEntity) : base(protoEntity)
        {
            if (protoEntity is IProtoItemFood)
            {
                IProtoItemFood protoFood = protoEntity as IProtoItemFood;
                this.Effects = protoFood.Effects;
                this.FoodRestore = protoFood.FoodRestore;
                this.ItemUseCaption = protoFood.ItemUseCaption;
                this.OrganicValue = protoFood.OrganicValue;
                this.StaminaRestore = protoFood.StaminaRestore;
                this.WaterRestore = protoFood.WaterRestore;
            }
        }

        // ProtoItemFood
        public IReadOnlyList<EffectAction> Effects { get; private set; }
        public float FoodRestore;
        public float WaterRestore;
        public float StaminaRestore;
        public ushort OrganicValue;
        public string ItemUseCaption;
        public string IconPath; // use GenerateIconPath

        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            this.SerializeItemModelProperties(sb);
            CustomSerializer.Serialize(sb, "foodRestore", this.FoodRestore);
            CustomSerializer.Serialize(sb, "waterRestore", this.WaterRestore);
            CustomSerializer.Serialize(sb, "staminaRestore", this.StaminaRestore);
            CustomSerializer.Serialize(sb, "organicValue", this.OrganicValue);
            CustomSerializer.Serialize(sb, "itemUseCaption", this.ItemUseCaption);
            CustomSerializer.Serialize(sb, "iconPath", this.IconPath);
            sb.Append("}");
            return sb.ToString();
        }
    }
}
