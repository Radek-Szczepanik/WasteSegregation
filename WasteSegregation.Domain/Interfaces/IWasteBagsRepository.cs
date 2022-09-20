namespace WasteSegregation.Domain.Interfaces;

public interface IWasteBagsRepository
{
    Task<WasteBag> AddAsync(WasteBag wasteBags);
}
