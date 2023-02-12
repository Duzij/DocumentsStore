using System.Runtime.Serialization;

public enum DocumentFileFormat
{
    None,
    [EnumMember(Value = "application/xml")]
    XML,
    [EnumMember(Value = "application/json")]
    JSON,
    [EnumMember(Value = "application/msgpack")]
    MessagePack
}