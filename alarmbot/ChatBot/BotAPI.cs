// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.DataBase;
using alarmbot.Extractor;
using alarmbot.Setting;
using alarmbot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alarmbot.ChatBot
{
    public class BotAPI
    {
        public static Task ProcessMessage<T>(T bot, BotUserIdentifier user, string msg) where T : BotModel
        {
            return Task.Run(async () =>
            {
                try
                {
                    var command = msg.Split(' ')[0];

                    Log.Logs.Instance.Push("[Bot] Received Message - " + msg + "\r\n" + Log.Logs.SerializeObject(user));

                    UserDBModel userdb = null;
                    var query_user = BotManager.Instance.Users.Where(x => x.ChatBotId == user.ToString()).ToList();
                    if (query_user != null && query_user.Count > 0)
                        userdb = query_user[0];

                    switch (command.ToLower())
                    {
                        case "/start":

                            {
                                if (userdb == null)
                                {
                                    userdb = new UserDBModel { ChatBotId = user.ToString(), ChatBotName = typeof(T).Name, Filtering = "" };
                                    BotManager.Instance.UserDB.Add(userdb);
                                    BotManager.Instance.Users.Add(userdb);
                                }
                            }

                            {
                                var builder = new StringBuilder();
                                builder.Append("인하대 알림봇\r\n");
                                builder.Append("\r\n");
                                builder.Append("인하대 알림봇에 정상적으로 등록되었습니다!\r\n");
                                builder.Append("'/help'를 통해 사용가능한 명령어를 확인해보세요!\r\n");
                                await bot.SendMessage(user, builder.ToString());
                            }

                            break;

                        case "/recent":

                            await bot.SendMessage(user, ExtractManager.InhaUnivArticles.Last().ToString());

                            break;

                        case "/filterlist":

                            {
                                var builder = new StringBuilder();
                                builder.Append($"[인하대 공식 홈페이지 필터링]\r\n");
                                ExtractManager.InhaUnivFilters.ToList().ForEach(x => builder.Append($"{x}\r\n"));
                                builder.Append($"\r\n");
                                builder.Append($"[학과 홈페이지 필터링]\r\n");
                                builder.Append($"추가 예정입니다!\r\n");
                                await bot.SendMessage(user, builder.ToString());
                            }

                            break;

                        case "/myfilter":

                            if (userdb.Filtering != null)
                                await bot.SendMessage(user, userdb.Filtering);

                            break;

                        case "/help":

                            {
                                var builder = new StringBuilder();
                                builder.Append($"인하대 알림봇 - {Version.Text}\r\n");
                                builder.Append("\r\n");
                                builder.Append("/recent => 가장 최근의 알림을 가져옵니다.\r\n");
                                builder.Append("/filterlist => 필터링 가능한 모든 목록을 가져옵니다.\r\n");
                                builder.Append("/myfilter => 내 필터링 정보를 가져옵니다.\r\n");
                                builder.Append("/rap => 관리자 권한을 요청합니다.\r\n");
                                builder.Append("/gg <메시지> => 채널 관리자에게 메시지를 남깁니다.(고장 문의 등)\r\n");
                                if (userdb.Option != null && userdb.Option.Contains("ADMIN"))
                                {
                                    builder.Append("/msg <Type> <Id> <메시지> => 특정 사용자에게 메시지를 보냅니다.\r\n");
                                    builder.Append("/notice <메시지> => 모든 사용자에게 메시지를 보냅니다.\r\n");
                                }
                                builder.Append("/info => 봇 정보를 가져옵니다.\r\n");
                                await bot.SendMessage(user, builder.ToString());
                            }

                            break;

                        case "/rap":

                            {
                                var cc = msg.Trim().Split(' ');
                                if (cc.Length == 1)
                                {
                                    await bot.SendMessage(user, "적절한 요청이 아닙니다!");
                                    return;
                                }

                                var aim = msg.Replace(cc[0], "").Trim();

                                if (!string.IsNullOrEmpty(Settings.Instance.Model.BotSettings.AccessIdentifierMessage) 
                                    && aim == Settings.Instance.Model.BotSettings.AccessIdentifierMessage)
                                {
                                    userdb.Option += "ADMIN,";
                                    BotManager.Instance.UserDB.Update(userdb);
                                    await bot.SendMessage(user, $"새로운 신원 '{user.ToString()}'에 관리자 권한을 추가했습니다.");
                                }
                                else
                                {
                                    await bot.SendMessage(user, "액세스 식별자가 비어있거나 입력값과 다릅니다.");
                                }
                            }

                            break;

                        case "/gg":

                            {
                                var builder = new StringBuilder();
                                builder.Append($"사용자 메시지\r\n");
                                builder.Append($"식별자: {userdb.ChatBotName} - {userdb.ChatBotId}\r\n");
                                builder.Append($"메시지: {msg.Replace(msg.Trim().Split(' ')[0], "").Trim()}");
                                await BotManager.Instance.Notice(builder.ToString(), "ADMIN-GG");
                            }

                            break;

                        case "/msg":

                            {
                                if (userdb.Option == null || !userdb.Option.Contains("ADMIN"))
                                {
                                    await bot.SendMessage(user, "이 명령을 이용할 권한이 없습니다.");
                                    return;
                                }
                                
                                var cc = msg.Trim().Split(' ');

                                var builder = new StringBuilder();
                                builder.Append($"[알림봇 관리자 메시지]\r\n");
                                builder.Append(string.Join(' ',cc.Skip(3)));
                                if (await BotManager.Instance.Send(builder.ToString(), cc[1], cc[2]))
                                    await bot.SendMessage(user, "성공");
                                else
                                    await bot.SendMessage(user, "실패");
                            }

                            break;

                        case "/notice":

                            {
                                if (userdb.Option == null || !userdb.Option.Contains("ADMIN"))
                                {
                                    await bot.SendMessage(user, "이 명령을 이용할 권한이 없습니다.");
                                    return;
                                }

                                var cc = msg.Trim().Split(' ');

                                var builder = new StringBuilder();
                                builder.Append($"[알림봇 공지사항]\r\n");
                                builder.Append(string.Join(' ', cc.Skip(1)));
                                await BotManager.Instance.Notice(builder.ToString(), "MSG-NOTICE");
                            }

                            break;

                        case "/info":

                            {
                                var builder = new StringBuilder();
                                builder.Append($"인하대 알림봇 - {Version.Text}\r\n");
                                builder.Append("\r\n");
                                builder.Append($"바이너리: {Version.Name} {Version.Text}\r\n");
                                builder.Append($"빌드시간: {Internals.GetBuildDate().ToLongDateString()}\r\n");
                                builder.Append($"실행시간: {Program.StartTime.ToLongTimeString()}\r\n");
                                builder.Append($"루프횟수: {Loop.Count}회 (10분마다 업데이트됨)\r\n");
                                //builder.Append($"소드코드: https://github.com/275ab2accb925ab0b5375f4aa0718e97/inha-alarm\r\n");
                                builder.Append($"이 서비스는 카카오톡 및 디스코드, 텔레그램으로도 제공됩니다.");
                                await bot.SendMessage(user, builder.ToString());
                            }

                            break;

                        default:
                            await bot.SendMessage(user, $"'{command}'은/는 적절한 명령어가 아닙니다!\r\n자세한 정보는 '/help' 명령어를 통해 확인해주세요!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    try
                    {
                        Log.Logs.Instance.PushError("[Bot Error] " + e.Message + "\r\n" + e.StackTrace);
                        await bot.SendMessage(user, "서버오류가 발생했습니다! 관리자에게 문의해주세요!");
                    }
                    catch (Exception e2)
                    {
                        Log.Logs.Instance.PushError("[Bot IError] " + e2.Message + "\r\n" + e2.StackTrace);
                    }
                }

            });
        }
    }
}
