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
            var c1 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[1]/dt[1]").InnerText.Trim();
            var s1 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[1]/dd[1]").InnerText.Trim();
            var c2 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[2]/dt[1]").InnerText.Trim();
            var s2 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[2]/dd[1]").InnerText.Trim();
            var c3 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[3]/dt[1]").InnerText.Trim();
            var s3 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[3]/dd[1]").InnerText.Trim();
            var c4 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[3]/dt[1]").InnerText.Trim();
            var s4 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/dl[3]/dd[1]").InnerText.Trim();
            var title = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[2]/h2[1]").InnerText.Trim();

            var classify = "";
            var datetime = "";

            if (c1 == "작성일")
                datetime = s1;
            else if (c2 == "작성일")
                datetime = s2;
            else if (c3 == "작성일")
                datetime = s3;
            else if (c4 == "작성일")
                datetime = s4;

            if (c1 == "분류")
                classify = s1;
            else if (c2 == "분류")
                classify = s2;
            else if (c3 == "분류")
                classify = s3;
            else if (c4 == "분류")
                classify = s4;

            return (classify, datetime, title);
        }
        public static void FilteringArticle()
        {
            Directory.CreateDirectory(Path.Combine(Program.ApplicationPath, "maytrash"));
            var w = new SQLiteWrapper<InhaUnivDBModel>("database.db");
            foreach (var file in Directory.GetFiles(Path.Combine(Program.ApplicationPath, "tmp")))
            {
                try
                {
                    var html = File.ReadAllText(file);
                    var cc = Unit.ExtractHtml(html);

                    Console.WriteLine($"{cc.Item1}, {cc.Item2}, {cc.Item3}");
                    w.Add(new InhaUnivDBModel { Classify = cc.Item1, DateTime = cc.Item2, Title = cc.Item3 });
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
