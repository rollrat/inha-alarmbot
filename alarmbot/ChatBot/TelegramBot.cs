// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

using alarmbot.Setting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace alarmbot.ChatBot
{
    public class TelegramBot : BotModel
    {
        TelegramBotClient bot;

        public TelegramBot()
        {
            bot = new TelegramBotClient(Settings.Instance.Model.BotSettings.TelegramBotAccessToken);
        }

        public override Task SendMessage(BotUserIdentifier user, string message)
        {
            return bot.SendTextMessageAsync((user as TelegramBotIdentifier).user, message, disableWebPagePreview: true);
        }

        public override void Start()
        {
            ReceiverOptions receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            bot.StartReceiving(
                updateHandler: handleUpdateAsync,
                pollingErrorHandler: handlePollingErrorAsync,
                receiverOptions: receiverOptions
            );
        }

        public override Task SendMessage(UserDBModel udb, string message)
        {
            return SendMessage(new TelegramBotIdentifier(Convert.ToInt64(udb.ChatBotId)), message);
        }

        async Task handleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message == null || update.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text) return;
            await BotAPI.ProcessMessage(this, new TelegramBotIdentifier(update.Message.Chat.Id), update.Message.Text);
        }

        Task handlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
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
