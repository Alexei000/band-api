namespace BandApi.Services
{
    public interface IPropertyValidationService
    {
        bool HasValidProperties<T>(string fields);
    }
}