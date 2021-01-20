using System.Collections.Generic;
using System.Collections.Immutable;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace BuildingConfiguration.Infrastructure
{
    public class ImmutableListSerializer<TItem> : EnumerableSerializerBase<ImmutableList<TItem>, TItem>,
        IBsonArraySerializer,
        IChildSerializerConfigurable
    {
        public IBsonSerializer ChildSerializer => ItemSerializer;

        public IBsonSerializer WithChildSerializer(IBsonSerializer childSerializer)
        {
            return WithChildSerializer(childSerializer);
        }

        protected override void AddItem(object accumulator, TItem item)
        {
            var builder = accumulator as ImmutableList<TItem>.Builder;
            builder?.Add(item);
        }

        protected override object CreateAccumulator()
        {
            return ImmutableList.CreateBuilder<TItem>();
        }

        protected override IEnumerable<TItem> EnumerateItemsInSerializationOrder(ImmutableList<TItem> value)
        {
            return value;
        }

        protected override ImmutableList<TItem> FinalizeResult(object accumulator)
        {
            var builder = accumulator as ImmutableList<TItem>.Builder;
            return builder?.ToImmutableList() ?? ImmutableList<TItem>.Empty;
        }
    }
}