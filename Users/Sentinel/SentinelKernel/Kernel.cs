﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SentinelKernel.System.FileSystem.VFS;
using Sys = Cosmos.System;

namespace SentinelKernel
{
    public class Kernel : Sys.Kernel
    {
        private VFSBase myVFS;

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully.");
            myVFS = new SentinelVFS();
            VFSManager.RegisterVFS(myVFS);
        }

        protected override void Run()
        {
            bool xTest = Directory.Exists("0:\\test");
            Stop();
        }
    }
}