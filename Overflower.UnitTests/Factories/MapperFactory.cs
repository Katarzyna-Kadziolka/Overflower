using System.Reflection;
using Mapster;
using MapsterMapper;
using Overflower.Application;

namespace Overflower.UnitTests.Factories;

public static class MapperFactory
{
    private static readonly Lazy<IMapper> LazyMapper = new(InitMapper, 
        LazyThreadSafetyMode.ExecutionAndPublication);

    private static IMapper InitMapper()
    {
        var config = new TypeAdapterConfig();
        config.Scan(Assembly.GetAssembly(typeof(IApplicationMarker))!);
        return new Mapper(config);
    }

    public static IMapper Mapper => LazyMapper.Value;
}