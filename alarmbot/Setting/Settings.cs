﻿// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

using alarmbot.ChatBot;
using alarmbot.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace alarmbot.Setting
{
    public class SettingModel
    {
        public class NetworkSetting
        {
            public bool TimeoutInfinite;
            public int TimeoutMillisecond;
            public int DownloadBufferSize;
            public int RetryCount;
            public bool UsingProxyList;
            public bool UsingFreeProxy;
        }

        public NetworkSetting NetworkSettings;

        public BotSettings BotSettings;

        /// <summary>
        /// Scheduler Thread Count
        /// </summary>
        public int ThreadCount;

        /// <summary>
        /// Postprocessor Scheduler Thread Count
        /// </summary>
        public int PostprocessorThreadCount;

        /// <summary>
        /// Provider Language
        /// </summary>
        public string Language;

        /// <summary>
        /// Parent Path for Downloading
        /// </summary>
        public string SuperPath;
    }

    public class Settings : ILazy<Settings>
    {
        public const string Name = "settings.json";

        public SettingModel Model { get; set; }
        public SettingModel.NetworkSetting Network { get { return Model.NetworkSettings; } }

        public Settings()
        {
            var full_path = Path.Combine(Program.ApplicationPath, Name);
            if (File.Exists(full_path))
                Model = JsonConvert.DeserializeObject<SettingModel>(File.ReadAllText(full_path));

            if (Model == null)
            {
                Model = new SettingModel
                {
                    Language = GetLanguageKey(),
                    ThreadCount = Environment.ProcessorCount,
                    PostprocessorThreadCount = 3,

                    NetworkSettings = new SettingModel.NetworkSetting
                    {
                        TimeoutInfinite = false,
                        TimeoutMillisecond = 10000,
                        DownloadBufferSize = 131072,
                        RetryCount = 10,
                        UsingProxyList = false,
                        UsingFreeProxy = false,
                    },

                    BotSettings = new BotSettings()
                    {
                        EnableTelegramBot = false,
                        TelegramBotAccessToken = "",
                        EnableKakaoBot = false,
                        KakaoSkillServerPort = "",
                        EnableDiscordBot = false,
                        DiscordClientId = "",
                        AccessIdentifierMessage = ""
                    },

                };
            }
            Save();
        }

        public static string GetLanguageKey()
        {
            var lang = Thread.CurrentThread.CurrentCulture.ToString();
            var language = "all";
            switch (lang)
            {
                case "ko-KR":
                    language = "korean";
                    break;

                case "ja-JP":
                    language = "japanese";
                    break;

                case "en-US":
                    language = "english";
                    break;
            }
            return language;
        }

        /// <summary>
        /// Recover incorrect configuration.
        /// </summary>
        public void Recover()
        {
            if (string.IsNullOrWhiteSpace(Model.Language))
                Model.Language = GetLanguageKey();

            if (Model.ThreadCount <= 0 || Model.ThreadCount >= 128)
                Model.ThreadCount = Environment.ProcessorCount;
            if (Model.PostprocessorThreadCount <= 0 || Model.PostprocessorThreadCount >= 128)
                Model.ThreadCount = 3;
            if (string.IsNullOrWhiteSpace(Model.SuperPath))
                Model.SuperPath = Program.DefaultSuperPath;

            if (Model.NetworkSettings == null)
            {
                Model.NetworkSettings = new SettingModel.NetworkSetting
                {
                    TimeoutInfinite = false,
                    TimeoutMillisecond = 10000,
                    DownloadBufferSize = 131072,
                    RetryCount = 10,
                    UsingProxyList = false,
                    UsingFreeProxy = false,
                };
            }

            if (Model.NetworkSettings.TimeoutMillisecond < 1000)
                Model.NetworkSettings.TimeoutMillisecond = 10000;
            if (Model.NetworkSettings.DownloadBufferSize < 100000)
                Model.NetworkSettings.DownloadBufferSize = 131072;
            if (Model.NetworkSettings.RetryCount < 0)
                Model.NetworkSettings.RetryCount = 10;

            if (Model.BotSettings == null)
            {
                Model.BotSettings = new BotSettings()
                {
                    EnableTelegramBot = false,
                    TelegramBotAccessToken = "",
                    EnableKakaoBot = false,
                    KakaoSkillServerPort = "",
                    EnableDiscordBot = false,
                    DiscordClientId = "",
                    AccessIdentifierMessage = ""
                };
            }
        }

        public void Save()
        {
            var full_path = Path.Combine(Program.ApplicationPath, Name);
            var json = JsonConvert.SerializeObject(Model, Formatting.Indented);
            using (var fs = new StreamWriter(new FileStream(full_path, FileMode.Create, FileAccess.Write)))
            {
                fs.Write(json);
            }
            Program.DefaultSuperPath = Model.SuperPath;
        }
    }
}
