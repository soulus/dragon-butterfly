using System.Collections.Generic;

namespace MemcachedRepositoryProvider.Base
{
    public interface IMemcachedRepositoryProvider
    {
        IList<MemcachedEnvironment> GetEnvironments();
        void SaveChanges();
        void AddNewEnvironment(MemcachedEnvironment environment);
        void AddNewServer(MemcachedServer server);
        //IList<MemcachedServer> GetServers(MemcachedEnvironment environment);
    }

    public class MemcachedServer
    {
        public int Id { get; set; }
        public MemcachedEnvironment Environment { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }

    public class MemcachedEnvironment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<MemcachedServer> Servers { get; set; }
    }
}
