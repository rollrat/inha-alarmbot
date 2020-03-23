// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using System;
using System.Collections.Generic;
using System.Text;

namespace alarmbot
{
    public class Version
    {
        public const int MajorVersion = 2020;
        public const int MinorVersion = 03;
        public const int BuildVersion = 21;

        public const string Name = "Inha Alarm Bot";
        public static string Text { get; } = $"{MajorVersion}.{MinorVersion}.{BuildVersion}";
    }
}
