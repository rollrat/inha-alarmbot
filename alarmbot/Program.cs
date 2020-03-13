// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.Log;
using alarmbot.Network;
using alarmbot.Postprocessor;
using alarmbot.Setting;
using alarmbot.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace alarmbot
{
    public class Program
    {
        public static string ApplicationPath = Directory.GetCurrentDirectory();
        public static string DefaultSuperPath = Directory.GetCurrentDirectory();

        public static Dictionary<string, object> Instance =>
            InstanceMonitor.Instances;

        public static NetScheduler Scheduler { get; set; }

        public static PostprocessorScheduler PPScheduler { get; set; }

        public static DateTime StartTime = DateTime.Now;

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            GCLatencyMode oldMode = GCSettings.LatencyMode;
            RuntimeHelpers.PrepareConstrainedRegions();
            GCSettings.LatencyMode = GCLatencyMode.Batch;

            ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            Scheduler = new NetScheduler(Settings.Instance.Model.ThreadCount);
            PPScheduler = new PostprocessorScheduler(Settings.Instance.Model.PostprocessorThreadCount);

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            try
            {
                Command.Start(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured! " + e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Please, check log.txt file.");
            }

            Environment.Exit(0);
        }
    }
}
