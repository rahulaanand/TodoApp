using System;

public class TaskItem
{
    public Guid TitleId { get; set; }
    public string TitleName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DueTime { get; set; }
    public string Status { get; set; }

}
