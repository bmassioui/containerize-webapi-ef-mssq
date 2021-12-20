namespace TodoApi.Dtos
{
    /// <summary>
    /// Dto for updating TodoItem
    /// </summary>
    public class TodoItemUpdateDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; } = default;
    }
}