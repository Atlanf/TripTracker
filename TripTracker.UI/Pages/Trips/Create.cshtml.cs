using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TripTracker.BackService.Model;
using TripTracker.UI.Data;
using TripTracker.UI.Services;

namespace TripTracker.UI
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IApiClient _Client;

        public CreateModel(IApiClient client)
        {
            _Client = client;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Trip Trip { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _Client.AddTripAsync(Trip);

            return RedirectToPage("./Index");
        }
    }
}