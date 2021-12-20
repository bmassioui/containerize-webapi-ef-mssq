namespace TodoApi.Dtos
{
    /// <summary>
    /// Dto for creating new TodoItem
    /// </summary>
    public class TodoItemCreateDto
    {
        public string? Name { get; set; }
        public bool IsComplete { get; set; } = default;
    }
}