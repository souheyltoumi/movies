using Movies.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data
{
    public class Database
    {
         SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Movie>().Wait();
        }

        public Task<List<Movie>> GetMovieAsync()
        {
            return _database.Table<Movie>().ToListAsync();
        }

        public Task<int> SaveMovieAsync(Movie movie)
        {
            return _database.InsertAsync(movie);
        }
        public void DeleteMovieAsync(Movie movie)
        {
             _database.DeleteAsync( movie);
        }
    }
}
