namespace MarvelChallange.Infrastructure.ExternalService.ProfileMappings;

public static class ProfileMapping
{
    public static void RegisterMappings(TypeAdapterConfig config)
    {
        config.NewConfig<InfraDto.MarvelDto, AppDto.MarvelDto>().TwoWays();
        config.NewConfig<InfraDto.MarvelDataDto, AppDto.MarvelDataDto>().TwoWays();
        config.NewConfig<InfraDto.MarvelResultDto, AppDto.MarvelResultDto>().TwoWays();
        config.NewConfig<InfraDto.MarvelStructureDto, AppDto.MarvelStructureDto>().TwoWays();
        config.NewConfig<InfraDto.MarveltemsDto, AppDto.MarveltemsDto>().TwoWays();
        config.NewConfig<InfraDto.MarvelThumbNailDto, AppDto.MarvelThumbNailDto>().TwoWays();
    }
}
