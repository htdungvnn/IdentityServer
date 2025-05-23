// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using IdentityServer.WebApp.Areas.Identity.Data.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.WebApp.Areas.Identity.Pages.Account.Manage;

public class PersonalDataModel : PageModel
{
    private readonly ILogger<PersonalDataModel> _logger;
    private readonly UserManager<WebAppUser> _userManager;

    public PersonalDataModel(
        UserManager<WebAppUser> userManager,
        ILogger<PersonalDataModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        return Page();
    }
}