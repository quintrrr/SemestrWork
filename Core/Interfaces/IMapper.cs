namespace Core.Interfaces
{
    public interface IMapper<TBlo, TDao>
    {
        TDao ToDao(TBlo blo);
        TBlo ToBlo(TDao dao);
    }
}
