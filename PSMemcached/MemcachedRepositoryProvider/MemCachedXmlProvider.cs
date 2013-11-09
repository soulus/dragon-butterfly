using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MemcachedRepositoryProvider.Base;

namespace MemcachedRepositoryProvider
{
    public class MemCachedXmlProvider : IMemcachedRepositoryProvider
    {
        private static XDocument _container;
        public MemCachedXmlProvider()
        {
            Initialize(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "PSMemcachedConfiguration.xml"));
        }
        public MemCachedXmlProvider(string filePath)
        {
            Initialize(filePath);
        }

        private void Initialize(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                CreateInitialConfigurationFile(filePath);
            }
        }

        private void CreateInitialConfigurationFile(string filePath)
        {
            _container = new XDocument();
            _container.Add(new XElement("PSMemcachedConfiguration", new XElement("Environments")));
            AddNewEnvironment(new MemcachedEnvironment()
                {
                    Name = "Candidate"
                });
            var candidate =
                _container.Element("Environments")
                        .Descendants("Environment").First(x => x.Attribute("Name").Value == "Candidate");
            AddNewServer(new MemcachedServer()
                            {
                                Environment = new MemcachedEnvironment()
                                    {
                                        Id = Convert.ToInt32(candidate.Attribute("Id").Value),
                                        Name = candidate.Attribute("Name").Value
                                    },
                                Name = "Candidate1",
                                IpAddress = "10.200.225.29",
                                Id = 1
                            });
            //new XElement("Environment", 
            //        new XAttribute("Id",1), 
            //        new XAttribute("Name", "Candidate"),
            //        new XElement("Servers", new XElement("Server",
            //                                                new XAttribute("Id", 1),
            //                                                new XAttribute("Name", "Candidate1"),
            //                                                new XAttribute("IpAddress", "10.200.225.29"))
            //            ))));
            _container.Save(filePath);
        }

        public void AddNewEnvironment(MemcachedEnvironment environment)
        {
            var xElement = _container.Element("PSMemcachedConfiguration");
            if (xElement == null) return;
            var element = xElement.Element("Environments");
            if (element != null)
            {
                var maxCount = element.Elements().Count();
                element
                    .Add(   new XElement("Environment",
                                         new XAttribute("Id", maxCount),
                                         new XAttribute("Name", environment.Name)));
            }
        }

        public void AddNewServer(MemcachedServer server)
        {
            throw new NotImplementedException();
        }

        public IList<MemcachedEnvironment> GetEnvironments()
        {

        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

    }
}
