// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using alarmbot.Extractor;
using alarmbot.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace alarmbot.Test
{
    [TestClass]
    public class ParsingTest
    {
        [TestInitialize]
        public void Init()
        {
            Program.Scheduler = new NetScheduler();
        }

        public void Test1(string url)
        {
            var html = NetTools.DownloadString(url);
            var c1 = DepartmentExtractor.ExtractStyle1(html, "");

            Assert.IsTrue(c1.Count > 0, $"{c1.Count}");
        }

        public void Test2(string url)
        {
            var html = NetTools.DownloadString(url);
            var c1 = DepartmentExtractor.ExtractStyle2(html, "");

            Assert.IsTrue(c1.Count > 0, $"{c1.Count}");
        }

        public void Test3(string url)
        {
            var html = NetTools.DownloadString(url);
            var c1 = DepartmentExtractor.ExtractStyle3(html, "");

            Assert.IsTrue(c1.Count > 0, $"{c1.Count}");
        }

        public void Test4(string url)
        {
            var html = NetTools.DownloadString(url);
            var c1 = DepartmentExtractor.ExtractStyle4(html, "");

            Assert.IsTrue(c1.Count > 0, $"{c1.Count}");
        }

        public void Test5(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var task = NetTask.MakeDefault(url);
            task.Encoding = Encoding.GetEncoding(51949);
            var html = NetTools.DownloadString(task);
            var c1 = DepartmentExtractor.ExtractStyle5(html, "");

            Assert.IsTrue(c1.Count > 0, $"{c1.Count}");
        }

        public void Test6(string url)
        {
            var html = NetTools.DownloadString(url);
            var c1 = DepartmentExtractor.ExtractStyle6(html, "");

            Assert.IsTrue(c1.Count > 0, $"{c1.Count}");
        }

        [TestMethod] public void bizParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=7105&siteId=bisin");
        [TestMethod] public void gfibaParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3463&siteId=gfiba");
        [TestMethod] public void apslParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=4167&siteId=logistics");
        [TestMethod] public void starParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=673&siteId=star");

        [TestMethod] public void koreaneduParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6105&siteId=koreanedu");
        [TestMethod] public void deleParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6167&siteId=dele");
        [TestMethod] public void socialeduParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3697&siteId=social");
        [TestMethod] public void educationParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6271&siteId=education");
        [TestMethod] public void physicaleduParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2924&siteId=physicaledu");
        [TestMethod] public void mathedParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6136&siteId=mathed");
        
        [TestMethod] public void econParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=275&siteId=tecon");
        [TestMethod] public void publicadParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3338&siteId=publicad");
        [TestMethod] public void politicalParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3304&siteId=political");
        [TestMethod] public void commParsingTest() => Test2("");
        [TestMethod] public void consumerParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5177&siteId=consumer");
        [TestMethod] public void childParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6958&siteId=child");
        [TestMethod] public void welfareParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2081&siteId=welfare");

        [TestMethod] public void kroeanParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1043&siteId=korean");
        [TestMethod] public void historyParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5314&siteId=history");
        [TestMethod] public void philosophyParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5070&siteId=philosophy");
        [TestMethod] public void chineseParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2476&siteId=china");
        [TestMethod] public void japanParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=7858&siteId=japan");
        [TestMethod] public void englishParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=4658&siteId=english");
        [TestMethod] public void franceParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3129&siteId=france");
        [TestMethod] public void culturecmParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=4951&siteId=culturecm");

        [TestMethod] public void artsportsParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=7797&siteId=artsports");

        [TestMethod] public void mechParsingTest() => Test6("https://mech.inha.ac.kr/mech/1134/subview.do");
        [TestMethod] public void aerospaceParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=7043&siteId=aerospace");
        [TestMethod] public void naoeParsingTest() => Test6("https://naoe.inha.ac.kr/naoe/1431/subview.do");
        [TestMethod] public void ieParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1089&siteId=ie");
        [TestMethod] public void chemengParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5950&siteId=chemeng");
        [TestMethod] public void bioParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2185&siteId=bio");
        [TestMethod] public void inhapolyParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5111&siteId=inhapoly");
        [TestMethod] public void dmseParsingTest() => Test4("http://inhasmse.cafe24.com/bbs/board.php?bo_table=notice&page=1");
        [TestMethod] public void civilParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5842&siteId=civil");
        [TestMethod] public void environmentParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1229&siteId=iuee");
        [TestMethod] public void geoinfoParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1517&siteId=geoinfo");
        [TestMethod] public void archParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6357&siteId=arch");
        [TestMethod] public void eneresParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=4991&siteId=eneres");
        [TestMethod] public void eeParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1125&siteId=einha");
        [TestMethod] public void cseParsingTest() => Test6("https://cse.inha.ac.kr/cse/888/subview.do");
        [TestMethod] public void electricalParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=1009&siteId=electrical");
        [TestMethod] public void iceParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=6712&siteId=ice");
        
        [TestMethod] public void mathParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2362&siteId=math");
        [TestMethod] public void statisticsParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2410&siteId=statistics");
        [TestMethod] public void physicsParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3555&siteId=physics");
        [TestMethod] public void chemistryParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=2964&siteId=chemistry");
        [TestMethod] public void biologyParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=658&siteId=biology");
        [TestMethod] public void oceanParsingTest() => Test5("http://www.wdn.co.kr/html/info02.php");
        [TestMethod] public void foodnutriParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5374&siteId=foodnutri");

        [TestMethod] public void medicineParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=5682&siteId=medicine");
        [TestMethod] public void nursingParsingTest() => Test1("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=3845&siteId=nursing");

        [TestMethod] public void fccollegeParsingTest() => Test2("https://dept.inha.ac.kr/user/indexSub.do?codyMenuSeq=765&siteId=cfc");
    }
}
