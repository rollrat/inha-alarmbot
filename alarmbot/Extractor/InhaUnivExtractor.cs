// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace alarmbot.Extractor
{
    public class InhaUnivDBModel : SQLiteColumnModel
    {
        public string DateTime { get; set; }
        public string Classify { get; set; }
        public string Title { get; set; }
    }

    public class InhaUnivExtractor
    {
    }
}
