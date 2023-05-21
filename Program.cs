using ContactsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
//This is the starting point to our class

// Add services to the container.

//There are some services injected to it

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injectuing the ContactsAPIDBContext service
//we add the Adddbcontect and add the type ContactsAPIDBContext 
//we give some options and tell it to use in memory db ang give the dbname
//builder.Services.AddDbContext<ContactsAPIDBContext>(options => options.UseInMemoryDatabase("ContactsDb"));
builder.Services.AddDbContext<ContactsAPIDBContext>(options =>
    options.UseSqlServer(builder.Configuration.
    GetConnectionString("ContactsApiConnectionString")));
//Entity framework has the ability to Postgress or sqlite db
//Then in packet manager console type this => Add-Migration "Initial Migration"
//then type => Update-Database

//now we have given EF core everting it need to create the db and it also knows 
//the tables becoz we have given the dbset

// now we create a controller and inject this dbcontext so we can read and write in the
//inmemory db

var app = builder.Build();//Build the application

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(); //we run the application
