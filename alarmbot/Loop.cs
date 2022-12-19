// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

using alarmbot.ChatBot;
using alarmbot.Extractor;
using alarmbot.Network;
using alarmbot.Res;
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

                        Log.Logs.Instance.Push($"[Loop] New item is added. -  IUA  - {cc.Title}");

                        await BotManager.Instance.Notice(cc.ToString(), "MSG-MAIN");
                    }
                    catch { }
                }
            }

            // Department Notices
            {
                // Lazy downloading
                foreach (var department in DepartmentList.Lists)
                {
                    try
                    {
                        if (department.Item3 == "") continue;
                        var task = NetTask.MakeDefault(department.Item3);
                        if (department.Item2 == "s5")
                            task.Encoding = Encoding.GetEncoding(51949);
                        var html = NetTools.DownloadString(task);

                        List<DepartmentDBModel> cc = null;

                        if (department.Item2 == "s1")
                            cc = DepartmentExtractor.ExtractStyle1(html, department.Item1);
                        else if (department.Item2 == "s2")
                            cc = DepartmentExtractor.ExtractStyle2(html, department.Item1);
                        else if (department.Item2 == "s3")
                            cc = DepartmentExtractor.ExtractStyle3(html, department.Item1);
                        else if (department.Item2 == "s4")
                            cc = DepartmentExtractor.ExtractStyle4(html, department.Item1);
                        else if (department.Item2 == "s5")
                            cc = DepartmentExtractor.ExtractStyle5(html, department.Item1);
                        else if (department.Item2 == "s6")
                            cc = DepartmentExtractor.ExtractStyle6(html, department.Item1);
                        else if (department.Item2 == "s7")
                            cc = DepartmentExtractor.ExtractStyle7(html, department.Item1);
                        else
                            continue;

                        // get cse latest
                        var mm = new HashSet<int>();
                        ExtractManager.DepartmentArticles.Where(x => x.Department == department.Item1).ToList().ForEach(x => mm.Add(Convert.ToInt32(x.Number)));

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

                            Log.Logs.Instance.Push($"[Loop] New item is added. -  DN  - {cc[i].Title}");
                            await BotManager.Instance.Notice(cc[i].ToString(), "MSG-" + department.Item1);
                        }

                    }
                    catch (Exception e)
                    {
                        Log.Logs.Instance.PushError("[Loop] '" + department.Item1 + "' " + e.Message + "\r\n" + e.StackTrace);
                    }
                }
            }

            Log.Logs.Instance.Push("[Loop] Cycle " + Count.ToString());

            Count++;
        }
    }
}
