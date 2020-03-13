// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.DataBase;
using alarmbot.Extractor;
using alarmbot.Network;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace alarmbot
{
    class Unit
    {
        /// <summary>
        /// inha.ac.kr article downloader
        /// </summary>
        public static void DownloadArticles()
        {
            var range = Enumerable.Range(0, 22332);
            var rc = range.Count();

            using (ProgressBar pb = new ProgressBar())
            {
                Directory.CreateDirectory(Path.Combine(Program.ApplicationPath, "tmp"));
                var download_count = 0;
                NetTools.DownloadFiles(range.Select(x => ($"https://www.inha.ac.kr/bbs/kr/8/{x}/artclView.do",
                    Path.Combine(Program.ApplicationPath, "tmp", x + ".html"))).ToList(),
                    download: (sz) => pb.Report(rc, download_count, sz), complete: () => Interlocked.Increment(ref download_count));
            }
        }
        public static void FilteringArticle()
        {
            Directory.CreateDirectory(Path.Combine(Program.ApplicationPath, "maytrash"));
            var w = ExtractManager.InhaUnivDB;
            var files = Directory.GetFiles(Path.Combine(Program.ApplicationPath, "tmp")).ToList();
            files.Sort((x, y) => Convert.ToInt32(Path.GetFileNameWithoutExtension(x)).CompareTo(Convert.ToInt32(Path.GetFileNameWithoutExtension(y))));
            foreach (var file in files)
            {
                try
                {
                    var html = File.ReadAllText(file);
                    var cc = InhaUnivExtractor.Parse(html);

                    Console.WriteLine($"{cc.Classify}, {cc.DateTime}, {cc.Title}");
                    cc.Link = $"https://www.inha.ac.kr/bbs/kr/8/{Path.GetFileNameWithoutExtension(file)}/artclView.do";
                    w.Add(cc);
                }
                catch
                {
                    Console.WriteLine("[Fail] " + file);
                    File.Move(file, Path.Combine(Program.ApplicationPath, "maytrash", Path.GetFileName(file)));
                }
            }

        }
    }
}
