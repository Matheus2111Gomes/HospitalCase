using HospitalCase.Repositories.Patient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HospitalCase.Repositories; 

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(builder.Configuration.GetConnectionString("DefaultConnection"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IPatientRepository>(provider => new PatientRepository(connectionString));
builder.Services.AddTransient<PatientService>();

builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>(provider =>
{
    var connectionString = provider.GetRequiredService<string>();
    return new AppointmentRepository(connectionString);
});




var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();