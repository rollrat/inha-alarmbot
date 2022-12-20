// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

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

        public void Test7(string url)
        {
            var html = NetTools.DownloadString(url);
            var c1 = DepartmentExtractor.ExtractStyle7(html, "");

            Assert.IsTrue(c1.Count > 0, $"{c1.Count}");
        }
        [TestMethod] public void bizParsingTest() => Test6("https://biz.inha.ac.kr/biz/4410/subview.do");
        [TestMethod] public void gfibaParsingTest() => Test6("https://gfiba.inha.ac.kr/gfiba/4870/subview.do");
        [TestMethod] public void apslParsingTest() => Test6("https://apsl.inha.ac.kr/logistics/4465/subview.do");
        [TestMethod] public void starParsingTest() => Test7("https://star.inha.ac.kr/star/4282/subview.do");
        [TestMethod] public void koreaneduParsingTest() => Test7("https://koreanedu.inha.ac.kr/koreanedu/4144/subview.do");
        [TestMethod] public void deleParsingTest() => Test6("https://dele.inha.ac.kr/dele/4839/subview.do");
        [TestMethod] public void socialeduParsingTest() => Test6("https://socialedu.inha.ac.kr/socialedu/4099/subview.do");
        [TestMethod] public void educationParsingTest() => Test6("https://education.inha.ac.kr/education/4233/subview.do");
        [TestMethod] public void physicaleduParsingTest() => Test6("https://physicaledu.inha.ac.kr/physicaledu/4653/subview.do");
        [TestMethod] public void mathedParsingTest() => Test6("https://mathed.inha.ac.kr/mathed/4187/subview.do");
        [TestMethod] public void econParsingTest() => Test6("https://econ.inha.ac.kr/econ/5083/subview.do");
        [TestMethod] public void publicadParsingTest() => Test6("https://publicad.inha.ac.kr/publicad/7688/subview.do");
        [TestMethod] public void politicalParsingTest() => Test6("https://political.inha.ac.kr/political/7753/subview.do");
        [TestMethod] public void consumerParsingTest() => Test6("https://consumer.inha.ac.kr/consumer/7213/subview.do");
        [TestMethod] public void childParsingTest() => Test6("https://child.inha.ac.kr/child/7467/subview.do");
        [TestMethod] public void welfareParsingTest() => Test6("https://welfare.inha.ac.kr/welfare/7786/subview.do");
        [TestMethod] public void koreanParsingTest() => Test6("https://korean.inha.ac.kr/korean/6786/subview.do");
        [TestMethod] public void historyParsingTest() => Test6("https://history.inha.ac.kr/history/8215/subview.do");
        [TestMethod] public void philosophyParsingTest() => Test6("https://philosophy.inha.ac.kr/philosophy/7853/subview.do");
        [TestMethod] public void chineseParsingTest() => Test6("https://chinese.inha.ac.kr/chinese/6568/subview.do");
        [TestMethod] public void japanParsingTest() => Test6("https://japan.inha.ac.kr/japan/8179/subview.do");
        [TestMethod] public void englishParsingTest() => Test6("https://english.inha.ac.kr/english/5659/subview.do");
        [TestMethod] public void franceParsingTest() => Test6("https://france.inha.ac.kr/france/10063/subview.do");
        [TestMethod] public void culturecmParsingTest() => Test6("https://culturecm.inha.ac.kr/culturecm/7880/subview.do");
        [TestMethod] public void artsportsParsingTest() => Test6("https://artsports.inha.ac.kr/artsports/9518/subview.do");
        [TestMethod] public void sportParsingTest() => Test6("https://sport.inha.ac.kr/sport/9067/subview.do");
        [TestMethod] public void theatrefilmParsingTest() => Test6("https://theatrefilm.inha.ac.kr/theatrefilm/9566/subview.do");
        [TestMethod] public void fashionParsingTest() => Test6("https://fashion.inha.ac.kr/fashion/4982/subview.do");
        [TestMethod] public void mechParsingTest() => Test6("https://mech.inha.ac.kr/mech/1823/subview.do");
        [TestMethod] public void aerospaceParsingTest() => Test6("https://aerospace.inha.ac.kr/aerospace/9846/subview.do");
        [TestMethod] public void naoeParsingTest() => Test6("https://naoe.inha.ac.kr/naoe/1791/subview.do");
        [TestMethod] public void ieParsingTest() => Test6("https://ie.inha.ac.kr/ie/963/subview.do");
        [TestMethod] public void chemengParsingTest() => Test6("https://chemeng.inha.ac.kr/chemeng/2220/subview.do");
        [TestMethod] public void bioParsingTest() => Test6("https://bio.inha.ac.kr/bio/2346/subview.do");
        [TestMethod] public void inhapolyParsingTest() => Test6("https://inhapoly.inha.ac.kr/inhapoly/2321/subview.do");
        [TestMethod] public void dmseParsingTest() => Test6("https://dmse.inha.ac.kr/dmse/2121/subview.do");
        [TestMethod] public void civilParsingTest() => Test6("https://civil.inha.ac.kr/civil/2383/subview.do");
        [TestMethod] public void environmentParsingTest() => Test6("https://environment.inha.ac.kr/environment/2541/subview.do");
        [TestMethod] public void geoinfoParsingTest() => Test7("https://geoinfo.inha.ac.kr/geoinfo/2678/subview.do");
        [TestMethod] public void archParsingTest() => Test6("https://arch.inha.ac.kr/arch/2161/subview.do");
        [TestMethod] public void eneresParsingTest() => Test6("https://eneres.inha.ac.kr/eneres/3441/subview.do");
        [TestMethod] public void eeParsingTest() => Test6("https://ee.inha.ac.kr/ee/784/subview.do");
        [TestMethod] public void electricalParsingTest() => Test6("https://electrical.inha.ac.kr/electrical/2410/subview.do");
        [TestMethod] public void iceParsingTest() => Test6("https://ice.inha.ac.kr/ice/2269/subview.do");
        [TestMethod] public void cseParsingTest() => Test6("https://cse.inha.ac.kr/cse/888/subview.do");
        [TestMethod] public void doaiParsingTest() => Test6("https://doai.inha.ac.kr/doai/3046/subview.do");
        [TestMethod] public void datascienceParsingTest() => Test6("https://datascience.inha.ac.kr/datascience/3125/subview.do");
        [TestMethod] public void smeParsingTest() => Test6("https://sme.inha.ac.kr/sme/2867/subview.do");
        [TestMethod] public void designtechParsingTest() => Test6("https://designtech.inha.ac.kr/designtech/3083/subview.do");
        [TestMethod] public void mathParsingTest() => Test7("https://math.inha.ac.kr/math/3528/subview.do");
        [TestMethod] public void statisticsParsingTest() => Test6("https://statistics.inha.ac.kr/statistics/3383/subview.do");
        [TestMethod] public void physicsParsingTest() => Test6("https://physics.inha.ac.kr/physics/3908/subview.do");
        [TestMethod] public void chemistryParsingTest() => Test6("https://chemistry.inha.ac.kr/chemistry/3298/subview.do");
        [TestMethod] public void biologyParsingTest() => Test6("https://biology.inha.ac.kr/biology/3685/subview.do");
        [TestMethod] public void foodnutriParsingTest() => Test6("https://foodnutri.inha.ac.kr/foodnutri/3555/subview.do");
        [TestMethod] public void medicineParsingTest() => Test6("https://medicine.inha.ac.kr/medicine/9635/subview.do");
        [TestMethod] public void nursingParsingTest() => Test6("https://nursing.inha.ac.kr/nursing/9272/subview.do");
        [TestMethod] public void fccollegeParsingTest() => Test6("https://fccollege.inha.ac.kr/fccollege/8120/subview.do");

    }
}
