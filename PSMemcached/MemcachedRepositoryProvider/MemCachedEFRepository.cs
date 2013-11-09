using MemcachedRepositoryProvider.Base;
using PSMemcached.Repository;
using PSMemcached.Repository.DatabaseInitializer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MemcachedRepositoryProvider
{
    public class MemCachedEFRepository : IMemcachedRepositoryProvider, IDisposable
    {
        private static MemcacheLocalContainer _container;

        public MemCachedEFRepository()
        {
            const string configFile = @"C:\Users\rlopez\PSMemCached\app.config";
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", configFile);
            if (_container != null) return;
            Database.SetInitializer(new DBSeeder());
            _container = new MemcacheLocalContainer();
        }

        public IList<MemcachedEnvironment> GetEnvironments()
        {
            return _container.Environments.Select(env => new MemcachedEnvironment()
                {
                    Id = env.Id,
                    Name = env.Name,
                    Servers = new List<MemcachedServer>(env.Servers.Select(serv => new MemcachedServer()
                        {
                            Id = serv.Id,
                            IpAddress = serv.IpAddress,
                            Name = serv.Name,
                            Port = serv.Port
                        }))
                }).ToList();
        }

        public void SaveChanges()
        {
            if (_container != null)
            {
                _container.SaveChanges();
            }
        }

        public void AddNewEnvironment(MemcachedEnvironment environment)
        {
            throw new NotImplementedException();
        }

        public void AddNewServer(MemcachedServer server)
        {
            var env = _container.Environments.First(s => s.Id == server.Environment.Id);
            env.Servers.Add(new Server()
                {
                    Environment = env,
                    EnvironmentId = env.Id,
                    IpAddress = server.IpAddress,
                    Name = server.Name,
                    Port = server.Port
                });
            SaveChanges();
        }

        public void Dispose()
        {
            if (_container != null)
                _container.Dispose();
        }
    }
}
