using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace ShuiwenService
{
    public partial class ShuiwenService : ServiceBase
    {
        public ShuiwenService()
        {
            InitializeComponent();
        }
        
        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch(); 
            DbServer.ReadConnString();
            WorkProc.Work();

        }

        protected override void OnStop()
        {
            WorkProc.Stop();
        }
    }
}
