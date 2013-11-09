using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSMemcached.Repository.DatabaseInitializer
{
    public class DBSeeder:DropCreateDatabaseIfModelChanges<MemcacheLocalContainer>
    {
        protected override void Seed(MemcacheLocalContainer context)
        {
            context.Environments.Add(new Environment() {Name = "Candidate"});
            context.Environments.Add(new Environment() { Name = "Stage" });
            context.Environments.Add(new Environment() { Name = "Production" });
            context.Environments.Add(new Environment() { Name = "Test01" });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
