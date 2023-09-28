using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public long Edit(Content content)
        {
            var contentEdit = GetByID(content.ID);
            //Xử lý Alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
                contentEdit.MetaTitle = content.MetaTitle;
            }
            contentEdit.Name = content.Name;
            contentEdit.Description = content.Description;
            contentEdit.Image = content.Image;
            contentEdit.Detail = content.Detail;
            contentEdit.Warranty = content.Warranty;
            contentEdit.MetaKeywords = content.MetaKeywords;
            contentEdit.MetaDescriptions = content.MetaDescriptions;
            contentEdit.ModifiedBy = content.ModifiedBy;
            contentEdit.Status = content.Status;
            contentEdit.Tags = content.Tags;
            contentEdit.ModifiedDate = DateTime.Now;
            db.SaveChanges();

            //Xử lý Tags
            if (!string.IsNullOrEmpty(content.Tags))
            {
                RemoveAllContentTag(content.ID);
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = CheckTag(tagId);
                    //insert to tag table
                    if (!existedTag)
                    {
                        InsertTag(tagId, tag);

                    }
                    //Insert to content tag table
                    InsertContentTag(content.ID, tagId);
                }
            }
            return content.ID;
        }

        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContenID == contentId));
            db.SaveChanges();
        }

        public long Create(Content content)
        {
            //Xử lý Alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            content.ViewCount = 0;
            db.Contents.Add(content);
            db.SaveChanges();

            //Xử lý Tags
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = CheckTag(tagId);
                    //insert to tag table
                    if (!existedTag)
                    {
                        InsertTag(tagId, tag);

                    }
                    //Insert to content tag table
                    InsertContentTag(content.ID, tagId);
                }
            }
            return content.ID;
        }

        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContenID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }

        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }


        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }

        public IEnumerable<Content> ListAllByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContenID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedBy=a.CreatedBy,
                             CreatedDate=a.CreatedDate,
                             ID=a.ID,

                         }).AsEnumerable().Select(x => new Content()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedBy = x.CreatedBy,
                             CreatedDate = x.CreatedDate,
                             ID=x.ID,
                         });
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }


        //List all content for client
        public IEnumerable<Content> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public List<Tag> ListTag(long contentID)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContenID == contentID
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name,

                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

    }
}
