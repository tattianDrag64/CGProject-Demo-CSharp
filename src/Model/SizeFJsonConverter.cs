using System.Drawing;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

public class SizeFJsonConverter : JsonConverter<SizeF>
{
    public override SizeF Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string sizeString = reader.GetString();
            var parts = sizeString.Split(',');
            if (parts.Length == 2 &&
                float.TryParse(parts[0], out float width) &&
                float.TryParse(parts[1], out float height))
            {
                return new SizeF(width, height);
            }

            throw new JsonException($"Invalid SizeF format: {sizeString}");
        }

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject or string for SizeF");

        float w = 0, h = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return new SizeF(w, h);

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string prop = reader.GetString();
                reader.Read();

                switch (prop)
                {
                    case "Width": w = reader.GetSingle(); break;
                    case "Height": h = reader.GetSingle(); break;
                    default: reader.Skip(); break;
                }
            }
        }

        throw new JsonException("Incomplete SizeF object");
    }

    public override void Write(Utf8JsonWriter writer, SizeF value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("Width", value.Width);
        writer.WriteNumber("Height", value.Height);
        writer.WriteEndObject();
    }
}
