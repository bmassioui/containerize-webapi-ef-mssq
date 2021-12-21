using AutoMapper;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Profiles
{
    /// <summary>
    /// Setup TodoItem Profile
    /// </summary>
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            // Source => Target
            CreateMap<TodoItem, TodoItemReadDto>();

            // Target => Source
            CreateMap<TodoItemUpdateDto, TodoItem>();
            CreateMap<TodoItemCreateDto, TodoItem>();

        }
    }
}