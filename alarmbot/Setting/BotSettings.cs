﻿// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

using System;
using System.Collections.Generic;
using System.Text;

namespace alarmbot.ChatBot
{
    public class BotSettings
    {
        public bool EnableTelegramBot;
        public string TelegramBotAccessToken;

        public bool EnableKakaoBot;
        public string KakaoSkillServerPort;

        public bool EnableDiscordBot;
        public string DiscordClientId;

        public string AccessIdentifierMessage;
    }
}
