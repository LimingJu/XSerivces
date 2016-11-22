﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedModel;
using SharedModel.Identity;

namespace BackOfficeSystem.DynamicData.FieldTemplates
{
    public partial class ServiceUserGuidLinkedChildren : System.Web.DynamicData.FieldTemplateUserControl
    {
        private bool _allowNavigation = true;
        private string _navigateUrl;

        public string NavigateUrl
        {
            get
            {
                return _navigateUrl;
            }
            set
            {
                _navigateUrl = value;
            }
        }

        public bool AllowNavigation
        {
            get
            {
                return _allowNavigation;
            }
            set
            {
                _allowNavigation = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //HyperLink1.Text = "View " + ChildrenColumn.ChildTable.DisplayName;
        }

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

            var serviceUserEntity = entity as ServiceIdentityUser;
            if (ChildrenColumn.ChildTable.EntityType == typeof(ServiceIdentityUserClaim))
            {
                using (var db = new ApplicationDbContext())
                {
                    var entityCollection = db.ServiceIdentityUserClaimModels
                        .Where(c => c.UserId == serviceUserEntity.Id).ToList();//.Select(s => s.ClaimType + ":" + s.ClaimValue).ToList();
                    Repeater1.DataSource = entityCollection;
                    Repeater1.DataBind();
                    if (entityCollection == null || !entityCollection.Any())
                    {
                        HyperLink1.Visible = true;
                    }
                }
            }
            else if (ChildrenColumn.ChildTable.EntityType == typeof(IdentityUserLogin))
            {
                using (var db = new ApplicationDbContext())
                {
                    var entityCollection = db.IdentityUserLoginModels
                        .Where(item => item.UserId == serviceUserEntity.Id).ToList();
                    Repeater1.DataSource = entityCollection;
                    Repeater1.DataBind();
                    if (entityCollection == null || !entityCollection.Any())
                    {
                        HyperLink1.Visible = true;
                    }
                }
            }
            else if (ChildrenColumn.ChildTable.EntityType == typeof(ServiceIdentityUserRole))
            {
                using (var db = new ApplicationDbContext())
                {
                    var entityCollection = from roles in db.Roles
                                           join userRole in db.ServiceIdentityUserRoleModels
                                               on roles.Id equals userRole.RoleId
                                           where userRole.UserId == serviceUserEntity.Id
                                           select roles;
                    Repeater1.DataSource = entityCollection.ToList();
                    Repeater1.DataBind();
                    if (entityCollection == null || !entityCollection.Any())
                    {
                        HyperLink1.Visible = true;
                    }
                }
            }
        }

        protected void DynamicHyperLink_DataBinding(object sender, EventArgs e)
        {
            var repeaterItem = ((DynamicHyperLink)sender).BindingContainer as RepeaterItem;
            var identityUserClaim = repeaterItem?.DataItem as ServiceIdentityUserClaim;
            if (identityUserClaim != null)
                ((DynamicHyperLink)sender).Text = identityUserClaim.ClaimType + ":" + identityUserClaim.ClaimValue;
            else if ((repeaterItem?.DataItem as ServiceIdentityUserRole) != null)
            {
                var role = repeaterItem?.DataItem as ServiceIdentityUserRole;
                ((DynamicHyperLink)sender).Text = role.ServiceIdentityRole.Name;
            }
        }

        protected string GetChildrenPath()
        {
            if (!AllowNavigation)
            {
                return null;
            }

            if (String.IsNullOrEmpty(NavigateUrl))
            {
                return ChildrenPath;
            }
            else
            {
                return BuildChildrenPath(NavigateUrl);
            }
        }

        protected string GetDisplayString()
        {
            object value = FieldValue;

            if (value == null)
            {
                return FormatFieldValue(ForeignKeyColumn.GetForeignKeyString(Row));
            }
            else
            {
                return FormatFieldValue(ForeignKeyColumn.ParentTable.GetDisplayString(value));
            }
        }

        protected string GetNavigateUrl()
        {
            if (!AllowNavigation)
            {
                return null;
            }

            if (String.IsNullOrEmpty(NavigateUrl))
            {
                return ForeignKeyPath;
            }
            else
            {
                return BuildForeignKeyPath(NavigateUrl);
            }
        }

        public override Control DataControl
        {
            get
            {
                return Wrapper;
            }
        }

    }
}
