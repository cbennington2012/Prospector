namespace Prospector.Domain.Contracts.AutoMapping
{
    public interface IAutoMapper
    {
        TD Map<TS, TD>(TS source);
        TD Map<TS, TD>(TS source, TD destination);
        void InitializeMaps();
    }
}
