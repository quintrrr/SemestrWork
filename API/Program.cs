using BLL.Services;
using Core.DTO;
using Core.Interfaces;
using DAL;
using DAL.DAO;
using DAL.Repository;
using Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetSection("Storage").GetValue<string>("MacConnectionString")
             ?? throw new Exception("Storage connection string is missing");
builder.Services.AddSingleton(new AppDbContext(connectionString));
builder.Services.AddSingleton<ITGroupRepository, TGroupRepository>();
builder.Services.AddSingleton<ITPropertyRepository, TPropertyRepository>();
builder.Services.AddSingleton<ITRelationRepository, TRelationRepository>();

builder.Services.AddSingleton<IMapper<TGroupDTO, TGroup>, GroupMapper>();
builder.Services.AddSingleton<IMapper<TPropertyDTO, TProperty>, PropertyMapper>();
builder.Services.AddSingleton<IMapper<TRelationDTO, TRelation>, RelationMapper>();

builder.Services.AddSingleton<IGroupService, GroupService>();
builder.Services.AddSingleton<IPropertyService, PropertyService>();
builder.Services.AddSingleton<IRelationService, RelationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();