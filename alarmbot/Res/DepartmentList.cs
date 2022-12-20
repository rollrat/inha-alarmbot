// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

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
                ("biz", "s6", "https://biz.inha.ac.kr/biz/4410/subview.do", "경영대학", new[] {"경영학과", "경영"}),
                ("gfiba", "s6", "https://gfiba.inha.ac.kr/gfiba/4870/subview.do", "경영대학", new[] {"글로벌금융학과", "글금"}),
                ("apsl", "s6", "https://apsl.inha.ac.kr/logistics/4465/subview.do", "경영대학", new[] {"아태물류학부", "아태", "아태물류"}),
                ("star", "s7", "https://star.inha.ac.kr/star/4282/subview.do", "경영대학", new[] {"국제통상학과", "국통"}),

                // 사범대학
                ("koreanedu", "s7", "https://koreanedu.inha.ac.kr/koreanedu/4144/subview.do", "사범대학", new[] { "국어교육과", "국어교육", "국어", "국교" }),
                ("dele", "s6", "https://dele.inha.ac.kr/dele/4839/subview.do", "사범대학", new[] { "영어교육과", "영어교육", "영교" }),
                ("socialedu", "s6", "https://socialedu.inha.ac.kr/socialedu/4099/subview.do", "사범대학", new[] { "사회교육과", "사회교육", "사회", "사교" }),
                ("education", "s6", "https://education.inha.ac.kr/education/4233/subview.do", "사범대학", new[] { "교육학과", "교육" }),
                ("physicaledu", "s6", "https://physicaledu.inha.ac.kr/physicaledu/4653/subview.do", "사범대학", new[] { "체육교육과", "체육", "체교" }),
                ("mathed", "s6", "https://mathed.inha.ac.kr/mathed/4187/subview.do", "사범대학", new[] { "수학교육과", "수교" }),

                // 사회과학대학
                ("econ", "s6", "https://econ.inha.ac.kr/econ/5083/subview.do", "사회과학대학", new[] { "경제학과", "경제" }),
                ("publicad", "s6", "https://publicad.inha.ac.kr/publicad/7688/subview.do", "사회과학대학", new[] { "행정학과", "행정" }),
                ("political", "s6", "https://political.inha.ac.kr/political/7753/subview.do", "사회과학대학", new[] { "정치외교학과", "정외" }),
                ("comm", "", "https://comm.inha.ac.kr/comm/6352/subview.do", "사회과학대학", new[] { "미디어커뮤니케이션", "미디어", "미커" }),
                ("consumer", "s6", "https://consumer.inha.ac.kr/consumer/7213/subview.do", "사회과학대학", new[] { "소비자학과", "소비", "소비자" }),
                ("child", "s6", "https://child.inha.ac.kr/child/7467/subview.do", "사회과학대학", new[] { "아동심리학과", "아동심리" }),
                ("welfare", "s6", "https://welfare.inha.ac.kr/welfare/7786/subview.do", "사회과학대학", new[] { "사회복지학과", "사회복지" }),

                // 문과대학
                ("korean", "s6", "https://korean.inha.ac.kr/korean/6786/subview.do", "문과대학", new[] { "한국어문학과", "한국어"}),
                ("history", "s6", "https://history.inha.ac.kr/history/8215/subview.do", "문과대학", new[] { "사학과", "사학"}),
                ("philosophy", "s6", "https://philosophy.inha.ac.kr/philosophy/7853/subview.do", "문과대학", new[] { "철학과", "철학"}),
                ("chinese", "s6", "https://chinese.inha.ac.kr/chinese/6568/subview.do", "문과대학", new[] { "중국학과", "중국"}),
                ("japan", "s6", "https://japan.inha.ac.kr/japan/8179/subview.do", "문과대학", new[] { "일본언어문화학과", "일본언어", "일본어" }),
                ("english", "s6", "https://english.inha.ac.kr/english/5659/subview.do", "문과대학", new[] { "영어영문학과", "영어영문", "영어"}),
                ("france", "s6", "https://france.inha.ac.kr/france/10063/subview.do", "문과대학", new[] { "프랑스언어문화학과", "프랑스"}),
                ("culturecm", "s6", "https://culturecm.inha.ac.kr/culturecm/7880/subview.do", "문과대학", new[] { "문화콘텐츠문화경영학과", "문화콘텐츠", "문콘" }),

                // 예술체육대학
                ("artsports", "s6", "https://artsports.inha.ac.kr/artsports/9518/subview.do", "예술체육대학", new[] { "조형예술학과", "조형예술", "조형", "조예" }),
                ("artsports", "", "", "예술체육대학", new[] { "디자인융합학과", "디자인융합", "디자인", "디융" }),
                ("sport", "s6", "https://sport.inha.ac.kr/sport/9067/subview.do", "예술체육대학", new[] { "스포츠과학과", "스포츠과학", "스포츠" }),
                ("theatrefilm", "s6", "https://theatrefilm.inha.ac.kr/theatrefilm/9566/subview.do", "예술체육대학", new[] { "연극영화학과", "연극영화", "연극", "연영" }),
                ("fashion", "s6", "https://fashion.inha.ac.kr/fashion/4982/subview.do", "예술체육대학", new[] { "의류디자인학과", "의류디자인", "의류", "의디" }),

                // 공과대학
                ("mech", "s6", "https://mech.inha.ac.kr/mech/1823/subview.do", "공과대학", new[] { "기계공학과", "기계"}),
                ("aerospace", "s6", "https://aerospace.inha.ac.kr/aerospace/9846/subview.do", "공과대학", new[] { "항공우주공학과", "항공우주", "항우공", "항공"}),
                ("naoe", "s6", "https://naoe.inha.ac.kr/naoe/1791/subview.do", "공과대학", new[] { "조선해양공학과", "조선해양", "조선"}),
                ("ie", "s6", "https://ie.inha.ac.kr/ie/963/subview.do", "공과대학", new[] { "산업경영공학과", "산업경영공학", "산공" }),
                ("chemeng", "s6", "https://chemeng.inha.ac.kr/chemeng/2220/subview.do", "공과대학", new[] { "화학공학과", "화공"}),
                ("bio","s6", "https://bio.inha.ac.kr/bio/2346/subview.do", "공과대학", new[] { "생명공학과", "생공"}),
                ("inhapoly", "s6", "https://inhapoly.inha.ac.kr/inhapoly/2321/subview.do", "공과대학", new[] { "고분자공학과", "고분자"}),
                ("dmse", "s6", "https://dmse.inha.ac.kr/dmse/2121/subview.do", "공과대학", new[] { "신소재공학과", "신소재"}),
                ("civil", "s6", "https://civil.inha.ac.kr/civil/2383/subview.do", "공과대학", new[] { "사회인프라공학과", "사회인프라", "사인프"}),
                ("environment", "s6", "https://environment.inha.ac.kr/environment/2541/subview.do", "공과대학", new[] { "환경공학과", "환경"}),
                ("geoinfo", "s7", "https://geoinfo.inha.ac.kr/geoinfo/2678/subview.do", "공과대학", new[] { "공간정보공학과", "공간정보", "공간"}),
                ("arch", "s6", "https://arch.inha.ac.kr/arch/2161/subview.do", "공과대학", new[] { "건축학과", "건축"}),
                ("arch", "", "", "공과대학", new[] { "건축공학과", "건공"}),
                ("eneres", "s6", "https://eneres.inha.ac.kr/eneres/3441/subview.do", "공과대학", new[] {"에너지자원공학과", "에너지자원", "에너지"}),
                ("ee", "s6", "https://ee.inha.ac.kr/ee/784/subview.do", "공과대학", new[] { "전자공학과", "전자"}),
                ("electrical", "s6", "https://electrical.inha.ac.kr/electrical/2410/subview.do", "공과대학", new[] { "전기공학과", "전기"}),
                ("ice", "s6", "https://ice.inha.ac.kr/ice/2269/subview.do", "공과대학", new[] { "정보통신공학과", "정보통신", "정통"}),
                ("", "", "", "공과대학", new[] { "융합기술경영학부", "융합기술경영"}),

                // 소프트웨어융합대학
                ("cse", "s6", "https://cse.inha.ac.kr/cse/888/subview.do", "소프트웨어융합대학", new[] { "컴퓨터공학과", "컴공"}),
                ("doai", "s6", "https://doai.inha.ac.kr/doai/3046/subview.do", "소프트웨어융합대학", new[] { "인공지능공학과", "인공지능", "인공"}),
                ("datascience", "s6", "https://datascience.inha.ac.kr/datascience/3125/subview.do", "소프트웨어융합대학", new[] { "데이터사이언스학과", "데이터사이언스", "데이터", "데사"}),
                ("sme", "s6", "https://sme.inha.ac.kr/sme/2867/subview.do", "소프트웨어융합대학", new[] { "스마트모빌리티공학과", "스마트모빌리티", "스모공"}),
                ("designtech", "s6", "https://designtech.inha.ac.kr/designtech/3083/subview.do", "소프트웨어융합대학", new[] { "디자인테크놀로지학과", "디자인테크놀로지", "디자인테크", "디테크"}),
                
                // 자연과학대학
                ("math", "s7", "https://math.inha.ac.kr/math/3528/subview.do", "자연과학대학", new[] { "수학과", "수학"}),
                ("statistics", "s6", "https://statistics.inha.ac.kr/statistics/3383/subview.do", "자연과학대학", new[] { "통계학과", "통계"}),
                ("physics", "s6", "https://physics.inha.ac.kr/physics/3908/subview.do", "자연과학대학", new[] { "물리학과", "물리"}),
                ("chemistry", "s6", "https://chemistry.inha.ac.kr/chemistry/3298/subview.do", "자연과학대학", new[] { "화학과", "화학"}),
                ("biology", "s6", "https://biology.inha.ac.kr/biology/3685/subview.do", "자연과학대학", new[] { "생명과학과", "생명"}),
                //("ocean", "s6", "http://www.wdn.co.kr/html/info02.php", "자연과학대학", new[] { "해양과학과", "해양"}),
                ("foodnutri", "s6", "https://foodnutri.inha.ac.kr/foodnutri/3555/subview.do", "자연과학대학", new[] { "식품영양학과", "식품영양", "식품"}),

                // 의과대학
                ("medicine", "s6", "https://medicine.inha.ac.kr/medicine/9635/subview.do", "의과대학", new[] { "의예과", "의예", "의학", "의대" }),
                ("nursing", "s6", "https://nursing.inha.ac.kr/nursing/9272/subview.do", "의과대학", new[] { "간호학과", "간호" }),

                // 미래융합대학
                ("fccollege", "s6", "https://fccollege.inha.ac.kr/fccollege/8120/subview.do", "미래융합대학", new[] { "메카트로닉공학과", "메카트로닉"}),
                ("fccollege", "", "", "미래융합대학", new[] { "소프트웨어융합공학과", "소프트웨어", "소공"}),
                ("fccollege", "", "", "미래융합대학", new[] { "산업경영학과", "산업", "산경"}),
                ("fccollege", "", "", "미래융합대학", new[] { "금융투자학과", "금융"}),
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
