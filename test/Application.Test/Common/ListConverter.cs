using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Test.Common
{
    internal class ListConverter : JsonConverter<IList>
    {
        public override IList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return UnwrapList(JsonSerializer.Deserialize<IList>(ref reader));
        }

        public override void Write(Utf8JsonWriter writer, IList value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private IList UnwrapList(IList deserializedList)
        {
            List<object> list = new List<object>();
            foreach (object deserialized in deserializedList)
            {
                list.Add(UnwrapJsonElement(deserialized));
            }

            return list;
        }

        private object UnwrapJsonElement(object deserializeObject)
        {
            if (deserializeObject == null)
            {
                return null;
            }

            if (!(deserializeObject is JsonElement jsonElement))
            {
                throw new InvalidOperationException();
            }

            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Array:
                    return (from item in jsonElement.EnumerateArray()
                            select UnwrapJsonElement(item)).ToList();
                case JsonValueKind.String:
                    return jsonElement.GetString();
                case JsonValueKind.Null:
                    return null;
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return jsonElement.GetBoolean();
                case JsonValueKind.Number:
                    {
                        if (jsonElement.TryGetInt32(out var value))
                        {
                            return value;
                        }

                        if (jsonElement.TryGetDecimal(out var value2))
                        {
                            return value2;
                        }

                        throw new NotImplementedException();
                    }
                case JsonValueKind.Object:
                    return jsonElement;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}