using AtomicTorch.CBND.CoreMod.Items.Seeds;
using AtomicTorch.CBND.GameApi.Data;
using AtomicTorch.CBND.GameApi.Data.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMod.Scripts.Models
{
    //public class SeedsModel : ItemModel, ICustomSerializable
    //{
    //    public string plantShortId;

    //    public SeedsModel(IProtoEntity protoEntity) : base(protoEntity)
    //    {
    //        if (protoEntity is IProtoItemSeed)
    //        {
    //            IProtoItemSeed protoSeed = protoEntity as IProtoItemSeed;
    //            IProtoItem protoPlantItem = protoSeed.ObjectPlantProto as IProtoItem;
    //            plantShortId = protoPlantItem.ShortId;
    //        }
    //    }

    //    public string Serialize()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("{");
    //        this.SerializeItemModelProperties(sb);
    //        CustomSerializer.Serialize(sb, "plantShortId", this.plantShortId);
    //        sb.Append("}");
    //        return sb.ToString();
    //    }
    //}
}
