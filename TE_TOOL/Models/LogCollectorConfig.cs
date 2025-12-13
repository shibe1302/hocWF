using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TE_TOOL.Models
{
    internal class LogCollectorConfig
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Protocol { get; set; }
        public string PortNumber { get; set; }
        public string LocalDownloadDestination { get; set; }
        public string WinscpDLL { get; set; }
        public  List<string> RemoteFolderScan { get; set; }
        public string MacFilePath { get; set; }
        public string MaxThreadScan { get; set; }
        public bool ScanLocalMode { get; set; }
    }
}
