using Microsoft.EntityFrameworkCore;

public interface IHouseRepository{
    Task<List<HouseDto>> GetAll();
    Task<HouseDetailDto?> Get(int id);

    Task<HouseDetailDto> Add(HouseDetailDto dto);
    Task<HouseDetailDto> Update(HouseDetailDto dto);
    Task Delete(int id);
}
public class HouseRepository: IHouseRepository{
    private readonly HouseDbContext context;

    public HouseRepository(HouseDbContext context)
    {
        this.context = context;
    }

    private static void DtoToEntity(HouseDetailDto dto, HouseEntity e){
        e.Address = dto.Address;
        e.Country = dto.Country;
        e.Description = dto.Description;
        e.Photo = dto.Photo;
        e.Price = dto.Price;
    }

    private static HouseDetailDto EntityToDetailDto(HouseEntity e){
        return new HouseDetailDto(e.Id, e.Address, e.Country, e.Price, 
        e.Description, e.Photo);
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
        return EntityToDetailDto(e);
    }

    public async Task<HouseDetailDto> Add(HouseDetailDto dto){
        var e = new HouseEntity();
        DtoToEntity(dto, e);
        context.Houses.Add(e);
        await context.SaveChangesAsync();
        return EntityToDetailDto(e);
    }

    public async Task<HouseDetailDto> Update(HouseDetailDto dto){
        var e = await context.Houses.FindAsync(dto.Id);
        if(e == null)
            throw new ArgumentException($"House with ID {dto.Id} not found.");
        DtoToEntity(dto, e);
        context.Entry(e).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return EntityToDetailDto(e);
    }

    public async Task Delete(int id){
        var e = await context.Houses.FindAsync(id);
        if(e == null)
            throw new ArgumentException($"Error deleting house with ID {id} not found.");
        context.Houses.Remove(e);
        await context.SaveChangesAsync();
    }
}