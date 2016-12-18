using Microsoft.Azure.Batch;
using Microsoft.Azure.Batch.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureConnection
{
    public class Job
    {
        public async Task Run()
        {
            using (var batchClient = BatchClient.Open(Settings.Credentials))
            {
                await CreateJobAsync(batchClient, Settings.JobId, Settings.PoolId);

                //// Add the tasks to the job. We need to supply a container shared access signature for the
                //// tasks so that they can upload their output to Azure Storage.
                await AddTasksAsync(batchClient, Settings.JobId); //, inputFiles, outputContainerSasUrl);
                await MonitorTasks(batchClient, Settings.JobId, TimeSpan.FromMinutes(30));

                Console.WriteLine("Done");

                await batchClient.JobOperations.DeleteJobAsync(Settings.JobId);
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

        private static async Task<List<CloudTask>> AddTasksAsync(BatchClient batchClient, string jobId) //, List<ResourceFile> inputFiles, string outputContainerSasUrl)
        {
            // Create a collection to hold the tasks that we'll be adding to the job
            List<CloudTask> tasks = new List<CloudTask>();

            // Create each of the tasks. Because we copied the task application to the
            // node's shared directory with the pool's StartTask, we can access it via
            // the shared directory on whichever node each task will run.
            //foreach (ResourceFile inputFile in inputFiles)
            //{
            //string taskId = "topNtask" + inputFiles.IndexOf(inputFile);

            for (int i = 0; i < 10; i++)
            {
                var taskCommandLine = string.Format("cmd /c %AZ_BATCH_NODE_SHARED_DIR%\\TaskApplication.exe");
                CloudTask task = new CloudTask(i.ToString(), taskCommandLine);
                //task.ResourceFiles = new List<ResourceFile> { inputFile };
                tasks.Add(task);
            }

            //}

            // Add the tasks as a collection opposed to a separate AddTask call for each. Bulk task submission
            // helps to ensure efficient underlying API calls to the Batch service.
            await batchClient.JobOperations.AddTaskAsync(jobId, tasks);

            return tasks;
        }

        private static async Task<bool> MonitorTasks(BatchClient batchClient, string jobId, TimeSpan timeout)
        {
            bool allTasksSuccessful = true;
            const string successMessage = "All tasks reached state Completed.";
            const string failureMessage = "One or more tasks failed to reach the Completed state within the timeout period.";

            // Obtain the collection of tasks currently managed by the job. Note that we use a detail level to
            // specify that only the "id" property of each task should be populated. Using a detail level for
            // all list operations helps to lower response time from the Batch service.
            ODATADetailLevel detail = new ODATADetailLevel(selectClause: "id");
            List<CloudTask> tasks = await batchClient.JobOperations.ListTasks(jobId, detail).ToListAsync();

            Console.WriteLine("Awaiting task completion, timeout in {0}...", timeout.ToString());

            // We use a TaskStateMonitor to monitor the state of our tasks. In this case, we will wait for all tasks to
            // reach the Completed state.
            TaskStateMonitor taskStateMonitor = batchClient.Utilities.CreateTaskStateMonitor();
            try
            {
                await taskStateMonitor.WhenAll(tasks, TaskState.Completed, timeout);
            }
            catch (TimeoutException)
            {
                await batchClient.JobOperations.TerminateJobAsync(jobId, failureMessage);
                Console.WriteLine(failureMessage);
                return false;
            }

            await batchClient.JobOperations.TerminateJobAsync(jobId, successMessage);

            // All tasks have reached the "Completed" state, however, this does not guarantee all tasks completed successfully.
            // Here we further check each task's ExecutionInfo property to ensure that it did not encounter a scheduling error
            // or return a non-zero exit code.

            // Update the detail level to populate only the task id and executionInfo properties.
            // We refresh the tasks below, and need only this information for each task.
            detail.SelectClause = "id, executionInfo";

            foreach (CloudTask task in tasks)
            {
                // Populate the task's properties with the latest info from the Batch service
                await task.RefreshAsync(detail);

                if (task.ExecutionInformation.SchedulingError != null)
                {
                    // A scheduling error indicates a problem starting the task on the node. It is important to note that
                    // the task's state can be "Completed," yet still have encountered a scheduling error.

                    allTasksSuccessful = false;

                    Console.WriteLine("WARNING: Task [{0}] encountered a scheduling error: {1}", task.Id, task.ExecutionInformation.SchedulingError.Message);
                }
                else if (task.ExecutionInformation.ExitCode != 0)
                {
                    // A non-zero exit code may indicate that the application executed by the task encountered an error
                    // during execution. As not every application returns non-zero on failure by default (e.g. robocopy),
                    // your implementation of error checking may differ from this example.

                    allTasksSuccessful = false;

                    Console.WriteLine("WARNING: Task [{0}] returned a non-zero exit code - this may indicate task execution or completion failure.", task.Id);
                }
            }

            if (allTasksSuccessful)
            {
                Console.WriteLine("Success! All tasks completed successfully within the specified timeout period.");
            }

            return allTasksSuccessful;
        }
    }
}
