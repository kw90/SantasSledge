using Microsoft.Azure.Batch;
using Microsoft.Azure.Batch.Common;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureConnection
{
    public class Pool
    {
        public async Task Create()
        {
            var storageAccount = CloudStorageAccount.Parse(Settings.StorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            const string appContainerName = "application";

            var applicationFilePaths = new List<string>
            {
                typeof(JobApp.Program).Assembly.Location,
                "Microsoft.WindowsAzure.Storage.dll"
            };

            //List<string> inputFilePaths = new List<string>
            //{
            //    @"..\..\taskdata1.txt",
            //    @"..\..\taskdata2.txt",
            //    @"..\..\taskdata3.txt"
            //};

            var applicationFiles = await UploadFilesToContainerAsync(blobClient, appContainerName, applicationFilePaths);
            //List<ResourceFile> inputFiles = await UploadFilesToContainerAsync(blobClient, inputContainerName, inputFilePaths);

            // Obtain a shared access signature that provides write access to the output container to which
            // the tasks will upload their output.
            //var outputContainerSasUrl = GetContainerSasUrl(blobClient, outputContainerName, SharedAccessBlobPermissions.Write);
            using (BatchClient batchClient = BatchClient.Open(Settings.Credentials))
            {
                await CreatePoolIfNotExistAsync(batchClient, Settings.PoolId, applicationFiles);
            }
        }

        public async Task Delete()
        {
            using (var batchClient = BatchClient.Open(Settings.Credentials))
            {
                await batchClient.PoolOperations.DeletePoolAsync(Settings.PoolId);
            }
        }

        private static async Task CreateContainerIfNotExistAsync(CloudBlobClient blobClient, string containerName)
        {
            var container = blobClient.GetContainerReference(containerName);

            if (await container.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Container [{0}] created.", containerName);
            }
            else
            {
                Console.WriteLine("Container [{0}] exists, skipping creation.", containerName);
            }
        }

        private static async Task<List<ResourceFile>> UploadFilesToContainerAsync(CloudBlobClient blobClient, string inputContainerName, List<string> filePaths)
        {
            List<ResourceFile> resourceFiles = new List<ResourceFile>();

            foreach (string filePath in filePaths)
            {
                resourceFiles.Add(await UploadFileToContainerAsync(blobClient, inputContainerName, filePath));
            }

            return resourceFiles;
        }

        private static async Task<ResourceFile> UploadFileToContainerAsync(CloudBlobClient blobClient, string containerName, string filePath)
        {
            Console.WriteLine("Uploading file {0} to container [{1}]...", filePath, containerName);

            string blobName = Path.GetFileName(filePath);

            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blobData = container.GetBlockBlobReference(blobName);
            await blobData.UploadFromFileAsync(filePath, FileMode.Open);

            // Set the expiry time and permissions for the blob shared access signature. In this case, no start time is specified,
            // so the shared access signature becomes valid immediately
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(2),
                Permissions = SharedAccessBlobPermissions.Read
            };

            // Construct the SAS URL for blob
            string sasBlobToken = blobData.GetSharedAccessSignature(sasConstraints);
            string blobSasUri = String.Format("{0}{1}", blobData.Uri, sasBlobToken);

            return new ResourceFile(blobSasUri, blobName);
        }

        private static async Task CreatePoolIfNotExistAsync(BatchClient batchClient, string poolId, IList<ResourceFile> resourceFiles)
        {
            CloudPool pool = null;
            try
            {
                Console.WriteLine("Creating pool [{0}]...", poolId);

                pool = batchClient.PoolOperations.CreatePool(
                    poolId: poolId,
                    targetDedicated: Settings.NofCors,
                    virtualMachineSize: "small",                                                // single-core, 1.75 GB memory, 225 GB disk
                    cloudServiceConfiguration: new CloudServiceConfiguration(osFamily: "4"));   // Windows Server 2012 R2

                pool.StartTask = new StartTask
                {
                    CommandLine = "cmd /c (robocopy %AZ_BATCH_TASK_WORKING_DIR% %AZ_BATCH_NODE_SHARED_DIR%) ^& IF %ERRORLEVEL% LEQ 1 exit 0",
                    ResourceFiles = resourceFiles,
                    WaitForSuccess = true
                };

                await pool.CommitAsync();
            }
            catch (BatchException be)
            {
                if (be.RequestInformation?.BatchError != null && be.RequestInformation.BatchError.Code == BatchErrorCodeStrings.PoolExists)
                {
                    Console.WriteLine("The pool {0} already existed when we tried to create it", poolId);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
