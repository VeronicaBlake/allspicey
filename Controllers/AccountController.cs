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
    [Route("[controller]")]
    // TODO[epic=Auth] Adds authguard to all routes on the whole controller
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly AccountsService _service;
        private readonly RecipesServices _recipeService;

        public AccountController(AccountsService service, RecipesServices recipeService)
        {
            _service = service;
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                // TODO[epic=Auth] Replaces req.userinfo
                // IF YOU EVER NEED THE ACTIVE USERS INFO THIS IS HOW YOU DO IT (FROM AUTH0)
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Account currentUser = _service.GetOrCreateAccount(userInfo);
                return Ok(currentUser);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("recipes")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetMyRecipes()
        {
            // TODO[epic=Auth] Replaces req.userinfo
            // IF YOU EVER NEED THE ACTIVE USERS INFO THIS IS HOW YOU DO IT (FROM AUTH0)
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            IEnumerable<Recipe> recipes = _recipeService.GetByCreatorId(userInfo.Id);
            return Ok(recipes);

        }




    }
}