using Microsoft.AspNetCore.Components;

namespace Vavatech.Shopper.ClientApp.Pages;

public partial class Counter : ComponentBase
{
    private void ResetCount()
    {
        currentCount= 0;
    }
}
