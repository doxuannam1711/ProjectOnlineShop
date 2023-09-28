using Common;
using DocumentFormat.OpenXml.Drawing.Charts;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public List<Product> ListByCategoryId(long categoryID,ref int totalRecord, int pageIndex = 1, int pageSize = 2 )
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model= db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x=>x.CreatedDate).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return model;
        }

        public List<Product> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            var model = db.Products.Where(x => x.Name.Contains(keyword)).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListRelatedProduct(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public long Create(Product product)
        {
            //Xử lý Alias
            if (string.IsNullOrEmpty(product.MetaTitle))
            {
                product.MetaTitle = StringHelper.ToUnsignString(product.Name);
            }
            product.CreatedDate = DateTime.Now;
            product.ViewCount = 0;
            db.Products.Add(product);
            db.SaveChanges();

            return product.ID;
        }

        public long Edit(Product product)
        {
            var productEdit = ViewDetail(product.ID);

            if (string.IsNullOrEmpty(product.MetaTitle))
            {
                product.MetaTitle = StringHelper.ToUnsignString(product.Name);
                productEdit.MetaTitle = product.MetaTitle;
            }
            productEdit.Name = product.Name;
            productEdit.Description = product.Description;
            productEdit.Image = product.Image;
            productEdit.Detail = product.Detail;
            productEdit.Warranty = product.Warranty;
            productEdit.Quantity = product.Quantity;
            productEdit.CategoryID = product.CategoryID;
            productEdit.MetaKeywords = product.MetaKeywords;
            productEdit.MetaDescriptions = product.MetaDescriptions;
            productEdit.Status = product.Status;
            productEdit.IncludedVAT = product.IncludedVAT;
            productEdit.ModifiedDate = DateTime.Now;
            db.SaveChanges();

            
            return product.ID;
        }

        public void UpdateImages(long productId,string images)
        {
            var product = db.Products.Find(productId);
            product.MoreImages = images;
            db.SaveChanges();
        }
    }
}
