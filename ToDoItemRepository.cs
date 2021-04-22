using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;



public class ToDoItemRepository : BaseRepository, IRepository<ToDoItem>
{

    public ToDoItemRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<ToDoItem>> GetAll()
    {
        using var connection = CreateConnection();
        IEnumerable<ToDoItem> toDoItems = await connection.QueryAsync<ToDoItem>("SELECT * FROM ToDoList;");
        return toDoItems;
    }


    public async Task Delete(long id)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("DELETE FROM ToDoList WHERE Id = @Id;", new { Id = id });
    }

    public async Task<ToDoItem> Get(long id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<ToDoItem>("SELECT * FROM ToDoList WHERE Id = @Id;", new { Id = id });
    }


    public async Task<ToDoItem> Insert(ToDoItem toDoItem)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<ToDoItem>("INSERT INTO ToDoList (Title, Priority, IsComplete) VALUES (@Title, @Priority, @IsComplete) RETURNING *;", toDoItem);
    }
}