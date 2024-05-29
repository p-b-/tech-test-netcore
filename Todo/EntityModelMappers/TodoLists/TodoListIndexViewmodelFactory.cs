using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Todo.Data.Entities;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListIndexViewmodelFactory
    {
        public static TodoListIndexViewmodel Create(List<TodoList> todoLists)
        {
            var lists = todoLists.Select(tl => TodoListSummaryViewmodelFactory.Create(tl)).ToList();
            return new TodoListIndexViewmodel(lists);
        }
    }
}