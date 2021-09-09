using System;
using Persistence;
using DAL;
namespace BL
{
    public class ShoesBL
    {
        private ShoesDAL dal = new ShoesDAL();
        private ShoesDAL dal1 = new ShoesDAL();
        private ShoesDAL dal2 = new ShoesDAL();
        public Shoes SearchById(Shoes shoes)
        {
            return dal.GetbyID(shoes);
        }
        public Shoes SearchByBrand(Shoes shoes){
            return dal1.GetByBrand(shoes);
        }
        public Shoes SearchByName(Shoes shoes){
            return dal2.GetByName(shoes);
        }
    }
}