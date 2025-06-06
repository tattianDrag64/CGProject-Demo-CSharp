using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Draw;
using Draw.src.Model;

public class ShapeJsonConverter : JsonConverter<Shape>
{
    public override Shape Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument document = JsonDocument.ParseValue(ref reader))
        {
            var jsonObject = document.RootElement;
            var type = jsonObject.GetProperty("Type").GetString();

            switch (type)
            {
                case "RectangleShape":
                    return JsonSerializer.Deserialize<RectangleShape>(jsonObject.GetRawText(), options);
                case "EllipseShape":
                    return JsonSerializer.Deserialize<EllipseShape>(jsonObject.GetRawText(), options);
                case "GroupShape":
                    return JsonSerializer.Deserialize<GroupShape>(jsonObject.GetRawText(), options);
                case "PointShape":
                    return JsonSerializer.Deserialize<PointShape>(jsonObject.GetRawText(), options);
                case "LineShape":
                    return JsonSerializer.Deserialize<LineShape>(jsonObject.GetRawText(), options);
                case "SnowflakeShape":
                    return JsonSerializer.Deserialize<SnowflakeShape>(jsonObject.GetRawText(), options);
                case "StarShape":
                    return JsonSerializer.Deserialize<StarShape>(jsonObject.GetRawText(), options);
                case "TriangleShape":
                    return JsonSerializer.Deserialize<TriangleShape>(jsonObject.GetRawText(), options);
                default:
                    throw new NotSupportedException($"Unsupported shape type: {type}");
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, Shape value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}
