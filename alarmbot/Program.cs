// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.Network;
using alarmbot.Postprocessor;
using alarmbot.Setting;
using alarmbot.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Runtime.CompilerServices;
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

        static void Main(string[] args)
        {
            // GC Setting
            GCLatencyMode oldMode = GCSettings.LatencyMode;
            RuntimeHelpers.PrepareConstrainedRegions();
            GCSettings.LatencyMode = GCLatencyMode.Batch;

            // Extends Connteion Limit
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            // Initialize Scheduler
            Scheduler = new NetScheduler(Settings.Instance.Model.ThreadCount);

            // Initialize Postprocessor Scheduler
            PPScheduler = new PostprocessorScheduler(Settings.Instance.Model.PostprocessorThreadCount);

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);


        }
    }
}
