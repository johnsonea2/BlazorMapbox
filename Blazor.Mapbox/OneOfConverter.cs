using OneOf;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fennorad.Mapbox
{
    internal class OneOfConverter : JsonConverter<IOneOf>
    {
        public override IOneOf Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override void Write(Utf8JsonWriter writer, IOneOf value, JsonSerializerOptions options)
        {
            if (value.Value == null)
            {
                writer.WriteNullValue();
            }
            else if (value.Value is bool)
            {
                writer.WriteBooleanValue((bool)value.Value);
            }
            else if (value.Value is int)
            {
                writer.WriteNumberValue((int)value.Value);
            }
            else if (value.Value is long)
            {
                writer.WriteNumberValue((long)value.Value);
            }
            else if (value.Value is double)
            {
                writer.WriteNumberValue((double)value.Value);
            }
            else if (value.Value is float)
            {
                writer.WriteNumberValue((float)value.Value);
            }
            else if (value.Value is decimal)
            {
                writer.WriteNumberValue((decimal)value.Value);
            }
            else if (value.Value is DateTime)
            {
                writer.WriteStringValue((DateTime)value.Value);
            }
            else if (value.Value is DateTimeOffset)
            {
                writer.WriteStringValue((DateTimeOffset)value.Value);
            }
            else
            {
                JsonSerializer.Serialize(writer, value.Value);
            }
        }
    }
}
