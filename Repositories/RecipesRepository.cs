using System;
using System.Collections.Generic;
using System.Data;
using allspicey.Models;
using Dapper;

namespace allspicey.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;
        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }
        internal IEnumerable<Recipe> GetAll()
        {
            string sql = "SELECT * FROM recipe";
            return _db.Query<Recipe>(sql);
        }

        internal Recipe GetById(int id)
        {
            string sql = "SELECT * FROM recipe WHERE id = @id";
            return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
        }

        internal Recipe Create(Recipe newRecipe)
        {
            string sql = @"
            INSERT INTO recipe
            (creatorId, name, description)
            VALUES
            (@CreatorId, @Name, @Description);
            SELECT LAST_INSERT_ID()";
            newRecipe.Id = _db.ExecuteScalar<int>(sql, newRecipe);
            return newRecipe;

        }
    }
}