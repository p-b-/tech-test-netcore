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
            var count = dbContext.TodoItems
                .Where(ti => ti.TodoListId == todoListId)
                .Max(r => r.Rank);

            return count+1;
        }

        public static bool SwapRanksForItems(this ApplicationDbContext dbContext, int item1Id, int item2Id)
        {
            TodoItem item1 = dbContext.SingleTodoItem(item1Id);
            TodoItem item2 = dbContext.SingleTodoItem(item2Id);
            if (item1==null || item2==null)
            {
                return false;
            }

            int item1Rank = item1.Rank;
            item1.Rank = item2.Rank;
            item2.Rank = item1Rank;

            dbContext.Update(item1);
            dbContext.Update(item2);
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