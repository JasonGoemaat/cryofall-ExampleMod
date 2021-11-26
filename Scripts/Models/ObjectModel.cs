using AtomicTorch.CBND.GameApi.Data.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMod.Scripts.Models
{
    public class ObjectModel
    {
        // Type
        public string TypeName;
        public string FullTypeName;

        // IProtoItem
        public string Id;
        public string ShortId;
        public string Name;

        public ObjectModel(IProtoStaticWorldObject protoObject)
        {
            Type type = protoObject.GetType();
            this.TypeName = type.Name;
            this.FullTypeName = type.FullName;

            this.Id = protoObject.Id;
            this.ShortId = protoObject.ShortId;
            this.Name = protoObject.Name;
        }

        public void SerializeObjectModelProperties(StringBuilder sb)
        {
            CustomSerializer.Serialize(sb, "name", this.Name);
            CustomSerializer.Serialize(sb, "shortId", this.ShortId);
            CustomSerializer.Serialize(sb, "id", this.Id);

            CustomSerializer.Serialize(sb, "typeName", this.TypeName);
            CustomSerializer.Serialize(sb, "fullTypeName", this.FullTypeName);
        }
    }
}
