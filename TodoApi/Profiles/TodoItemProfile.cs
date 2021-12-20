using AutoMapper;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Profiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            // Source => Target
            CreateMap<TodoItem, TodoItemReadDto>();
            // CreateMap<TodoItem, TodoItemUpdateDto>();

            // Target => Source
            CreateMap<TodoItemUpdateDto, TodoItem>();
            CreateMap<TodoItemCreateDto, TodoItem>();

        }
    }
}