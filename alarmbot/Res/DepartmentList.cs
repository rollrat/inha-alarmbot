// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace alarmbot.Res
{
    public class DepartmentList
    {
        public static (string, string, string, string, string[])[] Lists;
        public static Dictionary<string, string> ClassReference;
        public static Dictionary<string, string> InverseReference;
        public static Dictionary<string, string> OtherReference;
        public static List<KeyValuePair<string, List<(string, string)>>> ClassDepartmentReference; 
        public static List<(string, string)> KorEngReference;
        static DepartmentList()
        {
            // (shorts, parsing style, board address, department, other names)
            Lists = new (string, string, string, string, string[])[] {
                // 경영대학
                ("biz", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=7105&siteId=bisin", "경영대학", new[] {"경영학과", "경영"}),
                ("gfiba", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3463&siteId=gfiba", "경영대학", new[] {"글로벌금융학과", "글금"}),
                ("apsl", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=4167&siteId=logistics", "경영대학", new[] {"아태물류학부", "아태", "아태물류"}),
                ("star", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=673&siteId=star", "경영대학", new[] {"국제통상학과", "국통"}),

                // 사범대학
                ("koreanedu", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6105&siteId=koreanedu", "사범대학", new[] { "국어교육과", "국어교육", "국어", "국교" }),
                ("dele", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6167&siteId=dele", "사범대학", new[] { "영어교육과", "영어교육", "영교" }),
                ("socialedu", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3697&siteId=social", "사범대학", new[] { "사회교육과", "사회교육", "사회", "사교" }),
                ("education", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6271&siteId=education", "사범대학", new[] { "교육학과", "교육" }),
                ("physicaledu", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2924&siteId=physicaledu", "사범대학", new[] { "체육교육과", "체육", "체교" }),
                ("mathed", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6136&siteId=mathed", "사범대학", new[] { "수학교육과", "수교" }),

                // 사회과학대학
                ("econ", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=275&siteId=tecon", "사회과학대학", new[] { "경제학과", "경제" }),
                ("publicad", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3338&siteId=publicad", "사회과학대학", new[] { "행정학과", "행정" }),
                ("political", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3304&siteId=political", "사회과학대학", new[] { "정치외교학과", "정외" }),
                ("comm", "", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5449&siteId=comm", "사회과학대학", new[] { "미디어커뮤니케이션", "미디어", "미커" }),
                ("consumer", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5177&siteId=consumer", "사회과학대학", new[] { "소비자학과", "소비", "소비자" }),
                ("child", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6958&siteId=child", "사회과학대학", new[] { "아동심리학과", "아동심리" }),
                ("welfare", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2081&siteId=welfare", "사회과학대학", new[] { "사회복지학과", "사회복지" }),

                // 문과대학
                ("korean", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1043&siteId=korean", "문과대학", new[] { "한국어문학과", "한국어"}),
                ("history", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5314&siteId=history", "문과대학", new[] { "사학과", "사학"}),
                ("philosophy", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5070&siteId=philosophy", "문과대학", new[] { "철학과", "철학"}),
                ("chinese", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2476&siteId=china", "문과대학", new[] { "중국학과", "중국"}),
                ("japan", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=7858&siteId=japan", "문과대학", new[] { "일본언어문화학과", "일본언어", "일본어" }),
                ("english", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=4658&siteId=english", "문과대학", new[] { "영어영문학과", "영어영문", "영어"}),
                ("france", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3129&siteId=france", "문과대학", new[] { "프랑스언어문화학과", "프랑스"}),
                ("culturecm", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=4951&siteId=culturecm", "문과대학", new[] { "문화콘텐츠문화경영학과", "문화콘텐츠", "문콘" }),

                // 예술체육학부
                ("artsports", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=7797&siteId=artsports", "예술체육학부", new[] { "조형예술학과", "조형예술", "조형", "조예" }),
                ("artsports", "", "", "예술체육학부", new[] { "디자인융합학과", "디자인융합", "디자인", "디융" }),
                ("artsports", "", "", "예술체육학부", new[] { "스포츠과학과", "스포츠과학", "스포츠" }),
                ("artsports", "", "", "예술체육학부", new[] { "연극영화학과", "연극영화", "연극", "연영" }),
                ("artsports", "", "", "예술체육학부", new[] { "의류디자인학과", "의류디자인", "의류", "의디" }),

                // 공과대학
                ("mech", "s6", "https://mech.inha.ac.kr/mech/1134/subview.do", "공과대학", new[] { "기계공학과", "기계"}),
                ("aerospace", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=7043&siteId=aerospace", "공과대학", new[] { "항공우주공학과", "항공우주", "항우공", "항공"}),
                ("naoe", "s6", "https://naoe.inha.ac.kr/naoe/1431/subview.do", "공과대학", new[] { "조선해양공학과", "조선해양", "조선"}),
                ("ie", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1089&siteId=ie", "공과대학", new[] { "산업경영공학과", "산업경영공학", "산공" }),
                ("chemeng", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5950&siteId=chemeng", "공과대학", new[] { "화학공학과", "화공"}),
                ("bio","s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2185&siteId=bio", "공과대학", new[] { "생명공학과", "생공"}),
                ("inhapoly", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5111&siteId=inhapoly", "공과대학", new[] { "고분자공학과", "고분자"}),
                ("dmse", "s4", "http://inhasmse.cafe24.com/bbs/board.php?bo_table=notice&page=1", "공과대학", new[] { "신소재공학과", "신소재"}),
                ("civil", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5842&siteId=civil", "공과대학", new[] { "사회인프라공학과", "사회인프라", "사인프"}),
                ("environment", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1229&siteId=iuee", "공과대학", new[] { "환경공학과", "환경"}),
                ("geoinfo", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1517&siteId=geoinfo", "공과대학", new[] { "공간정보공학과", "공간정보", "공간"}),
                ("arch", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6357&siteId=arch", "공과대학", new[] { "건축학과", "건축"}),
                ("arch", "", "", "공과대학", new[] { "건축공학과", "건공"}),
                ("eneres", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=4991&siteId=eneres", "공과대학", new[] {"에너지자원공학과", "에너지자원", "에너지"}),
                ("ee", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1125&siteId=einha", "공과대학", new[] { "전자공학과", "전자"}),
                ("cse", "s6", "https://cse.inha.ac.kr/cse/888/subview.do", "공과대학", new[] { "컴퓨터공학과", "컴공"}),
                ("electrical", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1009&siteId=electrical", "공과대학", new[] { "전기공학과", "전기"}),
                ("ice", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6712&siteId=ice", "공과대학", new[] { "정보통신공학과", "정보통신", "정통"}),
                ("", "", "", "공과대학", new[] { "융합기술경영학부", "융합기술경영"}),
                
                // 자연과학대학
                ("math", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2362&siteId=math", "자연과학대학", new[] { "수학과", "수학"}),
                ("statistics", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2410&siteId=statistics", "자연과학대학", new[] { "통계학과", "통계"}),
                ("physics", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3555&siteId=physics", "자연과학대학", new[] { "물리학과", "물리"}),
                ("chemistry", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2964&siteId=chemistry", "자연과학대학", new[] { "화학과", "화학"}),
                ("biology", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=658&siteId=biology", "자연과학대학", new[] { "생명과학과", "생명"}),
                //("ocean", "s5", "http://www.wdn.co.kr/html/info02.php", "자연과학대학", new[] { "해양과학과", "해양"}),
                ("foodnutri", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5374&siteId=foodnutri", "자연과학대학", new[] { "식품영양학과", "식품영양", "식품"}),

                // 의과대학
                ("medicine", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5682&siteId=medicine", "의과대학", new[] { "의예과", "의예", "의학", "의대" }),
                ("nursing", "s1", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3845&siteId=nursing", "의과대학", new[] { "간호학과", "간호" }),

                // 미래융합대학
                ("fccollege", "s2", "https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=765&siteId=cfc", "미래융합대학", new[] { "메카트로닉스학과", "메카트로닉스"}),
                ("fccollege", "", "", "미래융합대학", new[] { "소프트웨어융합공학과", "소프트웨어", "소공"}),
                ("fccollege", "", "", "미래융합대학", new[] { "산업경영학과", "산업", "산경"}),
                ("fccollege", "", "", "미래융합대학", new[] { "금융세무재테크학과", "금융"}),
            };

            ClassReference = new Dictionary<string, string>();
            InverseReference = new Dictionary<string, string>();
            OtherReference = new Dictionary<string, string>();
            KorEngReference = new List<(string, string)>();
            var cdr = new Dictionary<string, List<(string, string)>>();

            foreach (var department in Lists)
            {
                if (!InverseReference.ContainsKey(department.Item1))
                {
                    InverseReference.Add(department.Item1, department.Item5[0]);
                    ClassReference.Add(department.Item1, department.Item4);
                }
                else
                {
                    InverseReference[department.Item1] += "," + department.Item5[0];
                }
                if (!cdr.ContainsKey(department.Item4))
                {
                    cdr.Add(department.Item4, new List<(string, string)>());
                }
                cdr[department.Item4].Add((department.Item1, department.Item5[0]));
                foreach (var dept_other in department.Item5)
                {
                    OtherReference.Add(dept_other, department.Item1);
                    KorEngReference.Add((department.Item1, string.Join("", dept_other.Select(x => hangul_disassembly(x)))));
                }
            }

            ClassDepartmentReference = cdr.ToList();
        }

        static readonly string[] cc = new[] { "r", "R", "rt", "s", "sw", "sg", "e", "E", "f", "fr", "fa", "fq", "ft", "fe",
            "fv", "fg", "a", "q", "Q", "qt", "t", "T", "d", "w", "W", "c", "z", "e", "v", "g", "k", "o", "i", "O",
            "j", "p", "u", "P", "h", "hk", "ho", "hl", "y", "n", "nj", "np", "nl", "b", "m", "ml", "l", " ", "ss",
            "se", "st", " ", "frt", "fe", "fqt", " ", "fg", "aq", "at", " ", " ", "qr", "qe", "qtr", "qte", "qw",
            "qe", " ", " ", "tr", "ts", "te", "tq", "tw", " ", "dd", "d", "dt", " ", " ", "gg", " ", "yi", "yO", "yl", "bu", "bP", "bl" };
        static readonly char[] cc1 = new[] { 'r', 'R', 's', 'e', 'E', 'f', 'a', 'q', 'Q', 't', 'T', 'd', 'w', 'W', 'c', 'z', 'x', 'v', 'g' };
        static readonly string[] cc2 = new[] { "k", "o", "i", "O", "j", "p", "u", "P", "h", "hk", "ho", "hl", "y", "n", "nj", "np", "nl", "b", "m", "ml", "l" };
        static readonly string[] cc3 = new[] { "", "r", "R", "rt", "s", "sw", "sg", "e", "f", "fr", "fa", "fq", "ft", "fx", "fv", "fg", "a", "q", "qt", "t", "T", "d", "w", "c", "z", "x", "v", "g" };

        public static string hangul_disassembly(char letter)
        {
            if (0xAC00 <= letter && letter <= 0xD7FB)
            {
                int unis = letter - 0xAC00;
                return cc1[unis / (21 * 28)] + cc2[(unis % (21 * 28)) / 28] + cc3[(unis % (21 * 28)) % 28];
            }
            else if (0x3131 <= letter && letter <= 0x3163)
            {
                int unis = letter;
                return cc[unis - 0x3131];
            }
            else
            {
                return letter.ToString();
            }
        }
    }
}
