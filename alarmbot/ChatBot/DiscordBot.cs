// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.Setting;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace alarmbot.ChatBot
{
    public class DiscordBotIdentifier : BotUserIdentifier
    {
        public ulong id { get; private set; }

        public DiscordBotIdentifier(ulong id)
        {
            this.id = id;
        }

        public override bool Equals(BotUserIdentifier other)
        {
            if (!(other is DiscordBotIdentifier))
                return false;
            return id == (other as DiscordBotIdentifier).id;
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }

    public class DiscordBot : BotModel
    {
        DiscordSocketClient client;

        public override Task SendMessage(BotUserIdentifier user, string message)
        {
            var embed = new EmbedBuilder();
            embed.Title = "인하대 알림봇";
            embed.WithColor(Color.Blue);
            embed.Description = message;
            //embed.Footer = new EmbedFooterBuilder().
            //embed.Url = "https://inhaalarmbot.kro.kr";
            //var footer = new EmbedFooterBuilder();
            //footer.Text = "봇 초대 https://inhaalarmbot.kro.kr";
            //footer.IconUrl = "https://inhaalarmbot.kro.kr";
            
            //embed.Footer = footer;
            //embed.Fields.Add(new EmbedFieldBuilder { Name = "봇 초대", Value = "https://inhaalarmbot.kro.kr" });
            return (client.GetChannel((user as DiscordBotIdentifier).id) as IMessageChannel).SendMessageAsync("", false, embed.Build());
        }

        public override Task SendMessage(UserDBModel udb, string message)
        {
            return SendMessage(new DiscordBotIdentifier(Convert.ToUInt64(udb.ChatBotId)), message);
        }

        public override void Start()
        {
            client = new DiscordSocketClient();

            client.MessageReceived += Client_MessageReceived;
            client.GuildAvailable += Client_GuildAvailable;

            client.LoginAsync(TokenType.Bot, Settings.Instance.Model.BotSettings.DiscordClientId).Wait();
            client.StartAsync().Wait();
        }

        private async Task Client_GuildAvailable(SocketGuild arg)
        {
            ;
        }

        private async Task Client_MessageReceived(SocketMessage arg)
        {
            if (arg is SocketSystemMessage)
            {
                var message = arg as SocketSystemMessage;
                if (message.Type == MessageType.GuildMemberJoin)
                {
                    await BotAPI.ProcessMessage(this, new DiscordBotIdentifier(arg.Channel.Id), "/start");
                }
            }
            else
            {
                if (arg.Content == null || arg.Content == "") return;
                await BotAPI.ProcessMessage(this, new DiscordBotIdentifier(arg.Channel.Id), arg.Content);
            }
        }
    }
}
