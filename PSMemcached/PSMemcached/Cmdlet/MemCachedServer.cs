using System.Linq;
using System.Management.Automation;
using MemcachedRepositoryProvider;
using MemcachedRepositoryProvider.Base;
using PSMemcached.Base;
using PSMemcached.Repository;

namespace PSMemcached.Cmdlet
{
    [Cmdlet(VerbsCommon.Add, "MemCachedServer")]
    public class AddMemCachedServer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Environment;

        [Parameter(Position = 1, Mandatory = true)]
        public string ServerIp;

        [Parameter(Position = 2, Mandatory = true)]
        protected int ServerPort = 11211;

        [Parameter(Position = 3, Mandatory = false)]
        public string ServerName;

        protected override void ProcessRecord()
        {
            using (var repository = new MemCachedEFRepository())
            {
                var environment = repository.GetEnvironments().FirstOrDefault(env => env.Name == this.Environment);
                if (environment == null)
                {
                    this.WriteObject(string.Format("Environment {0} doesn't exists in the collection", this.Environment));
                }
                if (environment.Servers.Any(server => server.IpAddress == this.ServerIp))
                {
                    this.WriteObject("Server" + ServerIp + " already exists in the collection");
                }
                repository.AddNewServer(new MemcachedServer()
                    {
                        Environment = environment,
                        IpAddress = ServerIp,
                        Name = ServerName,
                        Port = ServerPort
                    });
            }
        }
    }
}
