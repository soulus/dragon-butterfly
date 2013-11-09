using System.Collections.Generic;
using PSMemcached.Repository;


namespace PSMemcached.Base
{
    public class MemCacheRepository
    {
        private static Dictionary<string, IList<Server>> _memcacheServers;
        public static IDictionary<string, IList<Server>> MemcacheServers
        {
            get
            {
                if (_memcacheServers == null)
                {
                    _memcacheServers = new Dictionary<string, IList<Server>>();
                }
                return _memcacheServers;
            }
            set
            {
                _memcacheServers = (Dictionary<string, IList<Server>>)value;
            }
        }
    }
}
