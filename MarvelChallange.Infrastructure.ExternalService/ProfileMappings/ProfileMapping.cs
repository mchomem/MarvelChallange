namespace MarvelChallange.Infrastructure.ExternalService.ProfileMappings;

public static class ProfileMapping
{
    public static void RegisterMappings(TypeAdapterConfig config)
    {
        config.NewConfig<MarvelChallange.Infrastructure.ExternalService.DTOs.MarvelDto, MarvelChallange.Core.Application.DTOs.MarvelDto>().TwoWays();
        config.NewConfig<MarvelChallange.Infrastructure.ExternalService.DTOs.MarvelDataDto, MarvelChallange.Core.Application.DTOs.MarvelDataDto>().TwoWays();
        config.NewConfig<MarvelChallange.Infrastructure.ExternalService.DTOs.MarvelResultDto, MarvelChallange.Core.Application.DTOs.MarvelResultDto>().TwoWays();
        config.NewConfig<MarvelChallange.Infrastructure.ExternalService.DTOs.MarvelStructureDto, MarvelChallange.Core.Application.DTOs.MarvelStructureDto>().TwoWays();
        config.NewConfig<MarvelChallange.Infrastructure.ExternalService.DTOs.MarveltemsDto, MarvelChallange.Core.Application.DTOs.MarveltemsDto>().TwoWays();
        config.NewConfig<MarvelChallange.Infrastructure.ExternalService.DTOs.MarvelThumbNailDto, MarvelChallange.Core.Application.DTOs.MarvelThumbNailDto>().TwoWays();
    }
}
