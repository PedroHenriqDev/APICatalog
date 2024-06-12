namespace APICatalog.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
        {
            this.loggerName = loggerName;
            this.loggerConfig = loggerConfig;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = $"Log Level: {logLevel.ToString()} - Event Id:{eventId.Id} - Details:{formatter(state, exception)}";
            WriteMessageInLogFile(message);
        }

        public void WriteMessageInLogFile(string message) 
        {
            try
            {
                string pathFile = "C:\\LOG TEST\\ApiLog.txt";
                using (var streamWriter = new StreamWriter(pathFile, true))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }
    }
}
