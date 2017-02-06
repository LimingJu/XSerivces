using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web.UI;
using SharedModel;
using SharedModel.Identity;

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
                if (Column.ColumnType == typeof(BusinessUnit))
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var targetUser = (ServiceIdentityUser)entity;
                        var entityCollection = db.Users.Include(u => u.RestrictedInBusinessUnits)
                            .Where(r => r.Id == targetUser.Id)
                            .SelectMany(t => t.RestrictedInBusinessUnits).ToList();
                        Repeater1.DataSource = entityCollection;
                        Repeater1.DataBind();
                    }
                }
            }
            //else if (entity is SiteInfo)
            //{
            //    using (var db = new ApplicationDbContext())
            //    {
            //        var targetSiteInfo = (SiteInfo)entity;
            //        var entityCollection = db.SiteInfoModels.Include(u => u.BoundServiceUsers)
            //            .Where(s => s.Id == targetSiteInfo.Id)
            //            .SelectMany(t => t.BoundServiceUsers).ToList();
            //        Repeater1.DataSource = entityCollection;
            //        Repeater1.DataBind();
            //    }
            //}
            else if (entity is ServiceIdentityRole)
            {
                if (Column.ColumnType == typeof(BusinessUnit))
                {
                    //using (var db = new ApplicationDbContext())
                    //{
                    //    var targetRole = (ServiceIdentityRole)entity;
                    //    var entityCollection = db.Roles.Include(u => u.RestrictedInBusinessUnits)
                    //        .Where(r => r.Id == targetRole.Id)
                    //        .SelectMany(t => t.RestrictedInBusinessUnits).ToList();
                    //    Repeater1.DataSource = entityCollection;
                    //    Repeater1.DataBind();
                    //}
                }
                else if (Column.ColumnType == typeof(ServiceUserOperation))
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var targetRole = (ServiceIdentityRole)entity;
                        var entityCollection = db.Roles.Include(u => u.ProhibitedOperations)
                            .Where(r => r.Id == targetRole.Id)
                            .SelectMany(t => t.ProhibitedOperations).ToList();
                        Repeater1.DataSource = entityCollection;
                        Repeater1.DataBind();
                    }
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
