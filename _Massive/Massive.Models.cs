using Massive;
using Newtonsoft.Json;
using oMassive;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Web;
using System.Web.Services;
using System.Linq;

namespace Massive
{
    //form builder
    public class FlyBase_FormDesigner : DynamicModel { public FlyBase_FormDesigner() : base("DefaultConnection", "FlyBase_FormDesigner", "DesignerId") { } }
    public class FlyBase_FilledForms : DynamicModel { public FlyBase_FilledForms() : base("DefaultConnection", "FlyBase_FilledForms", "FormId") { } }
    //sql models
    public class Users : DynamicModel { public Users() : base("DefaultConnection", "aspnet_users", "UserId") { } }
    public class Roles : DynamicModel { public Roles() : base("DefaultConnection", "aspnet_roles", "RoleId") { } }
    public class UsersInRoles : DynamicModel { public UsersInRoles() : base("DefaultConnection", "aspnet_usersinroles", "UserId") { } }
    public class UsersInComp : DynamicModel { public UsersInComp() : base("DefaultConnection", "aspnet_UsersInCompanies", "UserId") { } }
    public class Companies : DynamicModel { public Companies() : base("DefaultConnection", "FlyBase_Companies", "CompanyId") { } }
    public class Membership : DynamicModel { public Membership() : base("DefaultConnection", "aspnet_membership", "UserId") { } }
    public class Events : DynamicModel { public Events() : base("DefaultConnection", "aspnet_WebEvent_Events", "UserId") { } }
    public class Navigation : DynamicModel { public Navigation() : base("DefaultConnection", "vw_FlyBasePagesWithRoles", "Id") { } }
    public class FlyBaseMenu : DynamicModel { public FlyBaseMenu() : base("DefaultConnection", "FlyBase_menu", "Id") { } }
    public class DynamicTable : DynamicModel { public DynamicTable() : base("DefaultConnection") { } }
    public class UserCompanyInfo : DynamicModel { public UserCompanyInfo() : base("DefaultConnection", "vwUserCompanyInfo", "CompanyId") { } }

    //role tables
    public class FlyBasePageRoles : DynamicModel { public FlyBasePageRoles() : base("DefaultConnection", "FlyBase_PagesInRoles") { } }
    public class FlyBaseSiteInRoles : DynamicModel { public FlyBaseSiteInRoles() : base("DefaultConnection", "FlyBase_SiteInRoles") { } }

    //rights & permissions views (not used to update/insert)
    public class SiteRights : DynamicModel { public SiteRights() : base("DefaultConnection", "vw_FlyBase_SiteInRoles", "RoleId") { } }
    public class PageRights : DynamicModel { public PageRights() : base("DefaultConnection", "vw_FlyBase_PagesInRoles", "PageId") { } }
    public class UserRights : DynamicModel { public UserRights() : base("DefaultConnection", "vw_UserRights", "UserId") { } }

    //dti - progress db through sqlserver
    public class FlyPaper_CustomerSearch : DynamicModel { public FlyPaper_CustomerSearch() : base("DefaultConnection", "vw_CustomerData", "CustomerID") { } }
    //fallback when dti is not available (search only)
    public class FlyPaper_CustomerSearchFallBack : DynamicModel { public FlyPaper_CustomerSearchFallBack() : base("DefaultConnection", "vw_FallBackCustomerData", "CustomerID") { } }

    //flybase contract tables
    public class FlyBase_Accounts : DynamicModel { public FlyBase_Accounts() : base("DefaultConnection", "FlyBase_Accounts", "Id") { } }
    public class FlyBase_Additions : DynamicModel { public FlyBase_Additions() : base("DefaultConnection", "FlyBase_Additions", "Id") { } }
    public class FlyBase_Contacts : DynamicModel { public FlyBase_Contacts() : base("DefaultConnection", "FlyBase_Contacts", "Id") { } }
    public class FlyBase_PackagesInContract : DynamicModel { public FlyBase_PackagesInContract() : base("DefaultConnection", "FlyBase_PackagesInContract", "Id") { } }
    public class FlyBase_Contracts : DynamicModel { public FlyBase_Contracts() : base("DefaultConnection", "FlyBase_Contracts", "Id") { } }
    public class FlyBase_Notes : DynamicModel { public FlyBase_Notes() : base("DefaultConnection", "FlyBase_Notes", "Id") { } }
    public class FlyBase_Adjustments : DynamicModel { public FlyBase_Adjustments() : base("DefaultConnection", "Flybase_Adjustments", "Id") { } }
    public class FlyBase_Packages : DynamicModel { public FlyBase_Packages() : base("DefaultConnection", "FlyBase_Packages", "Package_Id") { } }
    public class FlyBase_Agency : DynamicModel { public FlyBase_Agency() : base("DefaultConnection", "Flybase_Agency", "Id") { } }
    public class FlyBase_UserAgencies : DynamicModel { public FlyBase_UserAgencies() : base("DefaultConnection", "FlyBase_UserAgencies", "UserId") { } }
    public class FlyBase_Customers : DynamicModel { public FlyBase_Customers() : base("DefaultConnection", "FlyBase_Customers", "Id") { } }
    public class FlyBase_DTICustomers : DynamicModel { public FlyBase_DTICustomers() : base("DefaultConnection", "vw_FlyBase_Cust", "Id") { } }
}

namespace oMassive
{
    //oracle models
    public class oDyanmicTable : DynamicModel { public oDyanmicTable() : base("oracle") { } }
}