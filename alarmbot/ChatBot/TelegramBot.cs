// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.Setting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace alarmbot.ChatBot
{
    public class TelegramBot : BotModel
    {
        TelegramBotClient bot;

        public TelegramBot()
        {
            bot = new TelegramBotClient(Settings.Instance.Model.BotSettings.TelegramBotAccessToken);

            bot.OnMessage += Bot_OnMessage;
        }

        public override Task SendMessage(BotUserIdentifier user, string message)
        {
            return bot.SendTextMessageAsync((user as TelegramBotIdentifier).user, message);
        }

        public override void Start()
        {
            bot.StartReceiving();
        }

        private async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message == null || e.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text) return;
            await BotAPI.ProcessMessage(this, new TelegramBotIdentifier(e.Message.Chat.Id), e.Message.Text);
        }

        public override Task SendMessage(UserDBModel udb, string message)
        {
            return SendMessage(new TelegramBotIdentifier(Convert.ToInt64(udb.ChatBotId)), message);
        }
    }

    public class TelegramBotIdentifier : BotUserIdentifier
    {
        public long user { get; private set; }

        public TelegramBotIdentifier(long user)
        {
            this.user = user;
        }

        public override bool Equals(BotUserIdentifier other)
        {
            if (!(other is TelegramBotIdentifier))
                return false;
            return user == (other as TelegramBotIdentifier).user;
        }

        public override string ToString()
        {
            return user.ToString();
        }
    }

}
