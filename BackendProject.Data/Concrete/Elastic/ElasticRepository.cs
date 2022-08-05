using BackendProject.Data.Abstract;
using BackendProject.Entity.DomainModels;
using LinqToElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProject.Data.Concrete.Elastic
{
    public class ElasticRepository : IMonitorRepository
    {
        private readonly ElasticClient _elasticClient;

        public ElasticRepository(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Monitor GetMonitorData(string requestId)
        {
            throw new NotImplementedException();
        }

        public List<Monitor> MonitoringDataList()
        {
            var elasticQuery = new ElasticQueryable<Monitor>(_elasticClient, "backendproject-development-2022-07");

            var result = elasticQuery.ToList();

              return result;
        }
    }
}
