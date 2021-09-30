using System;

namespace Persistence
{
    public class Staff
    {
        public int StaffId { set; get; }
        public string StaffName { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Phone { set; get; }
        public int Role { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is Staff)
            {
                return ((Staff)obj).StaffId.Equals(StaffId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return StaffId.GetHashCode();
        }
    }
}