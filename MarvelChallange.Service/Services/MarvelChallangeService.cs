using MarvelChallange.Domain.Models;
using MarvelChallange.Domain.Models.External;
using MarvelChallange.Service.Services.Interfaces;

namespace MarvelChallange.Service.Services
{
    public class MarvelChallangeService : IMarvelChallangeService
    {
        public async Task AddToFile(MarvelDto marvelDto)
        {
            DateTime dateTimeNow = DateTime.Now;
            
            string fullFileName = $"{AppSettings.FileName}.{dateTimeNow.ToString("dd.MM.yyyy HH.mm.ss")}.{AppSettings.FileExtension}";

            using (StreamWriter sw = new StreamWriter(fullFileName, true))
            {
                await sw.WriteLineAsync($"File generated in {dateTimeNow.ToString("dd/MM/yyyy HH:mm:ss")}");
                await sw.WriteLineAsync("");

                foreach (MarvelResultDto result in marvelDto.Data.Results)
                {
                    int maxSeparator = 200;
                    await sw.WriteLineAsync(string.Empty.PadRight(maxSeparator, '='));
                    await sw.WriteLineAsync($"ID: {result.Id}");
                    await sw.WriteLineAsync($"Name: {result.Name}");
                    await sw.WriteLineAsync($"Description: {result.Description}");

                    await sw.WriteLineAsync("Comics (names):");
                    await Task.Run(() => result.Comics.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                    await sw.WriteLineAsync("");

                    await sw.WriteLineAsync($"Series (names):");
                    await Task.Run(() => result.Series.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                    await sw.WriteLineAsync("");

                    await sw.WriteLineAsync($"Stories (names):");
                    await Task.Run(() => result.Stories.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                    await sw.WriteLineAsync("");

                    await sw.WriteLineAsync($"Events (names):");
                    await Task.Run(() => result.Events.Items.ForEach(x => sw.WriteLine($"\t{x.Name}")));
                    await sw.WriteLineAsync("");

                    await sw.WriteLineAsync(string.Empty.PadRight(maxSeparator, '='));
                    await sw.WriteLineAsync("");
                }
            }
        }
    }
}
