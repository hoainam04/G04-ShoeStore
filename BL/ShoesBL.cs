using System.Collections.Generic;
using Persistence;
using DAL;
namespace BL
{
    public class ShoesBL
    {
        private ShoesDAL dal = new ShoesDAL();
        public Shoes SearchById(int shoeId)
        {
            return dal.GetbyID(shoeId);
        }
        public Shoes GetToInvocie(int sId,int cId,int szId)
        {
            return dal.GetToInvocie(sId,cId,szId);
        }
        public SizesColors Color(int shoeId)
        {
            return dal.Color(shoeId);
        }
        public SizesColors GetSizes(int shoeId, int colorId)
        {
            return dal.GetSizes(shoeId, colorId);
        }
        public List<Shoes> GetAll()
        {
            return dal.GetShoes(ShoeFilter.GET_ALL, null);
        }
        public List<Shoes> SearchByName(string shoeName)
        {
            return dal.GetShoes(ShoeFilter.FILTER_BY_SHOE_NAME, new Shoes { ShoeName = shoeName });
        }
        
        public List<Shoes> SearchByBrand(string brandName)
        {
            return dal.GetShoes(ShoeFilter.FILTER_BY_BRAND_NAME, new Shoes { BrandName = brandName });
        }
    }
}