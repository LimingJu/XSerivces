using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedConfig.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel;
using SharedModel.Identity;

namespace SharedConfig.Util.Tests
{
    [TestClass()]
    public class UserPriviledgeHelperTests
    {
        [TestMethod()]
        public void IfUserCanDoTest()
        {
            var context = new DefaultAppDbContext();
            var userStore = new ServiceUserStore(context);

            var debugUser = (new ServiceUserManager(userStore)).FindByNameAsync("x").Result;
            var r = UserPriviledgeHelper.IfUserCanDo("x", "ppp", "Site_Test_Eeco");
            Assert.IsTrue(r, "failed");
        }

        [TestMethod()]
        public void CreateOrUpdateUserTest()
        {
            UserPriviledgeHelper.CreateOrUpdateUser("x", "x@nodomain.com", "Pa88word!", "", new List<string>() { "RegionEastAsia" });
        }
    }
}