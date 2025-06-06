using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ColorJsonConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var colorString = reader.GetString();

        if (string.IsNullOrWhiteSpace(colorString))
            return Color.Empty;

        try
        {
            // Пробуем по имени цвета
            return Color.FromName(colorString);
        }
        catch
        {
            throw new JsonException($"Invalid color name: {colorString}");
        }
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.IsNamedColor ? value.Name : $"#{value.R:X2}{value.G:X2}{value.B:X2}");
    }
}
