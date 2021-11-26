using AtomicTorch.CBND.GameApi.Data;
using AtomicTorch.CBND.GameApi.Data.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMod.Scripts.Models
{
    public class ItemModel
    {
        // Type
        public string TypeName;
        public string FullTypeName;

        // IProtoItem
        public string Id;
        public string ShortId;
        public string Name;
        public ushort MaxItemsPerStack;
        public bool IsStackable;
        public string Description;

        public ItemModel(IProtoEntity protoEntity)
        {
            Type type = protoEntity.GetType();
            this.TypeName = type.Name;
            this.FullTypeName = type.FullName;

            if (protoEntity is IProtoItem)
            {
                var protoItem = protoEntity as IProtoItem;
                this.Id = protoItem.Id;
                this.ShortId = protoItem.ShortId;
                this.Name = protoItem.Name;
                this.MaxItemsPerStack = protoItem.MaxItemsPerStack;
                this.IsStackable = protoItem.IsStackable;
                this.Description = protoItem.Description;
            }
        }

        public void SerializeItemModelProperties(StringBuilder sb)
        {
            CustomSerializer.Serialize(sb, "name", this.Name);
            CustomSerializer.Serialize(sb, "shortId", this.ShortId);
            CustomSerializer.Serialize(sb, "id", this.Id);

            CustomSerializer.Serialize(sb, "typeName", this.TypeName);
            CustomSerializer.Serialize(sb, "fullTypeName", this.FullTypeName);

            CustomSerializer.Serialize(sb, "maxItemsPerStack", this.MaxItemsPerStack);
            CustomSerializer.Serialize(sb, "isStackable", this.IsStackable);
            CustomSerializer.Serialize(sb, "description", this.Description);
        }
    }
}
