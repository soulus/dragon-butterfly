using System.Linq;
using System.Management.Automation;
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

        [Parameter(Position = 2, Mandatory = false)]
        public string ServerName;



        protected override void ProcessRecord()
        {
            if (!MemCacheRepository.MemcacheServers.ContainsKey(this.Environment))
            {
                this.WriteObject(string.Format("Environment {0} doesn't exists in the collection", this.Environment));
            }
            if (MemCacheRepository.MemcacheServers[this.Environment].Any(server => server.IpAddress == this.ServerIp))
            {
                this.WriteObject("Server" + ServerIp + " already exists in the collection");
            }
            {
                var servers = MemCacheRepository.MemcacheServers[this.Environment];
                var environment = 
                servers.Add(new Server(){Environment = this.Environment, IpAddress = this.ServerIp, Name = this.ServerName});
            }
        }
    }
}
