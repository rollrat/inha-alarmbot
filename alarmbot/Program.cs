// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.Network;
using alarmbot.Postprocessor;
using alarmbot.Utils;
using System;
using System.Collections.Generic;
using System.IO;

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
            Console.WriteLine("Hello World!");
        }
    }
}
