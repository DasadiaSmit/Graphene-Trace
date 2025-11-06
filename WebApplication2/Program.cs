using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------
// 1️⃣ Add services to the container
// -------------------------------------------------------------

// Add MVC Controllers and Views
builder.Services.AddControllersWithViews();

// ✅ Add ApplicationDbContext (Database Connection)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Enable Session Handling (for login sessions)
builder.Services.AddSession();

// -------------------------------------------------------------
// 2️⃣ Build the application
// -------------------------------------------------------------
var app = builder.Build();

// -------------------------------------------------------------
// 3️⃣ Configure the HTTP request pipeline
// -------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Enable sessions before authorization
app.UseSession();

app.UseAuthorization();

// -------------------------------------------------------------
// 4️⃣ Default Routing
// -------------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// -------------------------------------------------------------
// 5️⃣ Apply migrations automatically on startup (optional)
// -------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// -------------------------------------------------------------
// 6️⃣ Run the Application
// -------------------------------------------------------------
app.Run();
