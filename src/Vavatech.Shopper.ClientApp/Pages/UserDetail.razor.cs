using Microsoft.AspNetCore.Components;
using System;
using Vavatech.Shopper.ClientApp.Services;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.ClientApp.Pages;

public partial class UserDetail 
{
    [Parameter]
    public int Id { get; set; }

    private User user;

    [Inject]
    JsonPlaceholderService Api { get; set; }

    [Inject]
    NavigationManager NavigationManager { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        user = await Api.GetUser(Id);
    }

    private async Task NavigateToUsersAsync()
    {
        user.Name = user.Name.ToLower();

        await Task.Delay(TimeSpan.FromSeconds(3));

        NavigationManager.NavigateTo("users");
    }
}
