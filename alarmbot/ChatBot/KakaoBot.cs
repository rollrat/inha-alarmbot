// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

using alarmbot.Setting;
using EmbedIO;
using EmbedIO.Actions;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Newtonsoft.Json;
using Swan.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace alarmbot.ChatBot
{
    public class KakaoBotIdentifier : BotUserIdentifier
    {
        public KakaoBotSkillServer.SkillPayload.User user;

        public override bool Equals(BotUserIdentifier other)
        {
            if (!(other is KakaoBotIdentifier))
                return false;
            return user.id == (other as KakaoBotIdentifier).user.id;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(user, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }
    }

    public class KakaoBotSkillServer
    {
        public static async void StartServer()
        {
            Logger.UnregisterLogger<ConsoleLogger>();
            using (var server = CreateWebServer($"http://*:{Settings.Instance.Model.BotSettings.KakaoSkillServerPort}/"))
            {
                await server.RunAsync();
            }
        }

        public class SkillPayload
        {
            public class Block
            {
                public string id;
                public string name;
            }

            public class User
            {
                public string id;
                public string type;
                public Dictionary<string, string> properties;
            }

            public class UserRequest
            {
                public string timezone;
                public Block block;
                public string utterance;
                public string lang;
                public User user;
            }

            public class Bot
            {
                public string id;
                public string name;
            }

            public class Action
            {
                public string id;
                public string name;
                [JsonProperty(PropertyName = "params")]
                public Dictionary<string, string> @params { get; set; }
                public Dictionary<string, object> detailParams;
                public Dictionary<string, object> clientExtra;
            }

            public UserRequest userRequest;
            public Bot bot;
            public Action action;
        }

        public class SkillResponse
        {
            public class SkillTemplate
            {
                public class Component
                {
                    public class SimpleText
                    {
                        public string text;
                    }

                    public class SimpleImage
                    {
                        public string imageUrl;
                        public string altText;
                    }

                    public class Link
                    {
                        public string mobile;
                        public string ios;
                        public string android;
                        public string pc;
                        public string mac;
                        public string win;
                        public string web;
                    }

                    public class Button
                    {
                        public string label;
                        public string action;
                        public string webLinkUrl;
                        public Link osLink;
                        public string messageText;
                        public string phoneNumber;
                        public string blockId;
                        public Dictionary<string, object> extra;
                    }

                    public class Thumbnail
                    {
                        public string imageUrl;
                        public Link link;
                        public bool fixedRatio;
                        public int width;
                        public int height;
                    }

                    public class Profile
                    {
                        public string nickname;
                        public string imageUrl;
                    }

                    public interface Carouselable { }

                    public class BasicCard : Carouselable
                    {
                        public class Social
                        {
                        }

                        public string title;
                        public string description;
                        public Thumbnail thumbnail;
                        public Profile profile;
                        public Social social;
                        public Button[] buttons;
                    }

                    public class CommerceCard : Carouselable
                    {
                        public string description;
                        public int price;
                        public string currency;
                        public int discount;
                        public int discountRate;
                        public int dicountedPrice;
                        public Thumbnail[] thumbnails;
                        public Profile profile;
                        public Button[] buttons;
                    }

                    public class ListCard
                    {
                        public class ListItem
                        {
                            public string title;
                            public string description;
                            public string imageUrl;
                            public Link link;
                        }

                        public ListItem header;
                        public ListItem[] items;
                        public Button[] buttons;
                    }

                    public class Carousel
                    {
                        public class CarouselHeader
                        {
                            public string title;
                            public string description;
                            public Thumbnail thumbnail;
                        }

                        public string type;
                        public Carouselable[] items;
                        public CarouselHeader header;
                    }

                    public SimpleText simpleText;
                    public SimpleImage simpleImage;
                    public BasicCard basicCard;
                    public CommerceCard commerceCard;
                    public ListCard listCard;
                    public Carousel carousel;
                }

                public class QuickReply
                {
                    public string label;
                    public string action;
                    public string messageText;
                    public string blockId;
                    public object extra;
                }

                public Component[] outputs;
                public QuickReply[] quickReplies;
            }

            public class ContextControl
            {
                public class ContextValue
                {
                    public string name;
                    public int lifeSpan;
                    [JsonProperty(PropertyName = "params")]
                    public Dictionary<string, string> @params { get; set; }
                }

                public ContextValue values;
            }

            public string version;
            public SkillTemplate template;
            public ContextControl context;
            public Dictionary<string, object> data;
        }

        public class ServerAPI : WebApiController
        {
            [BaseRoute(HttpVerbs.Post, "/")]
            public void PostedData()
            {
                var json = HttpContext.GetRequestBodyAsStringAsync().Result;
                var payload = JsonConvert.DeserializeObject<SkillPayload>(json);

                var output = new SkillResponse
                {
                    version = "2.0",
                    template = new SkillResponse.SkillTemplate 
                    {
                        outputs = new SkillResponse.SkillTemplate.Component[]
                        {
                            new SkillResponse.SkillTemplate.Component
                            {
                                simpleText = new SkillResponse.SkillTemplate.Component.SimpleText 
                                {
                                    text = "Test"
                                }
                            }
                        }
                    },
                    data = new Dictionary<string, object> { {"msg","asdf" } }
                };

                //HttpContext.SendStringAsync("Test data! " + dd, "text", Encoding.UTF8);
                HttpContext.SendStringAsync(JsonConvert.SerializeObject(output, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }), "text", Encoding.UTF8);
            }
        }

        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithLocalSessionManager()
                .WithWebApi("/", m => m
                    .WithController<ServerAPI>());

            return server;
        }
    }

    public class KakaoBot
    {
    }
}
