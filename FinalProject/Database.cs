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
                a.Background = "#FFFFFF";
                a.Quarters = 5;
                a.Dimes = 5;
                a.Nickels = 5;
                a.Pennies = 5;
                a.Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRw8NrJIS7Q1lqexVCY0TAuA7Nhm5dFNllCw&s";
                a.backgrounds = new List<string>();
                a.images = new List<string>();
                a.ChangeNeeded = 69;
                await database.InsertAsync(a);
                return a;
            } else
            {
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
