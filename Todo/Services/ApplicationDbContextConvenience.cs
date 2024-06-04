using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Todo.Data;
using Todo.Data.Entities;

namespace Todo.Services
{
    public static class ApplicationDbContextConvenience
    {
        public static List<TodoList> RelevantTodoLists(this ApplicationDbContext dbContext, string userId)
        {
            var ownedLists = dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .Where(tl => tl.Owner.Id == userId);
            var otherLists = dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .Where(tl => tl.Items.Any(td => td.ResponsiblePartyId == userId));

            // Cannot perform a union operation on IQueryable<> that contain joins to other tables. Cannot remove the .include(tl => tl.Items) 
            //  and perform the union, because the .Where brings the join in.
            // Converting to lists and performing the union works correctly.
            return ownedLists.ToList().Union(otherLists.ToList()).ToList();
        }

        public static int NextRankForList(this ApplicationDbContext dbContext, int todoListId)
        {
            var items = dbContext.TodoItems
                .Where(ti => ti.TodoListId == todoListId);
            if (items.Count()==0)
            {
                return 0;
            }

            var maxRank = items.Max(r => r.Rank);

            return maxRank + 1;
        }

        public static bool UpdateItemRank(this ApplicationDbContext dbContext, int itemId, int updatedRank)
        {
            TodoItem item1 = dbContext.SingleTodoItem(itemId);
            if (item1 == null)
            {
                return false;
            }

            item1.Rank = updatedRank;

            dbContext.Update(item1);
            dbContext.SaveChanges();
            return true;
        }

        public static TodoList SingleTodoList(this ApplicationDbContext dbContext, int todoListId)
        {
            return dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .ThenInclude(ti => ti.ResponsibleParty)
                .Single(tl => tl.TodoListId == todoListId);
        }

        public static TodoItem SingleTodoItem(this ApplicationDbContext dbContext, int todoItemId)
        {
            return dbContext.TodoItems.Include(ti => ti.TodoList).Single(ti => ti.TodoItemId == todoItemId);
        }
    }
}