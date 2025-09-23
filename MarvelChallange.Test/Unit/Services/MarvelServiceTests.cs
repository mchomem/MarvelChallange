using FluentAssertions;
using MarvelChallange.Core.Application.DTOs;
using MarvelChallange.Core.Application.Interfaces;
using MarvelChallange.Core.Application.Services;
using MarvelChallange.Core.Domain.Exceptions.Marvel;
using Microsoft.Extensions.Configuration;
using Moq;

namespace MarvelChallange.Tests.Unit.Services;

public class MarvelServiceTests : IDisposable
{
    private readonly Mock<IMarvelApiClient> _marvelApiClientMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly MarvelService _marvelService;
    private readonly string _testDirectory;
    private readonly string _fileName;
    private readonly string _fileExtension;

    public MarvelServiceTests()
    {
        _marvelApiClientMock = new Mock<IMarvelApiClient>();
        _configurationMock = new Mock<IConfiguration>();

        // Configuração mais simples e reutilizável
        _testDirectory = Path.Combine(Path.GetTempPath(), "MarvelTests", Guid.NewGuid().ToString());
        _fileName = "marvel_data";
        _fileExtension = "txt";
        
        SetupConfiguration();

        _marvelService = new MarvelService(_marvelApiClientMock.Object, _configurationMock.Object);
    }

    private void SetupConfiguration()
    {
        // Simplificação da configuração com método de extensão
        _configurationMock.SetupGetSection("FileExportData:FileOutputDirectory", _testDirectory);
        _configurationMock.SetupGetSection("FileExportData:FileName", _fileName);
        _configurationMock.SetupGetSection("FileExportData:FileExtension", _fileExtension);
    }

    [Fact]
    public async Task GetFullDataAsync_ShouldReturnMarvelDto_WhenApiClientReturnsData()
    {
        // Arrange
        var expectedMarvelDto = CreateSampleMarvelDto();
        _marvelApiClientMock.Setup(x => x.GetFullDataAsync())
            .ReturnsAsync(expectedMarvelDto);

        // Act
        var result = await _marvelService.GetFullDataAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedMarvelDto);
        _marvelApiClientMock.Verify(x => x.GetFullDataAsync(), Times.Once);
    }

    [Fact]
    public async Task GetFullDataAsync_ShouldReturnNull_WhenApiClientReturnsNull()
    {
        // Arrange
        _marvelApiClientMock.Setup(x => x.GetFullDataAsync())
            .ReturnsAsync((MarvelDto)null);

        // Act
        var result = await _marvelService.GetFullDataAsync();

        // Assert
        result.Should().BeNull();
        _marvelApiClientMock.Verify(x => x.GetFullDataAsync(), Times.Once);
    }
    
    [Fact]
    public async Task GetFullDataAsync_ShouldPropagateException_WhenApiClientThrows()
    {
        // Arrange
        var expectedException = new HttpRequestException("API Error");
        _marvelApiClientMock.Setup(x => x.GetFullDataAsync())
            .ThrowsAsync(expectedException);

        // Act & Assert
        await _marvelService.Invoking(x => x.GetFullDataAsync())
            .Should().ThrowAsync<HttpRequestException>()
            .WithMessage("API Error");
    }

    [Fact]
    public async Task ExportDataToFileAsync_ShouldReturnFilePath_WhenDataIsAvailable()
    {
        // Arrange
        var marvelDto = CreateSampleMarvelDto();
        _marvelApiClientMock.Setup(x => x.GetFullDataAsync())
            .ReturnsAsync(marvelDto);

        // Act
        var result = await _marvelService.ExportDataToFileAsync();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().StartWith(_testDirectory);
        result.Should().EndWith(".txt");
        File.Exists(result).Should().BeTrue();
        
        // Verificação mais detalhada do conteúdo
        var fileContent = await File.ReadAllTextAsync(result);
        fileContent.Should().Contain("Spider-Man");
        fileContent.Should().Contain("Peter Parker was bitten by a radioactive spider");
        fileContent.Should().Contain("Amazing Spider-Man #1");
        fileContent.Should().Contain("Amazing Spider-Man (1963 - 1998)");
    }

    [Fact]
    public async Task ExportDataToFileAsync_ShouldThrowMarvelException_WhenNoDataAvailable()
    {
        // Arrange
        _marvelApiClientMock
            .Setup(x => x.GetFullDataAsync())
            .ReturnsAsync((MarvelDto)null);

        // Act & Assert
        await _marvelService
            .Invoking(x => x.ExportDataToFileAsync())
            .Should().ThrowAsync<MarvelException>()
            .WithMessage("No data to export");
    }

    [Fact]
    public async Task AddToFileAsync_ShouldCreateFileWithCorrectStructure_WhenValidDataProvided()
    {
        // Arrange
        var marvelDto = CreateSampleMarvelDto();

        // Act
        var result = await _marvelService.AddToFileAsync(marvelDto);

        // Assert
        result.Should().NotBeNullOrEmpty();
        File.Exists(result).Should().BeTrue();

        var fileContent = await File.ReadAllTextAsync(result);
        
        // Verificações de estrutura
        fileContent.Should().Contain("File generated in");
        fileContent.Should().MatchRegex(@"ID: 1009610");
        fileContent.Should().Contain("Name: Spider-Man");
        fileContent.Should().Contain("Comics (names):");
        fileContent.Should().Contain("\tAmazing Spider-Man #1");
        fileContent.Should().Contain("Series (names):");
        fileContent.Should().Contain("Stories (names):");
        fileContent.Should().Contain("Events (names):");
    }

    [Fact]
    public async Task AddToFileAsync_ShouldHandleEmptyCollections_WhenItemsAreMissing()
    {
        // Arrange
        var marvelDto = new MarvelDto
        {
            Data = new MarvelDataDto
            {
                Results = new List<MarvelResultDto>
                {
                    new MarvelResultDto
                    {
                        Id = 1,
                        Name = "Test Character",
                        Description = "Test description",
                        Comics = new MarvelStructureDto { Items = new List<MarveltemsDto>() },
                        Series = new MarvelStructureDto { Items = new List<MarveltemsDto>() },
                        Stories = new MarvelStructureDto { Items = new List<MarveltemsDto>() },
                        Events = new MarvelStructureDto { Items = new List<MarveltemsDto>() }
                    }
                }
            }
        };

        // Act
        var result = await _marvelService.AddToFileAsync(marvelDto);

        // Assert
        File.Exists(result).Should().BeTrue();
        var fileContent = await File.ReadAllTextAsync(result);
        fileContent.Should().Contain("Test Character");
        fileContent.Should().NotContain("\t"); // Não deve haver itens indentados
    }

    [Fact]
    public async Task DeleteAllFilesAsync_ShouldRemoveDirectory_WhenDirectoryExists()
    {
        // Arrange
        Directory.CreateDirectory(_testDirectory);
        await File.WriteAllTextAsync(Path.Combine(_testDirectory, "test.txt"), "test content");

        // Act
        await _marvelService.DeleteAllFilesAsync();

        // Assert
        Directory.Exists(_testDirectory).Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAllFilesAsync_ShouldNotThrow_WhenDirectoryDoesNotExist()
    {
        // Arrange
        if (Directory.Exists(_testDirectory))
            Directory.Delete(_testDirectory, true);

        // Act & Assert
        await _marvelService
            .Invoking(x => x.DeleteAllFilesAsync())
            .Should().NotThrowAsync();
    }

    private static MarvelDto CreateSampleMarvelDto()
    {
        return new MarvelDto
        {
            Data = new MarvelDataDto
            {
                Results = new List<MarvelResultDto>
                {
                    new MarvelResultDto
                    {
                        Id = 1009610,
                        Name = "Spider-Man",
                        Description = "Peter Parker was bitten by a radioactive spider...",
                        Comics = new MarvelStructureDto
                        {
                            Items = new List<MarveltemsDto>
                            {
                                new MarveltemsDto { Name = "Amazing Spider-Man #1" },
                                new MarveltemsDto { Name = "Amazing Spider-Man #2" }
                            }
                        },
                        Series = new MarvelStructureDto
                        {
                            Items = new List<MarveltemsDto>
                            {
                                new MarveltemsDto { Name = "Amazing Spider-Man (1963 - 1998)" }
                            }
                        },
                        Stories = new MarvelStructureDto
                        {
                            Items = new List<MarveltemsDto>
                            {
                                new MarveltemsDto { Name = "Cover #19" }
                            }
                        },
                        Events = new MarvelStructureDto
                        {
                            Items = new List<MarveltemsDto>
                            {
                                new MarveltemsDto { Name = "Secret Wars" }
                            }
                        }
                    }
                }
            }
        };
    }

    public void Dispose()
    {
        // Limpeza após os testes
        try
        {
            if (Directory.Exists(_testDirectory))
                Directory.Delete(_testDirectory, true);
        }
        catch
        {
            // Ignora erros de limpeza
        }
    }
}

// Extensão para simplificar as configurações dos mocks
public static class ConfigurationMockExtensions
{
    public static void SetupGetSection(this Mock<IConfiguration> configurationMock, string sectionPath, string returnValue)
    {
        var section = new Mock<IConfigurationSection>();
        section.Setup(s => s.Value).Returns(returnValue);
        
        configurationMock
            .Setup(x => x.GetSection(sectionPath))
            .Returns(section.Object);
    }
}
