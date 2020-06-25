using Medikit.Mobile.Models;
using Microsoft.Extensions.Options;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Medikit.Mobile.Services
{
    public class SqliteCertificateStore : ICertificateStore
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly MedikitMobileOptions _options;

        public SqliteCertificateStore(IOptions<MedikitMobileOptions> options)
        {
            _options = options.Value;
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Medikit.db3");
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<MedikitCertificate>().Wait();
            Init().Wait();
        }

        public Task<List<MedikitCertificate>> GetAll()
        {
            return _database.Table<MedikitCertificate>().ToListAsync();
        }

        public Task<MedikitCertificate> GetOrgCertificate()
        {
            return _database.Table<MedikitCertificate>().FirstOrDefaultAsync(_ => _.Type == MedikitCertificateTypes.ORG);
        }

        public Task<List<MedikitCertificate>> GetIdentityCertificates()
        {
            return _database.Table<MedikitCertificate>().OrderByDescending(_ => _.CreateDateTime).Where(_ => _.Type == MedikitCertificateTypes.IDENTITY).ToListAsync();
        }

        public Task<int> Remove(string name)
        {
            return _database.Table<MedikitCertificate>().DeleteAsync(_ => _.Name == name);
        }

        public Task<MedikitCertificate> Get(string name)
        {
            return _database.Table<MedikitCertificate>().FirstOrDefaultAsync(_ => _.Name == name);
        }

        public Task<int> Add(MedikitCertificate certificate)
        {
            return _database.InsertAsync(certificate);
        }

        public Task<int> Update(MedikitCertificate certificate)
        {
            return _database.UpdateAsync(certificate);
        }

        private async Task Init()
        {
            var result = await GetAll().ConfigureAwait(false);
            if (result.Any())
            {
                return;
            }

            await Add(new MedikitCertificate
            {
                Name = _options.OrgName,
                Password = _options.OrgPassword,
                Payload = _options.OrgCertificate,
                IsSelected = true,
                Type = MedikitCertificateTypes.ORG,
                CreateDateTime = DateTime.UtcNow
            }).ConfigureAwait(false);
        }
    }
}
