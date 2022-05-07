using AutoMapper;

namespace Citrouille.Shared.Mapping;

public interface IMapFrom<T>
{
    void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), GetType());
}