using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Database
    {
        public string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "MathGame.db3");
        public SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;

        private SQLiteAsyncConnection database;

        private async Task Init()
        {
            if (database == null) //only runs once per program execution
            {
                database = new SQLiteAsyncConnection(DatabasePath, Flags); //File is created only per run
                await database.CreateTableAsync<User>();
            }
        }

        public async Task<User> GetUserAsync()
        {
            await Init();
            List<User> result = await database.Table<User>().ToListAsync();
            if (result.Count == 0)
            {
                User a = new User();
                a.Name = "Student";
                a.Background = 0;
                a.Quarters = 0;
                a.Dimes = 0;
                a.Nickels = 0;
                a.Pennies = 0;
                a.Picture = 0;
                a.Backgrounds = "1 2 3 4 5 6 6 1 4 1 5 1";
                a.Images = "1 2 3 4 5 6 6 7 8 1 4 1 5 1 10 2 3 4 14 15 10";
                a.ChangeNeeded = 1;
                await database.InsertAsync(a);
                return a;
            } else
            {
                 //await DeleteUserAsync(result[0]);
                return result[0];
            }
            
        }

        

        public async Task UpdateExistingUserAsync(User c)
        {
            if (c.UserID == 0)
                return;
            await Init();
            await database.UpdateAsync(c);
        }

       
        public async Task DeleteUserAsync(User c)
        {
            await Init();
            await database.DeleteAsync(c);
        }

    }
}
