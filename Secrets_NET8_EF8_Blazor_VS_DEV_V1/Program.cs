using Secrets_NET8_EF8_Blazor_VS_DEV_V1;
using Secrets_NET8_EF8_Blazor_VS_DEV_V1.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// retrieve SyncFusion Key from Key Vault
Secrets secrets = new Secrets();
string SyncFusionKey = secrets.GetSecret(builder.Configuration,
    builder.Configuration["SecretName:SyncFusionKey"] ?? "".ToString());
Console.WriteLine(SyncFusionKey);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
