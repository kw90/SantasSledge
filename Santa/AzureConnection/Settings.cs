using Microsoft.Azure.Batch.Auth;

namespace AzureConnection
{
    public class Settings
    {
        public const int NofCors = 20;

        public const string BatchAccountName = "santabatchaccount";
        public const string BatchAccountKey = "oW5sXItb1MRQOSc028plqklHQe3CCPbXb2VYWZ1WDADfP2Wzsa74p9OBzWffTF8/GFU1ww/vwiQ2vjc0JCkntA==";
        public const string BatchAccountUrl = "https://santabatchaccount.westeurope.batch.azure.com";

        private const string storageAccountName = "santastorageaccount";
        private const string storageAccountKey = "aaj58ddxtXlx67OAnQ8trzqQonO2Ap/zrUc6AkflNEagmOQOnFmCLPMgbPyogg/HkoX3dYDxuokA9q6/ZYgsiA==";

        public static string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

        public const string WorkSpace = @"C:\Users\linri\Desktop\Santa";
        public static string TaskApplicationPath = WorkSpace + @"\TaskApplication";

        public static BatchSharedKeyCredentials Credentials = new BatchSharedKeyCredentials(BatchAccountUrl, BatchAccountName, BatchAccountKey);

        public const string PoolId = "santasPool";
        public const string JobId = "santasJob";
    }
}
