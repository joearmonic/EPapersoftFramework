namespace EPapersoftWindowsService.FileWriter
{
    using Interfaces;

    public class FileWriterProcessWorkerFactory : ProcessWorkerFactory
    {
        public override ProcessWorkerBase CreateWorker()
        {
            return new FileWriterProcessWorker();
        }
    }
}