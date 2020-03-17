// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.ChatBot;
using alarmbot.Extractor;
using alarmbot.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alarmbot
{
    public class Loop
    {
        public static int Count { get; private set; }

        public static async Task LoopInternal()
        {
            // Inha Univ Article
            {
                var range = Enumerable.Range(Convert.ToInt32(ExtractManager.InhaUnivArticles.Last().Link.Split('/')[6]) + 1, 5).ToList();

                var htmls = NetTools.DownloadStrings(range.Select(x => $"https://www.inha.ac.kr/bbs/kr/8/{x}/artclView.do").ToList());

                for (int i = 0; i < htmls.Count; i++)
                {
                    try
                    {
                        var cc = InhaUnivExtractor.Parse(htmls[i]);

                        cc.Link = $"https://www.inha.ac.kr/bbs/kr/8/{range[i]}/artclView.do";

                        ExtractManager.InhaUnivArticles.Add(cc);
                        ExtractManager.InhaUnivDB.Add(cc);

                        await BotManager.Instance.Notice(cc.ToString(), "MSG-MAIN");
                    }
                    catch { }
                }
            }

            // CSE Department
            {
                var url = "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6594&siteId=cse";
                var html = NetTools.DownloadString(url);

                try
                {
                    while (true)
                    {
                        var cc = DepartmentExtractor.ExtractStyle1(html, "CSE");

                        // get cse latest
                        var mm = new HashSet<int>();
                        ExtractManager.DepartmentArticles.Where(x => x.Department == "CSE").ToList().ForEach(x => mm.Add(Convert.ToInt32(x.Number)));

                        int starts = 0;
                        for (starts = cc.Count - 1; starts >= 0; starts--)
                        {
                            if (mm.Contains(Convert.ToInt32(cc[starts].Number)))
                                break;
                        }

                        for (int i = starts + 1; i < cc.Count; i++)
                        {
                            ExtractManager.DepartmentArticles.Add(cc[i]);
                            ExtractManager.DepartmentDB.Add(cc[i]);

                            await BotManager.Instance.Notice(cc[i].ToString(), "MSG-CSE");
                        }

                        break;
                    }
                }
                catch { }
            }

            Count++;
        }
    }
}
