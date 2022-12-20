// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

using alarmbot.Extractor;
using alarmbot.Res;
using alarmbot.Setting;
using alarmbot.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                    BotManager.Instance.Messages.Add(new ChatMessage { ChatBotName = typeof(T).Name, Identification = user.ToString(), RawMessage = msg });

                    var command = msg.Split(' ')[0];
                    var filter = new HashSet<string>();

                    // IsSenario = 0/1
                    // Senario = <string>
                    var option = new Dictionary<string, string>();

                    Log.Logs.Instance.Push("[Bot] Received Message - " + msg + "\r\n" + Log.Logs.SerializeObject(user));

                    UserDBModel userdb = null;
                    var query_user = BotManager.Instance.Users.Where(x => x.ChatBotId == user.ToString()).ToList();
                    if (query_user != null && query_user.Count > 0)
                    {
                        userdb = query_user[0];
                        if (userdb.Filtering != null)
                            userdb.Filtering.Split(",").ToList().ForEach(x => filter.Add(x));
                        if (userdb.Option != null)
                            option = JsonConvert.DeserializeObject<Dictionary<string, string>>(userdb.Option);
                    }

                    if (option.ContainsKey("IsSenario") && option["IsSenario"] == "1")
                    {
                        await Senario(bot, user, msg, userdb, option, filter);
                        return;
                    }

                    switch (command.ToLower())
                    {
                        case "/start":
                            await commandInternalStart(bot, user, userdb);
                            break;

                        case "/my":
                            await commandInternalMy(bot, msg, user, userdb);
                            break;

                        case "/recent":
                            await commandInternalRecent(bot, msg, user, userdb);
                            break;

                        case "/today":
                            await commandInternalToday(bot, msg, user, userdb);
                            break;

                        case "/dt":
                            await commandInternalDt(bot, msg, user, userdb);
                            break;

                        case "/search":
                            await commandInternalSearch(bot, msg, user, userdb);
                            break;

                        case "/filterlist":
                            await commandInternalFilterList(bot, msg, user, userdb);
                            break;

                        case "/myfilter":
                            if (userdb.Filtering != null)
                                await bot.SendMessage(user, userdb.Filtering);
                            break;

                        case "/help":
                            await commandInternalHelp(bot, msg, user, userdb, filter);
                            break;

                        case "/rap":
                            await commandInternalRap(bot, msg, user, userdb);
                            break;

                        case "/gg":
                            await commandInternalGg(bot, msg, user, userdb);
                            break;

                        case "/msg":
                            await commandInternalMsg(bot, msg, user, userdb, filter);
                            break;

                        case "/notice":
                            await commandInternalNotice(bot, msg, user, userdb, filter);
                            break;

                        case "/info":
                            await commandInternalInfo(bot, msg, user, userdb);
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

        static async Task commandInternalStart<T>(T bot, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            userdb = new UserDBModel { ChatBotId = user.ToString(), ChatBotName = typeof(T).Name, Filtering = "" };
            BotManager.Instance.UserDB.Add(userdb);
            BotManager.Instance.Users.Add(userdb);

            Dictionary<string, string> option;
            var builder = new StringBuilder();
            builder.Append("인하대 알림봇\r\n");
            builder.Append("\r\n");
            builder.Append("인하대 알림봇을 구독해주셔서 감사합니다!\r\n");
            builder.Append("기본적인 설정을 위해 다음 옵션 중 하나를 선택해주세요!\r\n");
            builder.Append("\r\n");
            builder.Append("1. 인하대 공식 공지사항만 받겠습니다.\r\n");
            builder.Append("2. 학과 공지사항도 같이 받겠습니다.\r\n");
            builder.Append("\r\n");
            builder.Append("'1' 또는 '2' 숫자만 입력해주세요.\r\n");
            option = new Dictionary<string, string>
                {
                    { "IsSenario", "1" },
                    { "Senario", "start01" }
                };
            userdb.Option = JsonConvert.SerializeObject(option);
            BotManager.Instance.UserDB.Update(userdb);
            await bot.SendMessage(user, builder.ToString());
        }

        static string idbModelToString(List<IDBModel> items, int count)
        {
            items.Sort((x, y) => x.DateTime.CompareTo(y.DateTime));
            var builder = new StringBuilder();

            foreach (var item in items.TakeLast(count))
            {
                builder.Append(item.ToString());
                builder.Append("\n\n");
            }

            return builder.ToString();
        }

        static async Task commandInternalMy<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            var count = 5;
            var cc = msg.Trim().Split(' ');

            if (cc.Length == 2)
            {
                if (!int.TryParse(cc[1], out count))
                {
                    await bot.SendMessage(user, "적절한 요청이 아닙니다!");
                    return;
                }
            }

            var userDept = userdb.Filtering.Split(",");

            var items = new List<IDBModel>();

            if (userDept.Length > 0)
            {
                foreach (var item in ExtractManager.DepartmentArticles)
                {
                    if (userDept.Contains(item.Department))
                    {
                        if (item.DateTime != null)
                            items.Add(item);
                    }
                }
            }

            await bot.SendMessage(user, idbModelToString(items, count).ToString());
        }

        static async Task commandInternalRecent<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            var count = 5;
            var cc = msg.Trim().Split(' ');

            if (cc.Length == 2)
            {
                if (!int.TryParse(cc[1], out count))
                {
                    await bot.SendMessage(user, "적절한 요청이 아닙니다!");
                    return;
                }
            }

            var userDept = userdb.Filtering.Split(",");

            var items = new List<IDBModel>();

            foreach (var item in ExtractManager.InhaUnivArticles.TakeLast(count))
            {
                if (item.DateTime != null)
                    items.Add(item);
            }

            if (userDept.Length > 0)
            {
                foreach (var item in ExtractManager.DepartmentArticles)
                {
                    if (userDept.Contains(item.Department))
                    {
                        if (item.DateTime != null)
                            items.Add(item);
                    }
                }
            }

            await bot.SendMessage(user, idbModelToString(items, count).ToString());
        }

        static async Task commandInternalToday<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            var items = new List<IDBModel>();

            var today = DateTime.Now;
            var provider = CultureInfo.InvariantCulture;

            foreach (var item in ExtractManager.InhaUnivArticles)
            {
                if (item.DateTime != "" &&
                    Math.Abs(DateTime.ParseExact(item.DateTime.TrimEnd('.'), "yyyy.MM.dd", provider).Subtract(today).Days) <= 2)
                {
                    items.Add(item);
                }
            }

            foreach (var item in ExtractManager.DepartmentArticles)
            {
                if (item.DateTime != "" &&
                    Math.Abs(DateTime.ParseExact(item.DateTime.TrimEnd('.'), "yyyy.MM.dd", provider).Subtract(today).Days) <= 2)
                {
                    items.Add(item);
                }
            }

            await bot.SendMessage(user, idbModelToString(items, 20).ToString());
        }

        static async Task commandInternalDt<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            var cc = msg.Trim().Split(' ');

            if (cc.Length == 1)
            {
                await bot.SendMessage(user, "적절한 요청이 아닙니다!");
                return;
            }

            var items = new List<IDBModel>();

            foreach (var item in ExtractManager.InhaUnivArticles)
            {
                if (item.DateTime != "" && item.DateTime.TrimEnd('.') == cc[1])
                {
                    items.Add(item);
                }
            }

            foreach (var item in ExtractManager.DepartmentArticles)
            {
                if (item.DateTime != "" && item.DateTime.TrimEnd('.') == cc[1])
                {
                    items.Add(item);
                }
            }

            await bot.SendMessage(user, idbModelToString(items, items.Count).ToString());
        }

        static async Task commandInternalSearch<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            var cc = msg.Trim().Split(' ');

            if (cc.Length == 1)
            {
                await bot.SendMessage(user, "적절한 요청이 아닙니다!");
                return;
            }

            var search = string.Join(' ', cc.Skip(1));

            if (search.Length < 2)
            {
                await bot.SendMessage(user, "최소 두 글자의 검색어를 입력해주세요!");
                return;
            }

            var items = new List<IDBModel>();

            foreach (var item in ExtractManager.InhaUnivArticles)
            {
                if (item.DateTime != "" && item.Title.Contains(search))
                {
                    items.Add(item);
                }
            }

            foreach (var item in ExtractManager.DepartmentArticles)
            {
                if (item.DateTime != "" && item.Title.Contains(search))
                {
                    items.Add(item);
                }
            }

            await bot.SendMessage(user, idbModelToString(items, items.Count).ToString());
        }

        static async Task commandInternalFilterList<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            var builder = new StringBuilder();
            builder.Append($"[인하대 공식 홈페이지 필터링]\r\n");
            ExtractManager.InhaUnivFilters.ToList().ForEach(x => builder.Append($"{x}\r\n"));
            builder.Append($"\r\n");
            builder.Append($"[학과 홈페이지 필터링]\r\n");
            builder.Append($"추가 예정입니다!\r\n");
            await bot.SendMessage(user, builder.ToString());
        }

        static async Task commandInternalHelp<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb, HashSet<string> filter) where T : BotModel
        {
            var builder = new StringBuilder();
            builder.Append($"인하대 알림봇 - {Version.Text}\r\n");
            builder.Append("\r\n");
            builder.Append("/start => 알림봇을 다시 설정합니다.\r\n");
            builder.Append("/my <개수> => 가장 최근의 학과 알림들을 지정한 개수만큼 가져옵니다. (기본값 5)\r\n");
            builder.Append("/recent <개수> => 가장 최근의 알림들을 지정한 개수만큼 가져옵니다. (기본값 5)\r\n");
            builder.Append("/today => 최근에 올라온 알림을 학과에 상관없이 모두 가져옵니다.\r\n");
            builder.Append("/dt <날짜> => 특정 날짜에 올라온 알림을 학과에 상관없이 모두 가져옵니다. (ex: 2022.12.20)\r\n");
            builder.Append("/search <검색어> => 특정 검색어가 들어간 알림들을 모두 찾습니다.\r\n");
            builder.Append("/filterlist => 필터링 가능한 모든 목록을 가져옵니다.\r\n");
            builder.Append("/myfilter => 내 필터링 정보를 가져옵니다.\r\n");
            builder.Append("/rap => 관리자 권한을 요청합니다.\r\n");
            builder.Append("/gg <메시지> => 채널 관리자에게 메시지를 남깁니다.(고장 문의 등)\r\n");
            if (filter.Contains("ADMIN"))
            {
                builder.Append("/msg <Type> <Id> <메시지> => 특정 사용자에게 메시지를 보냅니다.\r\n");
                builder.Append("/notice <메시지> => 모든 사용자에게 메시지를 보냅니다.\r\n");
            }
            builder.Append("/info => 봇 정보를 가져옵니다.\r\n");
            await bot.SendMessage(user, builder.ToString());
        }

        static async Task commandInternalRap<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
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
                userdb.Filtering += ",ADMIN";
                BotManager.Instance.UserDB.Update(userdb);
                await bot.SendMessage(user, $"새로운 신원 '{user.ToString()}'에 관리자 권한을 추가했습니다.");
            }
            else
            {
                await bot.SendMessage(user, "액세스 식별자가 비어있거나 입력값과 다릅니다.");
            }
        }

        static async Task commandInternalGg<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            var builder = new StringBuilder();
            builder.Append($"사용자 메시지\r\n");
            builder.Append($"식별자: {userdb.ChatBotName} - {userdb.ChatBotId}\r\n");
            builder.Append($"메시지: {msg.Replace(msg.Trim().Split(' ')[0], "").Trim()}");
            await BotManager.Instance.Notice(builder.ToString(), "ADMIN-GG");
        }

        static async Task commandInternalMsg<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb, HashSet<string> filter) where T : BotModel
        {
            if (!filter.Contains("ADMIN"))
            {
                await bot.SendMessage(user, "이 명령을 이용할 권한이 없습니다.");
                return;
            }

            var cc = msg.Trim().Split(' ');

            var builder = new StringBuilder();
            builder.Append($"[알림봇 관리자 메시지]\r\n");
            builder.Append(string.Join(' ', cc.Skip(3)));
            if (await BotManager.Instance.Send(builder.ToString(), cc[1], cc[2]))
                await bot.SendMessage(user, "성공");
            else
                await bot.SendMessage(user, "실패");
        }

        static async Task commandInternalNotice<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb, HashSet<string> filter) where T : BotModel
        {
            if (!filter.Contains("ADMIN"))
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

        static async Task commandInternalInfo<T>(T bot, string msg, BotUserIdentifier user, UserDBModel userdb) where T : BotModel
        {
            var builder = new StringBuilder();
            builder.Append($"인하대 알림봇 - {Version.Text}\r\n");
            builder.Append("\r\n");
            builder.Append($"바이너리: {Version.Name} {Version.Text}\r\n");
            builder.Append($"빌드시간: {Internals.GetBuildDate().ToLongDateString()}\r\n");
            builder.Append($"실행시간: {Program.StartTime.ToLongTimeString()}\r\n");
            builder.Append($"루프횟수: {Loop.Count}회 (10분마다 업데이트됨)\r\n");
            //builder.Append($"채널관리자: rollrat.cse@gmail.com\r\n");
            builder.Append($"소드코드: https://github.com/rollrat/inha-alarmbot\r\n");
            builder.Append("\r\n");
            //builder.Append("디스코드 알림봇: http://inhaalarmbot.kro.kr\r\n");
            builder.Append("텔레그램 알림봇: https://t.me/inhaalarmbot\r\n");
            await bot.SendMessage(user, builder.ToString());
        }

        static async Task Senario<T>(T bot, BotUserIdentifier user, string msg, UserDBModel userdb, Dictionary<string, string> option, HashSet<string> filter) where T : BotModel
        {
            switch (option["Senario"])
            {
                case "start01":
                    if (msg == "1")
                    {
                        var builder = new StringBuilder();
                        builder.Append("인하대 알림봇에 정상적으로 등록되었습니다!\r\n");
                        builder.Append("'/help'를 통해 사용가능한 명령어를 확인해보세요!\r\n");
                        option["IsSenario"] = "0";
                        userdb.Option = JsonConvert.SerializeObject(option);
                        BotManager.Instance.UserDB.Update(userdb);
                        await bot.SendMessage(user, builder.ToString());
                    }
                    else if (msg == "2")
                    {
                        var builder = new StringBuilder();
                        builder.Append("알림을 받고자하는 모든 학과를 쉼표로 구분하여 입력해주세요!\r\n");
                        builder.Append("(ex: 컴공,의디,경제학과)\r\n");
                        option["Senario"] = "start02";
                        userdb.Option = JsonConvert.SerializeObject(option);
                        BotManager.Instance.UserDB.Update(userdb);
                        await bot.SendMessage(user, builder.ToString());
                    }
                    else
                    {
                        await bot.SendMessage(user, "옳바른 옵션이 아닙니다! 다시입력해주세요!");
                    }
                    break;

                case "start02":
                    {
                        var items = msg.Split(",").Select(x => x.Trim()).Where(x => x != "");

                        // First, Check complete matches
                        var success = new List<string>();
                        var fail = new List<string>();

                        foreach (var item in items)
                        {
                            if (DepartmentList.OtherReference.ContainsKey(item))
                                success.Add(DepartmentList.OtherReference[item]);
                            else
                                fail.Add(item);
                        }

                        // Second, Check similar matches
                        foreach (var item in fail)
                        {
                            var candidate = new List<(string, int)>();
                            var kor2eng = string.Join("", item.Select(x => DepartmentList.hangul_disassembly(x)));
                            foreach (var xx in DepartmentList.KorEngReference)
                            {
                                candidate.Add((xx.Item1, Strings.ComputeLevenshteinDistance(kor2eng, xx.Item2)));
                            }
                            candidate.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                            success.Add(candidate[0].Item1);
                        }

                        var builder = new StringBuilder();
                        builder.Append("선택한 학과가 맞는지 확인해주세요!\r\n");
                        builder.Append("\r\n");
                        builder.Append("[입력받은 학과]\r\n");
                        success.ForEach(dept => builder.Append($"{DepartmentList.InverseReference[dept]} - {DepartmentList.ClassReference[dept]} ({dept.ToUpper()})\r\n"));
                        userdb.Filtering = string.Join(",", success);
                        builder.Append("\r\n");
                        builder.Append("선택한 학과가 맞다면 '1', 다시 입력하려면 '2', 모든 학과리스트를 보려면 '3'을 입력해주세요!\r\n");
                        option["Senario"] = "start03";
                        userdb.Option = JsonConvert.SerializeObject(option);
                        BotManager.Instance.UserDB.Update(userdb);
                        await bot.SendMessage(user, builder.ToString());
                    }
                    break;

                case "start03":

                    if (msg == "1")
                    {
                        var builder = new StringBuilder();
                        builder.Append("인하대 알림봇에 정상적으로 등록되었습니다!\r\n");
                        builder.Append("'/help'를 통해 사용가능한 명령어를 확인해보세요!\r\n");
                        option["IsSenario"] = "0";
                        userdb.Option = JsonConvert.SerializeObject(option);
                        BotManager.Instance.UserDB.Update(userdb);
                        await bot.SendMessage(user, builder.ToString());
                    }
                    else if (msg == "2")
                    {
                        var builder = new StringBuilder();
                        builder.Append("알림을 받고자하는 모든 학과를 쉼표로 구분하여 입력해주세요!\r\n");
                        builder.Append("(ex: 컴공,의디,경제학과)\r\n");
                        option["Senario"] = "start02";
                        userdb.Option = JsonConvert.SerializeObject(option);
                        BotManager.Instance.UserDB.Update(userdb);
                        await bot.SendMessage(user, builder.ToString());
                    }
                    else if (msg == "3")
                    {
                        var builder = new StringBuilder();
                        builder.Append("[모든 학과 리스트]\r\n");
                        DepartmentList.ClassDepartmentReference.ForEach(x =>
                        {
                            builder.Append($"[{x.Key}]\r\n");
                            x.Value.ForEach(y => builder.Append($"{y.Item2} ({y.Item1.ToUpper()})\r\n"));
                            builder.Append($"\r\n");
                        });
                        builder.Append("누락된 학과나 이름이 바뀐 학과가 있다면 채널관리자에게 문의해주세요!\r\n");
                        await bot.SendMessage(user, builder.ToString());
                    }
                    else
                    {
                        await bot.SendMessage(user, "옳바른 입력이 아닙니다! 다시입력해주세요!");
                    }
                    break;

            }
        }
    }
}
