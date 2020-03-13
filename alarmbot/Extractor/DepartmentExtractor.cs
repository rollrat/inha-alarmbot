// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.DataBase;
using alarmbot.Utils;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace alarmbot.Extractor
{
    public class DepartmentDBModel : SQLiteColumnModel
    {
        public string Department { get; set; }
        public string Number { get; set; } // Index id
        public string Title { get; set; }
        public string Author { get; set; }
        public string DateTime { get; set; }
        public string Views { get; set; }
        public string Link { get; set; }

        public override string ToString()
        {
            var prefix = "";
            if (Department == "CSE")
                prefix = "컴퓨터공학과";
            else if (Department == "MECH")
                prefix = "기계공학과";
            return $"[{prefix}] {Title} - {DateTime}\r\n{Link}";
        }
    }

    public class DepartmentExtractor
    {
        #region 경영대학

        public static List<DepartmentDBModel> ExtractBIZ(string html)
        {
            var result = new List<DepartmentDBModel>();
            var root_node = html.ToHtmlNode();
            for (int i = 1; ; i++)
            {
                var node = root_node.SelectSingleNode($"/html[1]/body[1]/div[1]/div[2]/div[2]/div[2]/div[1]/div[2]/form[2]/table[1]/tbody[1]/tr[{i}]");
                if (node == null) break;
                var pattern = new DepartmentDBModel();
                pattern.Number = node.SelectSingleNode("./td[1]").InnerText.Trim();
                if (pattern.Number == "")
                    continue;
                pattern.Title = HttpUtility.HtmlDecode(node.SelectSingleNode("./td[2]/a[1]").InnerText).Trim();
                pattern.Link = "https://dept.inha.ac.kr" + HttpUtility.HtmlDecode(node.SelectSingleNode("./td[2]/a[1]").GetAttributeValue("href", "").Trim());
                pattern.Author = node.SelectSingleNode("./td[3]").InnerText.Trim();
                pattern.DateTime = node.SelectSingleNode("./td[4]").InnerText.Trim();
                pattern.Views = node.SelectSingleNode("./td[5]").InnerText.Trim();
                pattern.Department = "CSE";
                result.Add(pattern);
            }
            result.Sort((x, y) => Convert.ToInt32(x.Number).CompareTo(Convert.ToInt32(y.Number)));
            return result;
        }

        #endregion

        #region 공과대학

        /// <summary>
        /// https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6594&siteId=cse
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<DepartmentDBModel> ExtractCSE(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var result = new List<DepartmentDBModel>();
            var root_node = document.DocumentNode;
            for (int i = 1; ; i++)
            {
                var node = root_node.SelectSingleNode($"/html[1]/body[1]/div[1]/div[2]/div[2]/div[2]/div[1]/div[2]/form[2]/table[1]/tbody[1]/tr[{i}]");
                if (node == null) break;
                var pattern = new DepartmentDBModel();
                pattern.Number = node.SelectSingleNode("./td[1]").InnerText.Trim();
                if (pattern.Number == "")
                    continue;
                pattern.Title = HttpUtility.HtmlDecode(node.SelectSingleNode("./td[2]/a[1]").InnerText).Trim();
                pattern.Link = "https://dept.inha.ac.kr" + HttpUtility.HtmlDecode(node.SelectSingleNode("./td[2]/a[1]").GetAttributeValue("href", "").Trim());
                pattern.Author = node.SelectSingleNode("./td[3]").InnerText.Trim();
                pattern.DateTime = node.SelectSingleNode("./td[4]").InnerText.Trim();
                pattern.Views = node.SelectSingleNode("./td[5]").InnerText.Trim();
                pattern.Department = "CSE";
                result.Add(pattern);
            }
            result.Sort((x,y) => Convert.ToInt32(x.Number).CompareTo(Convert.ToInt32(y.Number)));
            return result;
        }

        /// <summary>
        /// https://mech.inha.ac.kr/board_notice/list.aspx
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<DepartmentDBModel> ExtractMech(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var result = new List<DepartmentDBModel>();
            var root_node = document.DocumentNode;
            for (int i = 1; ; i++)
            {
                var node = root_node.SelectSingleNode($"/html[1]/body[1]/form[1]/div[3]/div[2]/div[1]/div[2]/div[1]/div[4]/div[1]/div[1]/div[1]/div[1]/div[1]/table[1]/tr[{1 + i}]");
                if (node == null) break;
                var pattern = new DepartmentDBModel();
                pattern.Department = "MECH";
                pattern.Number = node.SelectSingleNode("./td[1]").InnerText.Trim();
                if (pattern.Number == "")
                    continue;
                pattern.Title = node.SelectSingleNode("./td[2]").InnerText.Trim();
                pattern.Author = node.SelectSingleNode("./td[3]").InnerText.Trim();
                pattern.DateTime = node.SelectSingleNode("./td[4]").InnerText.Trim();
                pattern.Views = node.SelectSingleNode("./td[5]").InnerText.Trim();
                result.Add(pattern);
            }
            result.Sort((x, y) => Convert.ToInt32(x.Number).CompareTo(Convert.ToInt32(y.Number)));
            return result;
        }

        #endregion
    }
}
