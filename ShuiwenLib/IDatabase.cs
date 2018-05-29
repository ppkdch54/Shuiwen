using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuiwenLib
{
    public interface IDatabase
    {
        byte[] ReadConfig();
        bool SaveConfig(byte[] config);
        bool TestConnect();
    }
}
