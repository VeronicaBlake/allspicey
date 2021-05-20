using System;
using System.Collections.Generic;
using allspicey.Models;
using allspicey.Repositories;

namespace allspicey.Services
{
    public class RecipesServices
    {

        private readonly RecipesRepository _repo;
        public RecipesServices(RecipesRepository repo)
        {
            _repo = repo;
        }
        internal IEnumerable<Recipe> GetAll()
        {
            return _repo.GetAll();
        }
        internal IEnumerable<Recipe> GetByCreatorId(string id)
        {
            throw new NotImplementedException();
        }

        internal Recipe GetById(int id)
        {
            Recipe recipe = _repo.GetById(id);
            if (recipe == null)
            {
                throw new Exception("Invalid Recipe Id");
            }
            return recipe;
        }

        internal Recipe Create(Recipe newRecipe)
        {
            return _repo.Create(newRecipe);
        }
    }
}