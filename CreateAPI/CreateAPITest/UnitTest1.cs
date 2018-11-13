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
            DbContextOptions<TodoDbContext> options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("GetTodoName")
                .Options;

            using (TodoDbContext context = new TodoDbContext(options))
            {
                Todo task = new Todo();
                task.Title = "todoTest";

                context.Todos.Add(task);
                context.SaveChanges();

                var taskTitle = await context.Todos.FirstOrDefaultAsync(x => x.Title == task.Title);

                Assert.Equal("todoTest", taskTitle.Title);
            }
        }
    }
}
