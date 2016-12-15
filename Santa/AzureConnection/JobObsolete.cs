using Microsoft.Azure.Batch;
using Microsoft.Azure.Batch.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureConnection
{
    public class JobObsolete
    {
        public async Task Run()
        {
            using (var batchClient = BatchClient.Open(Settings.Credentials))
            {
                await CreateJobAsync(batchClient, Settings.JobId, Settings.PoolId);

                //// Clean up Batch resources (if the user so chooses)
                //Console.WriteLine();
                //Console.Write("Delete job? [yes] no: ");
                //string response = Console.ReadLine().ToLower();
                //if (response != "n" && response != "no")
                //{
                //    await batchClient.JobOperations.DeleteJobAsync(JobId);
                //}
            }
        }

        private static async Task CreateJobAsync(BatchClient batchClient, string jobId, string poolId)
        {
            Console.WriteLine("Creating job [{0}]...", jobId);

            CloudJob job = batchClient.JobOperations.CreateJob();
            job.Id = jobId;
            job.PoolInformation = new PoolInformation { PoolId = poolId };

            await job.CommitAsync();
        }
    }
}
