namespace EPapersoftWindowsService.FileWriter
{
    using Interfaces;

    public class FileWriterProcessWorkerFactory : ProcessWorkerBaseFactory
    {
        public override ProcessWorkerBase CreateWorker()
        {
            return new FileWriterProcessWorker();
        }
    }
}