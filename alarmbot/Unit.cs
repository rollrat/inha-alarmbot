// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.Network;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

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
        public static (string, string, string) ExtractHtml(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var classify= document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[1]/dd[1]").InnerText.Trim();
            var datetime = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[2]/dd[1]").InnerText.Trim();
            var title = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[2]/h2[1]").InnerText.Trim();
            return (classify, datetime, title);
        }
        public static void FilteringArticle()
        {
            Directory.CreateDirectory(Path.Combine(Program.ApplicationPath, "maytrash"));
            foreach (var file in Directory.GetFiles(Path.Combine(Program.ApplicationPath, "tmp")))
            {
                try
                {
                    var html = File.ReadAllText(file);
                    var cc = Unit.ExtractHtml(html);

                    Console.WriteLine($"{cc.Item1}, {cc.Item2}, {cc.Item3}");
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
