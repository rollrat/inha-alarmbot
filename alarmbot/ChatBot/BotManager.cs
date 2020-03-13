// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.DataBase;
using alarmbot.Setting;
using alarmbot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alarmbot.ChatBot
{
    public class UserDBModel : SQLiteColumnModel
    {
        public string ChatBotName { get; set; } // telegram, kakao, ...
        public string ChatBotId { get; set; }  // Identifier
        public string Filtering { get; set; }
        public string Option { get; set; }
    }

    public class BotManager : ILazy<BotManager>
    {
        Dictionary<string, BotModel> bots = new Dictionary<string, BotModel>();

        public SQLiteWrapper<UserDBModel> UserDB { get; set; }
        public List<UserDBModel> Users { get; set; }

        public void StartBots()
        {
            UserDB = new SQLiteWrapper<UserDBModel>("users.db");
            Users = UserDB.QueryAll();

            if (Settings.Instance.Model.BotSettings.EnableTelegramBot)
                bots.Add("TelegramBot", new TelegramBot());



            bots.ToList().ForEach(x => x.Value.Start());
        }

        public async Task Notice(string contents, string type)
        {
            if (type.StartsWith("MSG-"))
                await Task.WhenAll(Users.Select(user => bots[user.ChatBotName].SendMessage(user, contents)));
            else if (type.StartsWith("ADMIN-"))
                await Task.WhenAll(Users.Where(x => x.Option != null && x.Option.Contains("ADMIN")).Select(user => bots[user.ChatBotName].SendMessage(user, contents)));
        }

        public async Task<bool> Send(string contents, string type, string id)
        {
            var xx = Users.Where(x => x.ChatBotId == id && x.ChatBotName == type).ToList();
            if (xx.Count == 0)
                return false;
            await Task.WhenAll(Users.Where(x => x.ChatBotId == id && x.ChatBotName == type).Select(user => bots[user.ChatBotName].SendMessage(user, contents)));
            return true;
        }
    }
}
