using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NodaTime;

namespace BuildingConfiguration.Infrastructure
{
    public class InstantSerializer : StructSerializerBase<Instant>
    {
        public override Instant Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Instant.FromUnixTimeTicks(context.Reader.ReadInt64());
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Instant value)
        {
            context.Writer.WriteInt64(((Instant)value).ToUnixTimeTicks());
        }
    }
}