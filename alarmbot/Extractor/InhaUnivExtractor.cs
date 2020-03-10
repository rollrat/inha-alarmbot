// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.DataBase;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace alarmbot.Extractor
{
    public class InhaUnivDBModel : SQLiteColumnModel
    {
        public string DateTime { get; set; }
        public string Classify { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
    }

    public class InhaUnivExtractor
    {
        public static InhaUnivDBModel Parse(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var pp = Enumerable.Range(1, 4).Select(i => ($"/html[1]/body[1]/div[1]/div[1]/div[1]/dl[{i}]/dt[1]", $"/html[1]/body[1]/div[1]/div[1]/div[1]/dl[{i}]/dd[1]"))
                .Select(x =>
                {
                    var cc = document.DocumentNode.SelectSingleNode(x.Item1);
                    if (cc == null)
                        return ("", "");
                    return (cc.InnerText.Trim(), document.DocumentNode.SelectSingleNode(x.Item2).InnerText.Trim());
                }).ToList();
            var title = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[2]/h2[1]").InnerText.Trim();

            var classify = pp.Exists(x => x.Item1 == "분류") ? pp.Single(x => x.Item1 == "분류").Item2 : "";
            var datetime = pp.Exists(x => x.Item1 == "작성일") ? pp.Single(x => x.Item1 == "작성일").Item2 : "";

            return new InhaUnivDBModel { Classify = classify, DateTime = datetime, Title = title };
        }
    }
}
