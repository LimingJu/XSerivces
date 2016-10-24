using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModel;
using EntityDataSource = Microsoft.AspNet.EntityDataSource.EntityDataSource;
using EntityDataSourceChangingEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangingEventArgs;

namespace BackOfficeSystem
{
    public partial class ManyToMany_EditField : FieldTemplateUserControl
    {
        protected ObjectContext ObjectContext { get; set; }

        public void Page_Load(object sender, EventArgs e)
        {
            EntityDataSource ds = (EntityDataSource)this.FindDataSourceControl();

            ds.ContextCreated += (_, ctxCreatedEnventArgs) => ObjectContext = ctxCreatedEnventArgs.Context;

            ds.Updating += new EventHandler<EntityDataSourceChangingEventArgs>(DataSource_UpdatingOrInserting);
            ds.Inserting += new EventHandler<EntityDataSourceChangingEventArgs>(DataSource_UpdatingOrInserting);
        }

        void DataSource_UpdatingOrInserting(object sender, EntityDataSourceChangingEventArgs e)
        {
            MetaTable childTable = ChildrenColumn.ChildTable;

            if (Mode == DataBoundControlMode.Edit)
            {
                ObjectContext.LoadProperty(e.Entity, Column.Name);
            }

            dynamic entityCollection = null;

            if (e.Entity is PosDiscount)
            {
                using (var db = new ApplicationDbContext())
                {
                    var targetTrx = (PosDiscount)(e.Entity);
                    if (Mode == DataBoundControlMode.Insert)
                    {
                        db.PosDiscountModels.Add(targetTrx);
                        db.SaveChanges();
                    }

                    var linkedPosItems = db.PosItemModels.Include(trx => trx.DiscountedIn)
                        .Where(trx => trx.Id == targetTrx.Id)
                        .SelectMany(t => t.DiscountedIn).ToList();
                    foreach (var childEntity in db.PosItemModels.ToList())
                    {
                        var isCurrentlyInList = ListContainsEntity(childTable, linkedPosItems, childEntity);

                        string pkString = childTable.GetPrimaryKeyString(childEntity);
                        ListItem listItem = CheckBoxList1.Items.FindByValue(pkString);
                        if (listItem == null)
                            continue;

                        if (listItem.Selected)
                        {
                            if (!isCurrentlyInList)
                            {
                                //linkedSaleItems.Add(childEntity);
                                var target = db.PosDiscountModels.Include(trx => trx.Targets)
                                        .First(trx => trx.Id == targetTrx.Id);
                                target.Targets.Add(childEntity);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            if (isCurrentlyInList)
                            {
                                //linkedSaleItems.Remove(childEntity);
                                var target = db.PosDiscountModels.Include(trx => trx.Targets)
                                        .First(trx => trx.Id == targetTrx.Id);
                                target.Targets.Remove(childEntity);
                                db.SaveChanges();
                            }
                        }
                    }
                    e.Cancel = true;
                    return;
                }
            }
            else if (e.Entity is PosItem)
            {
                using (var db = new ApplicationDbContext())
                {
                    var targetPosItem = (PosItem)(e.Entity);
                    if (Mode == DataBoundControlMode.Insert)
                    {
                        db.PosItemModels.Add(targetPosItem);
                        db.SaveChanges();
                    }

                    var involvedInPosDiscount = db.PosItemModels.Include(item => item.DiscountedIn)
                        .Where(item => item.Id == targetPosItem.Id)
                        .SelectMany(t => t.DiscountedIn).ToList();
                    foreach (var childEntity in db.PosItemModels.ToList())
                    {
                        var isCurrentlyInList = ListContainsEntity(childTable, involvedInPosDiscount, childEntity);

                        string pkString = childTable.GetPrimaryKeyString(childEntity);
                        ListItem listItem = CheckBoxList1.Items.FindByValue(pkString);
                        if (listItem == null)
                            continue;

                        if (listItem.Selected)
                        {
                            if (!isCurrentlyInList)
                            {
                                //entityCollection.Add(childEntity);
                                var target = db.PosDiscountModels.Include(item => item.Targets)
                                    .First(item => item.Id == targetPosItem.Id);
                                target.Targets.Add(childEntity);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            if (isCurrentlyInList)
                            {
                                //entityCollection.Remove(childEntity);
                                var target = db.PosDiscountModels.Include(item => item.Targets)
                                        .First(item => item.Id == targetPosItem.Id);
                                target.Targets.Remove(childEntity);
                                db.SaveChanges();
                            }
                        }
                    }
                    e.Cancel = true;
                    return;
                }
            }
            else if (e.Entity is PosTrxDiscount)
            {
                using (var db = new ApplicationDbContext())
                {
                    var targetPosTrxDiscount = (PosTrxDiscount)(e.Entity);
                    if (Mode == DataBoundControlMode.Insert)
                    {
                        db.PosTrxDiscountModels.Add(targetPosTrxDiscount);
                        db.SaveChanges();
                    }

                    var linkedPosTrxItems = db.PosTrxDiscountModels.Include(item => item.Items)
                        .Where(item => item.Id == targetPosTrxDiscount.Id)
                        .SelectMany(t => t.Items).ToList();
                    foreach (var childEntity in db.PosTrxItemModels.ToList())
                    {
                        var isCurrentlyInList = ListContainsEntity(childTable, linkedPosTrxItems, childEntity);

                        string pkString = childTable.GetPrimaryKeyString(childEntity);
                        ListItem listItem = CheckBoxList1.Items.FindByValue(pkString);
                        if (listItem == null)
                            continue;

                        if (listItem.Selected)
                        {
                            if (!isCurrentlyInList)
                            {
                                //entityCollection.Add(childEntity);
                                var target = db.PosTrxDiscountModels.Include(item => item.Items)
                                    .First(item => item.Id == targetPosTrxDiscount.Id);
                                target.Items.Add(childEntity);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            if (isCurrentlyInList)
                            {
                                //entityCollection.Remove(childEntity);
                                var target = db.PosTrxDiscountModels.Include(item => item.Items)
                                        .First(item => item.Id == targetPosTrxDiscount.Id);
                                target.Items.Remove(childEntity);
                                db.SaveChanges();
                            }
                        }
                    }
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                entityCollection = Column.EntityTypeProperty.GetValue(e.Entity, null);
            }
            //if (Mode == DataBoundControlMode.Edit && (entityCollection != null && entityCollection is RelatedEnd))
            //    entityCollection.Load();

            foreach (dynamic childEntity in childTable.GetQuery(e.Context))
            {
                var isCurrentlyInList = ListContainsEntity(childTable, entityCollection, childEntity);

                string pkString = childTable.GetPrimaryKeyString(childEntity);
                ListItem listItem = CheckBoxList1.Items.FindByValue(pkString);
                if (listItem == null)
                    continue;

                if (listItem.Selected)
                {
                    if (!isCurrentlyInList)
                        entityCollection.Add(childEntity);
                }
                else
                {
                    if (isCurrentlyInList)
                        entityCollection.Remove(childEntity);
                }
            }
        }

        private static bool ListContainsEntity(MetaTable table, IEnumerable<object> list, object entity)
        {
            if (list == null) return false;
            return list.Any(e => AreEntitiesEqual(table, e, entity));
        }

        private static bool AreEntitiesEqual(MetaTable table, object entity1, object entity2)
        {
            return Enumerable.SequenceEqual(table.GetPrimaryKeyValues(entity1), table.GetPrimaryKeyValues(entity2));
        }

        protected void CheckBoxList1_DataBound(object sender, EventArgs e)
        {
            MetaTable childTable = ChildrenColumn.ChildTable;

            IEnumerable<object> entityCollection = null;

            if (Mode == DataBoundControlMode.Edit)
            {
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
                    using (var db = new ApplicationDbContext())
                    {
                        var targetTrx = (PosTrx)entity;
                        entityCollection = db.PosTrxModels.Include(trx => trx.Items)
                            .Where(trx => trx.Id == targetTrx.Id)
                            .SelectMany(t => t.Items).ToList();
                    }
                }
                else if (entity is PosItem)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var targetPosItem = (PosItem)entity;
                        entityCollection = db.PosItemModels.Include(item => item.DiscountedIn)
                            .Where(item => item.Id == targetPosItem.Id)
                            .SelectMany(t => t.DiscountedIn).ToList();
                    }
                }
                else if (entity is PosDiscount)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var targetDiscount = (PosDiscount)entity;
                        entityCollection = db.PosDiscountModels.Include(item => item.Targets)
                            .Where(item => item.Id == targetDiscount.Id)
                            .SelectMany(t => t.Targets).ToList();
                    }
                }
                else if (entity is PosTrxDiscount)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var targetDiscount = (PosTrxDiscount)entity;
                        entityCollection = db.PosTrxDiscountModels.Include(item => item.Items)
                            .Where(item => item.Id == targetDiscount.Id)
                            .SelectMany(t => t.Items).ToList();
                    }
                }
                else
                {
                    entityCollection = (IEnumerable<object>)Column.EntityTypeProperty.GetValue(entity, null);
                    var realEntityCollection = entityCollection as RelatedEnd;
                    if (realEntityCollection != null && !realEntityCollection.IsLoaded)
                    {
                        realEntityCollection.Load();
                    }
                }
            }

            foreach (object childEntity in childTable.GetQuery(ObjectContext))
            {
                ListItem listItem = new ListItem(
                    childTable.GetDisplayString(childEntity),
                    childTable.GetPrimaryKeyString(childEntity));

                if (Mode == DataBoundControlMode.Edit)
                {
                    listItem.Selected = ListContainsEntity(childTable, entityCollection, childEntity);
                }
                CheckBoxList1.Items.Add(listItem);
            }
        }

        public override Control DataControl
        {
            get
            {
                return CheckBoxList1;
            }
        }

    }
}
