using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripTracker.BackService.Model;
using TripTracker.UI.Data;
using TripTracker.UI.Services;

namespace TripTracker.UI
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IApiClient _Client;

        public EditModel(IApiClient client)
        {
            _Client = client;
        }

        [BindProperty]
        public Trip Trip { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trip = await _Client.GetTripAsync(id.Value);

            if (Trip == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _Client.PutTripAsync(Trip);

            return RedirectToPage("./Index");
        }

        private async Task<bool> TripExists(int id)
        {
            return await _Client.GetTripAsync(id) != null;
        }
    }
}
