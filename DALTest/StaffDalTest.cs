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
        [InlineData("trandat", "trandat123")]
        [InlineData("hoainam", "hoainam04")]
        public void LoginTest(string userName, string pass)
        {
            staff.UserName = userName;
            staff.Password = pass;
            Staff result = dal.Login(staff);
            Assert.True(result != null);
            Assert.True(staff.UserName == userName);
            Assert.True(staff.Password == pass);
        }
        //  public void LoginTest1()
        // {
        //     staff.UserName = "hoainam";
        //     staff.Password = "hoainam04";
        //     int expected = 1;
        //     int result = dal.Login(staff).Role;
        //     Assert.True(expected == result);
        // }
        //  public void LoginTest2()
        // {
        //     staff.UserName = "trandat";
        //     staff.Password = "trandat123";
        //     int expected = 1;
        //     int result = dal.Login(staff).Role;
        //     Assert.True(expected == result);
        // }

    }
}
