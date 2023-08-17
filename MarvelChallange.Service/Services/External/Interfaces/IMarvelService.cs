﻿using MarvelChallange.Domain.Models.External;

namespace MarvelChallange.Service.Services.External.Interfaces
{
    public interface IMarvelService
    {
        public Task<MarvelDto?> GetFullData();

        public Task ExportDataToFile();

        public Task DeleteAllFiles();
    }
}