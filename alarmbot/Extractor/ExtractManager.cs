// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

using alarmbot.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace alarmbot.Extractor
{
    public class ExtractManager
    {
        public static List<InhaUnivDBModel> InhaUnivArticles { get; set; }
        public static SQLiteWrapper<InhaUnivDBModel> InhaUnivDB { get; set; } = new SQLiteWrapper<InhaUnivDBModel>("database.db");

        public static List<DepartmentDBModel> DepartmentArticles { get; set; }
        public static SQLiteWrapper<DepartmentDBModel> DepartmentDB { get; set; } = new SQLiteWrapper<DepartmentDBModel>("database.db");

        public static HashSet<string> InhaUnivFilters = new HashSet<string>();

        public static HashSet<string> DepartmentFilters = new HashSet<string>();

        static ExtractManager()
        {
            InhaUnivArticles = InhaUnivDB.QueryAll();
            if (InhaUnivArticles.Count == 0)
                InhaUnivArticles.Add(new InhaUnivDBModel {
                    Link = "https://www.inha.ac.kr/bbs/kr/8/2222/artclView.do", 
                    DateTime = DateTime.MinValue.ToString(),
                    Classify = "교수"
                });
            InhaUnivArticles.Sort((x, y) => x.DateTime.CompareTo(y.DateTime));

            InhaUnivArticles.ForEach(x => x.Classify.Split('/').Where(y => y.Trim() != "").ToList().ForEach(y => InhaUnivFilters.Add(y)));

            DepartmentArticles = DepartmentDB.QueryAll();
            DepartmentFilters.Add("컴퓨터 공학과(CSE)");

        }
    }
}
