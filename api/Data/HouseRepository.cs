using Microsoft.EntityFrameworkCore;

public interface IHouseRepository{
    Task<List<HouseDto>> GetAll();
    Task<HouseDetailDto?> Get(int id);
}
public class HouseRepository: IHouseRepository{
    private readonly HouseDbContext context;

    public HouseRepository(HouseDbContext context)
    {
        this.context = context;
    }

    public async Task<List<HouseDto>> GetAll(){
        return await context.Houses.Select(e => new HouseDto(e.Id, e.Address, 
        e.Country, e.Price)).ToListAsync();
    }

    public async Task<HouseDetailDto?> Get(int id){
        var e = await context.Houses.SingleOrDefaultAsync(
            h => h.Id == id);
        if(e == null)
            return null;
        return new HouseDetailDto(e.Id, e.Address, e.Country, e.Price, 
        e.Description, e.Photo);
    }
}