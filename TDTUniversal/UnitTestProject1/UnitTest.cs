using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TDTUniversal.API;
using TDTUniversal.API.Requests;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            AvatarRequest ar = new AvatarRequest("51403318", "51403318TDT");
            RequestBuilder rb = new RequestBuilder(ar);
            var r = await rb.BuildQuery();
            Debug.WriteLine(r);
            Assert.AreEqual(r, "https://api.trautre.cf/v2.php?act=avatar&user=51403318&pass=51403318TDT&token=");
        }
    }
}
