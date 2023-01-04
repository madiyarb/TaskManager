namespace CommonRepository.Models;

public class BaseRepositoryEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public DateTime? CompletionDate { get; set; }
}