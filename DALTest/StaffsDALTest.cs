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
        [InlineData("hoainam", "hoainam04")]
        [InlineData("trandat", "trandat123")]
        public void LoginTest(string userName, string pass)
        {
            // staff.UserName = userName;
            // staff.Password = pass;
            Staff staff = new Staff() { UserName = userName, Password = pass };
            int result = dal.Login(staff).Role;
            Assert.True(result != 0);
        }
        [Theory]
        [InlineData("Hoai Nam","hoainam", "hoainam04")]
        [InlineData("Tran Dat", "trandat","trandat123")]     
        public void LoginTest2(string staffName,string userName, string pass){
            staff.StaffName = staffName;
            staff.UserName = userName;
            staff.Password = pass;
            int result = dal.Login(staff).Role;
            Assert.True(result != 0);
        }

    }
}

