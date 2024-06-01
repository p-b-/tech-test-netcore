using System.Collections.Generic;
using Todo.Models.TodoItems;
using Microsoft.AspNetCore.Identity;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public string OwnerEmail { get; set; }
        public string OwnerId { get; set; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }

        public TodoListDetailViewmodel(int todoListId, string title, IdentityUser owner, ICollection<TodoItemSummaryViewmodel> items)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            OwnerEmail = owner.Email;
            OwnerId = owner.Id;
        }
    }
}