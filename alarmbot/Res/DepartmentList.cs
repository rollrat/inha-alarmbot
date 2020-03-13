﻿// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using System;
using System.Collections.Generic;
using System.Text;

namespace alarmbot.Res
{
    public class DepartmentList
    {
        static (string, string, string[])[] Lists;
        static DepartmentList()
        {
            Lists = new (string, string, string[])[] {
                // 경영대학
                ("biz", "경영대학", new[] {"경영학과", "경영"}),
                ("gfiba", "경영대학", new[] {"글로벌금융학과", "글금"}),
                ("apsl", "경영대학", new[] {"아태물류학부", "아태", "아태물류"}),
                ("star", "경영대학", new[] {"국제통상학과", "국통"}),

                // 사범대학
                ("koreanedu", "사범대학", new[] { "국어교육과", "국어교육", "국어"}),
                ("dele", "사범대학", new[] { "영어교육과", "영어교육", "영어"}),
                ("socialedu", "사범대학", new[] { "사회교육과", "사회교육", "사회"}),
                ("education", "사범대학", new[] { "교육학과", "교육" }),
                ("physicaledu", "사범대학", new[] { "체육교육과", "체육", "체교" }),
                ("mathed", "사범대학", new[] { "수학교육과", "수교" }),

                // 사회과학대학
                ("econ", "사회과학대학", new[] { "경제학과", "경제" }),
                ("publicad", "사회과학대학", new[] { "행정학과", "행정" }),
                ("political", "사회과학대학", new[] { "정치외교학과", "정외" }),
                ("comm", "사회과학대학", new[] { "언론정보학과", "언정", "언론" }),
                ("consumer", "사회과학대학", new[] { "소비자학과", "소비", "소비자" }),
                ("child", "사회과학대학", new[] { "아동심리학과", "아동심리" }),
                ("welfare", "사회과학대학", new[] { "사회복지학과", "사회복지" }),

                // 문과대학
                ("korean", "문과대학", new[] { "한국어문학과", "한국어"}),
                ("history", "문과대학", new[] { "사학과", "사학"}),
                ("philosophy", "문과대학", new[] { "철학과", "철학"}),
                ("chinese", "문과대학", new[] { "중국학과", "중국"}),
                ("japan", "문과대학", new[] { "일본언어문화학과", "일본언어", "일본어" }),
                ("english", "문과대학", new[] { "영어영문학과", "영어영문", "영어"}),
                ("france", "문과대학", new[] { "프랑스언어문화학과", "프랑스"}),
                ("culturecm", "문과대학", new[] { "문화콘텐츠문화경영학과", "문화콘텐츠", "문콘" }),

                // 예술체육학부
                ("artsports", "예술체육학부", new[] { "조형예술학과", "조형예술", "조형", "조예" }),
                ("artsports", "예술체육학부", new[] { "디자인융합학과", "디자인융합", "디자인", "디융" }),
                ("artsports", "예술체육학부", new[] { "스포츠과학과", "스포츠과학", "스포츠" }),
                ("artsports", "예술체육학부", new[] { "연극영화학과", "연극영화", "연극", "연영" }),
                ("artsports", "예술체육학부", new[] { "의류디자인학과", "의류디자인", "의류", "의디" }),

                // 공과대학
                ("MECH", "공과대학", new[] { "기계공학과", "기계"}),
                ("aerospace", "공과대학", new[] { "항공우주공학과", "항공우주", "항우공", "항공"}),
                ("naoe", "공과대학", new[] { "조선해양공학과", "조선해양", "조선"}),
                ("ie", "공과대학", new[] { "산업경영공학과", "산업경영", "산경", "산업"}),
                ("chemeng", "공과대학", new[] { "화학공학과", "화공"}),
                ("bio", "공과대학", new[] { "생명공학과", "생공"}),
                ("inhapoly", "공과대학", new[] { "고분자공학과", "고분자"}),
                ("dmse", "공과대학", new[] { "신소재공학과", "신소재"}),
                ("civil", "공과대학", new[] { "사회인프라공학과", "사회인프라", "사인프"}),
                ("environment", "공과대학", new[] { "환경공학과", "환경"}),
                ("geoinfo", "공과대학", new[] { "공간정보공학과", "공간정보", "공간"}),
                ("arch", "공과대학", new[] { "건축학과", "건축"}),
                ("arch", "공과대학", new[] { "건축공학과", "건공"}),
                ("eneres", "공과대학", new[] {"에너지자원공학과", "에너지자원", "에너지"}),
                ("ee", "공과대학", new[] { "전자공학과", "전자"}),
                ("CSE", "공과대학", new[] { "컴퓨터공학과", "컴공"}),
                ("electrical", "공과대학", new[] { "전기공학과", "전기"}),
                ("ice", "공과대학", new[] { "정보통신공학과", "정보통신", "정통"}),
                ("", "공과대학", new[] { "융합기술경영학부", "융합기술경영"}),
                
                // 자연과학대학
                ("math", "자연과학대학", new[] { "수학과", "수학"}),
                ("statistics", "자연과학대학", new[] { "통계학과", "통계"}),
                ("physics", "자연과학대학", new[] { "물리학과", "물리"}),
                ("chemistry", "자연과학대학", new[] { "화학과", "화학"}),
                ("biology", "자연과학대학", new[] { "생명과학과", "생명"}),
                ("ocean", "자연과학대학", new[] { "해양과학과", "해양"}),
                ("foodnutri", "자연과학대학", new[] { "식품영양학과", "식품영양", "식품"}),

                // 의과대학
                ("medicine", "의과대학", new[] { "의예과", "의예", "의학"}),
                ("nursing", "의과대학", new[] { "간호학과", "간호"}),

                // 미래융합대학
                ("fccollege", "미래융합대학", new[] { "메카트로닉스학과", "메카트로닉스"}),
                ("fccollege", "미래융합대학", new[] { "소프트웨어융합공학과", "소프트웨어", "소공"}),
                ("fccollege", "미래융합대학", new[] { "산업경영학과", "산업", "산경"}),
                ("fccollege", "미래융합대학", new[] { "금융세무재테크학과", "금융"}),
            };
        }
    }
}