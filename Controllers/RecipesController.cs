using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using allspicey.Models;
using allspicey.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace allspicey.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesServices _service;
        private readonly AccountsService _acctService;

        public RecipesController(RecipesServices service, AccountsService acctsService)
        {
            _service = service;
            _acctService = acctsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetAll()
        {
            try
            {
                IEnumerable<Recipe> recipes = _service.GetAll();
                return Ok(recipes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetById(int id)
        {
            try
            {
                Recipe found = _service.GetById(id);
                return Ok(found);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _acctService.GetOrCreateAccount(userInfo);
                newRecipe.CreatorId = userInfo?.Id;

                Recipe recipe = _service.Create(newRecipe);
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}