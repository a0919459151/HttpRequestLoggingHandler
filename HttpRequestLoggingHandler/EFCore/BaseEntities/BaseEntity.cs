namespace HttpRequestLoggingHandler.EFCore.BaseEntities;

public class BaseEntity
{
    [Comment("ID")]
    public int Id { get; set; }

    [Comment("CreateAt")]
    public DateTime CreateAt { get; set; } = DateTime.Now;

    [Comment("UpdateAt")]
    public DateTime UpdateAt { get; set; } = DateTime.Now;

    [Comment("DeleteAt")]
    public DateTime? DeleteAt { get; set; }

    [Comment("UpdaterId")]
    public int? UpdaterId { get; set; }
}
