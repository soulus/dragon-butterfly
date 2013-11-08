using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemcachedRepositoryProvider.Base;
using PSMemcached.Repository;

namespace MemcachedRepositoryProvider
{
    public class MemCachedEFRepository:IMemcachedRepositoryProvider
    {
        private MemcacheLocalContainer _container;

        public MemCachedEFRepository()
        {
            _container = new MemcacheLocalContainer();
        }

        public IList<MemcachedEnvironment> GetEnvironments()
        {
            return _container.Environments.Select(env => new MemcachedEnvironment()
                {
                    Id =env.Id,
                    Name = env.Name,
                    Servers = new List<MemcachedServer>(env.Servers.Select(serv => new MemcachedServer()
                        {
                            Id=serv.Id,
                            IpAddress = serv.IpAddress,
                            Name = serv.Name,
                            Port = serv.Port
                        }))
                }).ToList();
        }

        //public IList<MemcachedServer> GetServers(MemcachedEnvironment environment)
        //{
        //    return _container.Servers.Select(server => new MemcachedServer()
        //        {
        //            Id = server.Id,
        //            IpAddress = server.IpAddress,
        //            Name = server.Name,
        //            Port = server.Port
        //        }).ToList();
        //}
    }
}
