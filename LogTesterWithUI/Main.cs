using Logging4Net;
using NLogging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace LogTesterWithUI
{
    public class Main
    {
        public ICommand DoSomethingCommand;
        public ObservableCollection<ItemsForDataGrid> DGrid { get; set; } = new ObservableCollection<ItemsForDataGrid>();

        public Main()
        {
            Enumerable.Range(0, 10000).Select(x => x.ToString()).ToList().ForEach(x => DGrid.Add(new ItemsForDataGrid { Name = x, Name2 = $"{x}{x}" }));
            
            StartLogger();
            
        }

        private void StartLogger()
        {
            Log4NetTestLogger.SetNormalLogging();
           Task t = new Task(() =>
            {
                while(true)
                {
                    Application.Current.Dispatcher.Invoke(()=>Log4NetTestLogger.PrintText("LogLoglOg"));
                }
            });
            t.Start();
        }
    }
}
