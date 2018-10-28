using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace FlyBase
{
    public static class Routing
    {
        /* massive data query - get/put/post */
        public const string GetAllRoute = "/MassiveAll";
        public const string GetActiveRoute = "/ActiveData";
        public const string GetArchive = "/MassiveArchive";
        public const string GetColumns = "/MassiveColumns";
        public const string GetTables = "/MassiveTables";
        public const string GetViews = "/MassiveViews";
        public const string GetDelete = "/MassiveDelete";
        public const string GetForm = "/MassiveForm";
        public const string GetInsertForm = "/MassiveInsertForm";
        public const string GetInsertRoute = "/MassiveInsert";
        public const string GetQueryRoute = "/MassiveQuery";
        public const string GetSimpleRoute = "/MassiveSimple";
        public const string GetUpdateRoute = "/MassiveUpdate";

        /* special */
        public const string LogInsert = "/log/{table}/{where}/{arguments}/{columns}/{order}/{transactiontype}";
    }
}