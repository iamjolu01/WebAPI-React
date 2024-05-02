using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
public class HouseDbContext: HouseDbContext{
    public DbSet<HouseEntity> Houses => Set<HouseEntity>();

    protected override void Onconfiguring(DbContextOptionsBuilder options){
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        options.UseSqlite($"Data Source={Path.Join(path, "houses.db")}");
    }
}