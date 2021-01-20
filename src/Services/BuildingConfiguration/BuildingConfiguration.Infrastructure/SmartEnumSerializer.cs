using System;
using Ardalis.SmartEnum;
using MongoDB.Bson.Serialization;

namespace BuildingConfiguration.Infrastructure
{
    public class SmartEnumSerializer<T> : IBsonSerializer where T : SmartEnum<T>
    {
        public Type ValueType => typeof(T);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return SmartEnum<T>.FromValue(context.Reader.ReadInt32());
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            context.Writer.WriteInt32((value as T)?.Value ?? default);
        }
    }
}