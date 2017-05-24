using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SharedConfig;
using SharedConfig.Util;
using SharedModel;
using SharedModel.Identity;

namespace AuthService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DefaultAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DefaultAppDbContext context)
        {
            try
            {

                #region add pre-defined operation

                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Item Sale" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Price Check" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Void Item" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Item Comment" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Price Override" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Set Qty" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Clear Qty" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Item Search" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Return Item" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Weigh Item" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Fuel Item Sale" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Linked Items Add" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Set Dimensions" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Return Transaction" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Show Journal" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Loyalty Request" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Clear Salesperson" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Invoice Comment" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Change Unit of Measure"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Discount Voucher Add" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Discount Voucher Remove"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Cash" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Card" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Customer Account" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Currency" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Cheque" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Transaction" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Cash Quick" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Loyalty" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Corporate Card" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Change Back" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Void Payment" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Get Fleet Card Information"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Credit Memo" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pay Giftcard" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Line Discount Amount" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Line Discount Percent" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Total Discount Amount" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Total Discount Percent"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Popup menu" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Sub Menu" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Void Transaction" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Transaction Comment" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Sales Person" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Suspend Transaction" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Recall Transaction" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Recall Unconcluded Transaction"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Card Swipe" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Pharmcy Prescription Cancel"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Pharmcy Prescriptions" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Issue Credit Memo" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Issue Gift Certificate"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Display Total" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Sales Order" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Sales Invoice" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Income Account" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Expense Account" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Customer" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Customer Search" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Customer Clear" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Customer Transactions" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Customer Transactions Report"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Customer Balance Report"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Customer Add" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Logon" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Log Off" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Change User" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Lock Terminal" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Log Off Force" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Negative Adjustment" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Inventory Lookup" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Application Exit" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Print X" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Print Z" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Print Tax Free" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Print Previous Slip" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Print Previous Invoice"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Upload Printer Logo" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Restart Computer" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Shut Down Computer" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Design Mode Enable" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Design Mode disable" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Minimize POS Window" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Blank Operation" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Open Drawer" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "End Of Day" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "End Of Shift" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Tender Declaration" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation()
                    {
                        CreatedDateTime = DateTime.Now,
                        OperationName = "Customer Account Deposit"
                    });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Declare Start Amount" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Float Entry" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Tender Removal" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Safe Drop" });
                context.ServiceUserOperationModels.AddOrUpdate(p => p.OperationName,
                    new ServiceUserOperation() { CreatedDateTime = DateTime.Now, OperationName = "Bank Drop" });

                #endregion

                //  This method will be called after migrating to the latest version.

                context.CurrencyModels.AddOrUpdate(p => p.Name, new Currency() { Name = "CNY" });
                context.CurrencyModels.AddOrUpdate(p => p.Name, new Currency() { Name = "JAP" });
                context.CurrencyModels.AddOrUpdate(p => p.Name, new Currency() { Name = "SGD" });
                context.CurrencyModels.AddOrUpdate(p => p.Name, new Currency() { Name = "USD" });

                #region add pre-defined BusinessUnit


                var globalUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "Global",
                    Description =
                        "Pre-defined, the top most, and widest range unit for include all other units, user assgined in this unit has no business unit constrait.",
                    Name = UserPriviledgeHelper.GlobalUnitName,
                    ParentBusinessUnit = null,
                    UnitType = BusinessUnitTypeEnum.Global
                };
                // have to use this detection, otherwise, the AddOrUpdate will always update the ParaentBusinessUnit to null in Update mode.
                if (!context.BusinessUnitModels.Any(b => b.Name == globalUnit.Name))
                    context.BusinessUnitModels.Add(globalUnit);

                var eastAsiaUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "EastAsia",
                    Description = "Pre-defined, user assgined in this unit has business unit constrait to EastAsia",
                    Name = "RegionEastAsia",
                    ParentBusinessUnit = globalUnit,
                    UnitType = BusinessUnitTypeEnum.Region
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == eastAsiaUnit.Name))
                    context.BusinessUnitModels.Add(eastAsiaUnit);

                var southAsiaUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "SouthAsia",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to SouthAsia",
                    Name = "RegionSouthAsia",
                    ParentBusinessUnit = globalUnit,
                    UnitType = BusinessUnitTypeEnum.Region
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == southAsiaUnit.Name))
                    context.BusinessUnitModels.Add(southAsiaUnit);

                var northAmericaUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "NorthAmerica",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to NorthAmerica",
                    Name = "RegionNorthAmerica",
                    ParentBusinessUnit = globalUnit,
                    UnitType = BusinessUnitTypeEnum.Region
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == northAmericaUnit.Name))
                    context.BusinessUnitModels.Add(northAmericaUnit);

                var singaporeCountryUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "Singapore",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to country singapore",
                    Name = "CountrySingapore",
                    ParentBusinessUnit = southAsiaUnit,
                    UnitType = BusinessUnitTypeEnum.Country
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == singaporeCountryUnit.Name))
                    context.BusinessUnitModels.Add(singaporeCountryUnit);

                var thailandCountryUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "Thailand",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to country thailand",
                    Name = "CountryThailand",
                    ParentBusinessUnit = southAsiaUnit,
                    UnitType = BusinessUnitTypeEnum.Country
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == thailandCountryUnit.Name))
                    context.BusinessUnitModels.Add(thailandCountryUnit);

                var vietnamCountryUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "Vietnam",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to country vietnam",
                    Name = "CountryVietnam",
                    ParentBusinessUnit = southAsiaUnit,
                    UnitType = BusinessUnitTypeEnum.Country
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == vietnamCountryUnit.Name))
                    context.BusinessUnitModels.Add(vietnamCountryUnit);

                var laosCountryUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "Laos",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to country laos",
                    Name = "CountryLaos",
                    ParentBusinessUnit = southAsiaUnit,
                    UnitType = BusinessUnitTypeEnum.Country
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == laosCountryUnit.Name))
                    context.BusinessUnitModels.Add(laosCountryUnit);

                var burmaCountryUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "Burma",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to country burma",
                    Name = "CountryBurma",
                    ParentBusinessUnit = southAsiaUnit,
                    UnitType = BusinessUnitTypeEnum.Country
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == burmaCountryUnit.Name))
                    context.BusinessUnitModels.Add(burmaCountryUnit);

                var cambodiaCountryUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "Cambodia",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to country cambodia",
                    Name = "CountryCambodia",
                    ParentBusinessUnit = southAsiaUnit,
                    UnitType = BusinessUnitTypeEnum.Country
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == cambodiaCountryUnit.Name))
                    context.BusinessUnitModels.Add(cambodiaCountryUnit);

                /* below are testing purpose unit */
                var _Test_emsgProjectUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "singapore",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to project emsg",
                    Name = "Project_Test_Emsg",
                    ParentBusinessUnit = singaporeCountryUnit,
                    UnitType = BusinessUnitTypeEnum.Project
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == _Test_emsgProjectUnit.Name))
                    context.BusinessUnitModels.Add(_Test_emsgProjectUnit);

                var _Test_EECO_SiteUnit = new BusinessUnit()
                {
                    CreatedDateTime = DateTime.Now,
                    Address = "singapore",
                    Description =
                        "Pre-defined, user assgined in this unit has business unit constrait to project emsg, site EECO",
                    Name = "Site_Test_Eeco",
                    ParentBusinessUnit = _Test_emsgProjectUnit,
                    UnitType = BusinessUnitTypeEnum.Site
                };
                if (!context.BusinessUnitModels.Any(b => b.Name == _Test_EECO_SiteUnit.Name))
                    context.BusinessUnitModels.Add(_Test_EECO_SiteUnit);

                #endregion

                context.SaveChanges();

                #region add pre-defined Roles

                UserPriviledgeHelper.CreateOrUpdateRole("SuperAdmin", "top most (first) lv admin, the god", "");

                UserPriviledgeHelper.CreateOrUpdateRole("RegionAdmin",
                    "2nd lv admin, need further user Region id to specify the priviledge range", "SuperAdmin");
                //UserPriviledgeHelper.CreateOrUpdateRole("EastAsiaRegionAdmin",
                //   "region admin for east asia", "RegionAdmin", new List<int>()
                //    {
                //        context.BusinessUnitModels.First(b=>b.Name=="RegionEastAsia").Id
                //    });
                //UserPriviledgeHelper.CreateOrUpdateRole("SouthAsiaRegionAdmin",
                //  "region admin for south asia", "RegionAdmin", new List<int>()
                //   {
                //        context.BusinessUnitModels.First(b=>b.Name=="RegionSouthAsia").Id
                //   });
                UserPriviledgeHelper.CreateOrUpdateRole("CountryAdmin",
                    "3rd lv admin, need further user Country id to specify the priviledge range", "RegionAdmin");

                UserPriviledgeHelper.CreateOrUpdateRole("ProjectAdmin",
                    "4th lv admin, need further user Project id to specify the priviledge range", "CountryAdmin");

                //UserPriviledgeHelper.CreateOrUpdateRole("EmsgProjectFusionService",
                //    "Emsg project local fusion service should in this role", "ProjectAdmin", new List<int>()
                //    {
                //        context.BusinessUnitModels.First(b=>b.Name=="Project_Test_Emsg").Id
                //    });

                UserPriviledgeHelper.CreateOrUpdateRole("SiteAdmin", "5th lv admin, need further Site id to specify the priviledge range",
                    "ProjectAdmin");
                UserPriviledgeHelper.CreateOrUpdateRole("SiteManager",
                    "6th lv admin, need further user Site id to specify the priviledge range", "SiteAdmin");
                //UserPriviledgeHelper.AssignProhibitedOperationsToRole("SiteManager", new string[] { "Return Item", "Show Journal" });
                UserPriviledgeHelper.CreateOrUpdateRole("Cashier", "7th lv admin, need further user Site id to specify the priviledge range",
                    "SiteManager");
                //UserPriviledgeHelper.CreateOrUpdateRole("Emsg_EECO_Cashier", "cashier for Emsg project, site EECO",
                //    "Cashier", new List<int>()
                //    {
                //         context.BusinessUnitModels.First(b=>b.Name=="Site_Test_Eeco").Id
                //    });

                #endregion

                #region add pre-defined Users

                UserPriviledgeHelper.CreateOrUpdateUser("x", "x@nodomain.com", "Pa88word!", "", new List<string>() { "RegionEastAsia" });
                UserPriviledgeHelper.AssignUserToRoles("x", new string[] { "SuperAdmin" });

                UserPriviledgeHelper.CreateOrUpdateUser("102", "x@emsgEECO.com", "Pa88word!", "", new List<string>() { "Site_Test_Eeco" });
                UserPriviledgeHelper.AssignUserToRoles("102", new string[] { "Cashier" });

                #endregion
            }
            catch (Exception ex)
            {
                using (var tw = new StreamWriter(@"c:\XservicesErrorLog.txt"))
                {
                    tw.WriteLine(ex);
                    var aggException = ex as System.AggregateException;
                    var entityException =
                        aggException?.InnerException as System.Data.Entity.Validation.DbEntityValidationException;
                    if (entityException != null)
                    {
                        tw.WriteLine(aggException + "||||||||" +
                                     entityException.EntityValidationErrors.SelectMany(e => e.ValidationErrors)
                                         .Select(e => e.PropertyName + ":" + e.ErrorMessage)
                                         .Aggregate((acc, n) => acc + n));
                        //tw.WriteLine(aggException + "||||||||" + entityException.EntityValidationErrors.Count());
                        tw.Flush();
                    }
                }

                throw;
            }
        }
    }
}
