namespace TestConsole.Loggers
{
    public class FileLogger : Logger
    {
        private int _Index;

        public string FilePath { get; }

        public FileLogger(string FileName)
        {
            this.FilePath = FileName;
        }

        public override void Log(string Message)
        {
            System.IO.File.AppendAllText(FilePath, $"{++_Index}:{Message}\n");
        }
    }
}