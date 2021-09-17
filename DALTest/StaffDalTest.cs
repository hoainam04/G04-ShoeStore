using System;
using Xunit;
using DAL;
using Persistence;

namespace DALTest
{
    public class StaffDalTest
    {
        private StaffDal dal = new StaffDal();
        private Staff staff = new Staff();

        [Theory]
        [InlineData("hoainam", "hoainam04", 1)]
        [InlineData("trandat", "trandat123", 2)]
        public void LoginTest(string userName, string pass, int expected)
        {
            staff.UserName = userName;
            staff.Password = pass;
            int result = dal.Login(staff).Role;
            Assert.True(expected == result);
        }
    }
}
