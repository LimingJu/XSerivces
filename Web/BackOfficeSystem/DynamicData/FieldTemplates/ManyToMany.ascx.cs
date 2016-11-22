using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web.UI;
using SharedModel;

namespace BackOfficeSystem
{
    public partial class ManyToManyField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object entity;
            ICustomTypeDescriptor rowDescriptor = Row as ICustomTypeDescriptor;
            if (rowDescriptor != null)
            {
                entity = rowDescriptor.GetPropertyOwner(null);
            }
            else
            {
                entity = Row;
            }

            if (entity is PosTrx)
            {
                //using (var db = new ApplicationDbContext())
                //{
                //    var targetTrx = (PosTrx)entity;
                //    var entityCollection = db.PosTrxModels.Include(trx => trx.Items)
                //        .Where(trx => trx.Id == targetTrx.Id)
                //        .SelectMany(t => t.Items).ToList();
                //    Repeater1.DataSource = entityCollection;
                //    Repeater1.DataBind();
                //}
            }
            else if (entity is PosItem)
            {
                using (var db = new ApplicationDbContext())
                {
                    var targetPosItem = (PosItem)entity;
                    var entityCollection = db.PosItemModels.Include(item => item.DiscountedIn)
                        .Where(item => item.Id == targetPosItem.Id)
                        .SelectMany(t => t.DiscountedIn).ToList();
                    Repeater1.DataSource = entityCollection;
                    Repeater1.DataBind();
                }
            }
            else if (entity is PosDiscount)
            {
                using (var db = new ApplicationDbContext())
                {
                    var targetPosDiscountItem = (PosDiscount)entity;
                    var entityCollection = db.PosDiscountModels.Include(item => item.Targets)
                        .Where(item => item.Id == targetPosDiscountItem.Id)
                        .SelectMany(t => t.Targets).ToList();
                    Repeater1.DataSource = entityCollection;
                    Repeater1.DataBind();
                }
            }
            else if (entity is PosTrxDiscount)
            {
                using (var db = new ApplicationDbContext())
                {
                    var targetPosDiscountItem = (PosTrxDiscount)entity;
                    var entityCollection = db.PosTrxDiscountModels.Include(item => item.Items)
                        .Where(item => item.Id == targetPosDiscountItem.Id)
                        .SelectMany(t => t.Items).ToList();
                    Repeater1.DataSource = entityCollection;
                    Repeater1.DataBind();
                }
            }
            else if (entity is ServiceIdentityUser)
            {
                using (var db = new ApplicationDbContext())
                {
                    var targetServiceIdentityUser = (ServiceIdentityUser)entity;
                    var entityCollection = db.Users.Include(u=>u.BindingSites)
                        .Where(u => u.Id == targetServiceIdentityUser.Id)
                        .SelectMany(t => t.BindingSites).ToList();
                    Repeater1.DataSource = entityCollection;
                    Repeater1.DataBind();
                }
            }
            else if (entity is SiteInfo)
            {
                using (var db = new ApplicationDbContext())
                {
                    var targetSiteInfo = (SiteInfo)entity;
                    var entityCollection = db.SiteInfoModels.Include(u => u.BoundServiceUsers)
                        .Where(s => s.Id == targetSiteInfo.Id)
                        .SelectMany(t => t.BoundServiceUsers).ToList();
                    Repeater1.DataSource = entityCollection;
                    Repeater1.DataBind();
                }
            }
            else
            {
                var entityCollection = Column.EntityTypeProperty.GetValue(entity, null);
                var realEntityCollection = entityCollection as RelatedEnd;
                if (realEntityCollection != null && !realEntityCollection.IsLoaded)
                {
                    realEntityCollection.Load();
                }

                Repeater1.DataSource = entityCollection;
                Repeater1.DataBind();
            }
        }

        public override Control DataControl
        {
            get
            {
                return Repeater1;
            }
        }

    }
}
