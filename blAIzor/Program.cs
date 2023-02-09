using Azure;
using Azure.AI.OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<OpenAIClient>((services) => {
    string key = builder.Configuration.GetValue<string>("OpenAIKey");
    string endpoint = builder.Configuration.GetValue<string>("OpenAIUrl");
    OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
    return client;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
