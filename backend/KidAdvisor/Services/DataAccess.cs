using KidAdvisor.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidAdvisor.Services
{
    public class DataAccess
    {
        private static readonly string EndpointUri = "https://localhost:8081";

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "ImageDB";
        private string containerId = "CustomerImages";
        public DataAccess()
        {
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });

        }

        public async Task<IEnumerable<Image>> GetImages()
        {
            this.database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            this.container = await database.CreateContainerIfNotExistsAsync(containerId, "/_id", 400);

            var sqlQueryText = "SELECT * FROM c";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Image> queryResultSetIterator = this.container.GetItemQueryIterator<Image>(queryDefinition);

            List<Image> images = new List<Image>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Image> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Image image in currentResultSet)
                {
                    images.Add(image);
                }
            }

            return images;
        }

        public async Task<Image> Create(Image p)
        {
            this.database = cosmosClient.GetDatabase(databaseId);
            this.container = await database.CreateContainerIfNotExistsAsync(containerId, "/_id", 400);
            // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
            ItemResponse<Image> andersenFamilyResponse = await this.container.CreateItemAsync<Image>(p); //, new PartitionKey(p.URL)
            return p;
        }

    }
}
