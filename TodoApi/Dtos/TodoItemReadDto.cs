namespace TodoApi.Dtos
{
    /// <summary>
    /// Dto for reading TodoItem
    /// </summary>
    public class TodoItemReadDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; } = default;
    }
}