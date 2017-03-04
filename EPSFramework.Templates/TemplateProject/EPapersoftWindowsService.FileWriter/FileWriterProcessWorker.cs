namespace EPapersoftWindowsService.FileWriter
{
    using System;
    using System.Threading.Tasks;
    using Interfaces;
    using System.IO;
    using System.Diagnostics;

    public class FileWriterProcessWorker : ProcessWorkerBase
    {
        protected override void DoWorkInternal()
        {
            Debug.WriteLine("Starting to work");
            Debug.Flush();
            String[] thingsToWrite = new string[]
            {
                "Text",
                "To Add",
                "To a file",
                "As text"
            };

            System.Threading.Thread.Sleep(10000);

            using (StreamWriter dependentsWriter = new StreamWriter((Stream)File.Open(Path.Combine("DATA.csv"), FileMode.OpenOrCreate), System.Text.Encoding.GetEncoding(1252)))
            {
                int currentLineCount = 1;// line count as states by an excel file. 1 is header.
                foreach (var thing in thingsToWrite)
                {
                    dependentsWriter.WriteLine(thing);
                    Debug.WriteLine("Written " + thing);
                    currentLineCount++;
                }
            }

            System.Diagnostics.Debug.WriteLine("FINISHED WRITING TO FILE");
            Debug.Flush();
        }

        protected async override Task DoWorkInternalAsync()
        {
            Debug.WriteLine("Starting to work");
            Debug.Flush();
            String[] thingsToWrite = new string[]
            {
                 "Text",
                "To Add",
                "To a file",
                "As text"
            };

            System.Threading.Thread.Sleep(10000);

            using (StreamWriter dependentsWriter = new StreamWriter((Stream)File.Open(Path.Combine("DATA.csv"), FileMode.OpenOrCreate), System.Text.Encoding.GetEncoding(1252)))
            {
                int currentLineCount = 1;// line count as states by an excel file. 1 is header.
                foreach (var thing in thingsToWrite)
                {
                    await dependentsWriter.WriteLineAsync(thing);
                    Debug.WriteLine("Written " + thing);
                    currentLineCount++;
                }
            }

            System.Diagnostics.Debug.WriteLine("FINISHED WRITING TO FILE");
            Debug.Flush();
        }
    }
}