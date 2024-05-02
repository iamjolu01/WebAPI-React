using Microsoft.EntityFrameworkCore;

public interface IHouseRepository{
    Task<List<HouseDto>> GetAll();
}
public class HouseRepository: IHouseRepository{
    private readonly HouseDbContext context;

    public HouseRepository(HouseDbContext context)
    {
        this.context = context;
    }

    public async Task<List<HouseDto>> GetAll(){
        return await context.Houses.Select(e => new HouseDto(e.Id, e.Address, e.Country, e.Price)).ToListAsync();
    }
}