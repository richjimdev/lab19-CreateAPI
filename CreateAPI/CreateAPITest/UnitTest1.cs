using CreateAPI.Data;
using CreateAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Xunit;

namespace CreateAPITest
{
    public class UnitTest1
    {

        [Fact]
        public async void CanReadTodo()
        {
            //Arrange
            DbContextOptions<TodoDbContext> options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("GetTodoName")
                .Options;

            using (TodoDbContext context = new TodoDbContext(options))
            {
                Todo task = new Todo();
                task.Title = "todoTest";

                context.Todos.Add(task);
                context.SaveChanges();

                //Act
                var taskTitle = await context.Todos.FirstOrDefaultAsync(x => x.Title == task.Title);
                
                //Assert
                Assert.Equal("todoTest", taskTitle.Title);
            }
        }


        [Fact]
        public async void CanCreateATodo()
        {
            //Arrange
            DbContextOptions<TodoDbContext> options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("GetTodoName")
                .Options;

            using (TodoDbContext context = new TodoDbContext(options))
            {
                Todo task = new Todo();
                task.Title = "todoTest";

                context.Todos.Add(task);
                context.SaveChanges();

                //Act
                var taskTitle = await context.Todos.FirstOrDefaultAsync(x => x.Title == task.Title);

                //Assert
                Assert.Equal("todoTest", taskTitle.Title);
            }
        }

        [Fact]
        public async void CanUpdateATodo()
        {
            //Arrange
            DbContextOptions<TodoDbContext> options =
                new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("GetTodoName")
                .Options;

            using (TodoDbContext context = new TodoDbContext(options))
            {
                Todo task = new Todo();
                task.Title = "testTodo";

                context.Todos.Add(task);
                context.SaveChanges();

                //Act
                task.Title = "updateTodo";

                context.Todos.Update(task);
                context.SaveChanges();

                var taskTitle = await context.Todos.FirstOrDefaultAsync(x => x.Title == task.Title);

                //Assert
                Assert.Equal("updateTodo", taskTitle.Title);
            }
        }

        [Fact]
        public async void CanDeleteATodo()
        {
            //Arrange
            DbContextOptions<TodoDbContext> options =
                new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("GetTodoName")
                .Options;

            using (TodoDbContext context = new TodoDbContext(options))
            {
                Todo task = new Todo();
                task.Title = "testTodo";

                context.Todos.Add(task);
                context.SaveChanges();

                //Act
                context.Todos.Remove(task);
                context.SaveChanges();

                var taskTitle = await context.Todos.ToListAsync();

                //Assert
                Assert.DoesNotContain(task, taskTitle);
            }
        }

        [Fact]
        public async void CanReadATodoList()
        {
            //Arrange
            DbContextOptions<TodoDbContext> options =
                new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("GetTodoListName")
                .Options;

            using (TodoDbContext context = new TodoDbContext(options))
            {
                TodoList list = new TodoList();
                list.Title = "testList";

                context.TodoLists.Add(list);
                context.SaveChanges();

                //Act
                var listContents = await context.TodoLists.FirstOrDefaultAsync(x => x.Title == list.Title);

                //Assert
                Assert.Equal("testList", listContents.Title);
            }
        }

        [Fact]
        public async void CanUpdateATodoList()
        {
            //Arrange
            DbContextOptions<TodoDbContext> options =
                new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("GetTodoListName")
                .Options;

            using (TodoDbContext context = new TodoDbContext(options))
            {
                TodoList list = new TodoList();
                list.Title = "testList";

                context.TodoLists.Add(list);
                context.SaveChanges();

                //Act
                list.Title = "updateList";

                context.TodoLists.Update(list);
                context.SaveChanges();

                var listContents = await context.TodoLists.FirstOrDefaultAsync(x => x.Title == list.Title);

                //Assert
                Assert.Equal("updateList", listContents.Title);
            }
        }
    }
}
