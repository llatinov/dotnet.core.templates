using System.Runtime.Serialization;

namespace PROJECT_NAME.Sqs.Models
{
    public enum MovieGenre
    {
        [EnumMember(Value = "Action Movie")]
        Action,
        [EnumMember(Value = "Drama Movie")]
        Drama
    }
}