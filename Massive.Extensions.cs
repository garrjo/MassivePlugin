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
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;

namespace Massive.Extensions
{
    [WebService(Namespace = "http://www.1001skins.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    #region WebMethods
    public class WebServices
    {
        public static string Conn = {ConnectionStringName_WebConfig};
        [WebMethod]
        public String ActiveData(string table, string where, string arguments = "", string order_by = "1", string columns = "*", string template = "")
        {
            var tbl = new DynamicModel(Conn, table);
            var Columns = (!columns.Contains("*") ? columns : null);
            var Where = where;
            object[] args = JsonConvert.DeserializeObject<object[]>(arguments);
            var OrderBy = JsonConvert.DeserializeObject<string>(order_by);
            var results = new List<dynamic>();

            if (Columns != null) results = tbl.All(where: Where, orderBy: OrderBy, columns: Columns, args: args).ToList().Where(p => p.EndDate >= DateTime.Now && p.StartDate <= DateTime.Now).ToList();
            else results = tbl.All(where: Where, orderBy: OrderBy, args: args).ToList();
            var obj_json = new { draw = 1, recordsTotal = results.Count(), recordsFiltered = results.Count(), data = results };

            return JsonConvert.SerializeObject(results);
        }
        [WebMethod]
        public String MassiveSimple(string table, string where)
        {
            var results = (new DynamicModel(Conn, table)).All(where: where);
            var result = JsonConvert.SerializeObject(results);
            return JsonConvert.SerializeObject(results);
        }
        [WebMethod]
        public String MassiveAll(string table, string where, string arguments = "", string columns = "*", string orderby = "0")
        {
            var tbl = new DynamicModel(Conn, table);
            var Columns = (!columns.Contains("*") ? columns : null);
            var Where = where;
            object[] args = (arguments != "" ? JsonConvert.DeserializeObject<object[]>(arguments) : null);
            var results = new List<dynamic>();
            if (Columns != null && args != null) results = tbl.All(where: Where, columns: Columns, args: args, orderBy: orderby).ToList();
            if (Columns != null && args == null) results = tbl.All(where: Where, columns: Columns, orderBy: orderby).ToList();
            if (Columns == null && args != null) results = tbl.All(where: Where, args: args, orderBy: orderby).ToList();
            if (Columns == null && args == null) results = tbl.All(where: Where, orderBy: orderby).ToList();
            else results = tbl.All(where: Where, orderBy: orderby).ToList();
            return JsonConvert.SerializeObject(results);
        }
        [WebMethod]
        public String MassiveQuery(string table, string query)
        {
            var tbl = new DynamicModel(Conn, table);
            var results = new List<dynamic>();
            results = tbl.Query(query).ToList();
            var result = JsonConvert.SerializeObject(results);
            LogInsert(table, query, "", "", "", "MassiveQuery");
            return result;
        }
        [WebMethod]
        public String MassiveInsert(string table, string arguments = "")
        {
            var tbl = new DynamicModel(Conn, table);
            string arg_string = arguments.Replace("\"", "'");
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(arg_string);
            var args = new ExpandoObject() as IDictionary<string, object>;

            dict.ToList().ForEach(p =>
            {
                dynamic item_value = (!String.IsNullOrEmpty(p.Value) ? item_value = p.Value : null);
                args.Add(p.Key, item_value);
            });

            var results = tbl.Insert(args);
            var result = JsonConvert.SerializeObject(results);
            LogInsert(table, "", arguments, "", "", "MassiveInsert");
            return result;
        }
        [WebMethod]
        public String MassiveInsertForm(string table, string args)
        {
            var result = MassiveInsert(table, args);
            LogInsert(table, "", args, "", "", "MassiveInsertForm");
            return result;
        }
        [WebMethod]
        public String MassiveDelete(dynamic table, string where)
        {
            var tbl = new DynamicModel(Conn, table);
            var result = JsonConvert.SerializeObject(tbl.Delete(where: where));
            LogInsert(table, where, "", "", "", "MassiveDelete");
            return result;
        }
        [WebMethod]
        public String MassiveUpdate(dynamic table, string where, string arguments)
        {
            var tbl = new DynamicModel(Conn, table);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(arguments);
            var args = new ExpandoObject() as IDictionary<string, object>;
            string app_where = String.Empty;
            where.Split('=').ToList().ForEach(w =>
            {
                Guid oguid = new Guid();
                var isGuid = Guid.TryParse(w, out oguid);
                if (isGuid) app_where = where.Replace(w, String.Format("'{0}'", oguid));
            });

            dict.ToList().ForEach(p =>
            {
                object item = (!String.IsNullOrEmpty(p.Value) ? p.Value : (object)DBNull.Value);
                args.Add(p.Key, item);
            });

            var result = JsonConvert.SerializeObject(tbl.Update(args, where: (!String.IsNullOrEmpty(app_where) ? app_where : where)));
            LogInsert(table, "", arguments, "", "", "MassiveUpdate");
            return result;
        }
        [WebMethod]
        public String MassiveArchive(dynamic table, string where, string arguments)
        {
            String tblname = table;
            var tbl = new DynamicModel(Conn, table);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(arguments);
            var args = new ExpandoObject() as IDictionary<string, object>;
            string app_where = String.Empty;
            where.Split('=').ToList().ForEach(w =>
            {
                Guid oguid = new Guid();
                var isGuid = Guid.TryParse(w, out oguid);
                if (isGuid) app_where = where.Replace(w, String.Format("'{0}'", oguid));
            });

            dict.ToList().ForEach(p =>
            {
                object item = (!String.IsNullOrEmpty(p.Value) ? p.Value : (object)DBNull.Value);
                args.Add(p.Key, item);
            });
            var result = JsonConvert.SerializeObject(tbl.Update(args, where: (!String.IsNullOrEmpty(app_where) ? app_where : where)));
            LogInsert(table, "", arguments, "", "", "MassiveArchive");
            return result;
        }
        [WebMethod]
        public String MassiveForm(string rform, string table)
        {
            NameValueCollection frm = new NameValueCollection();

            var form = JsonConvert.DeserializeObject<NameValueCollection>(rform);
            string[] a_filter = GetColumns(table);
            var keys = form.AllKeys;

            foreach (string key in form.AllKeys)
            {
                if (a_filter.Contains(key))
                {
                    frm.Add(key, form[key]);
                }
            }
            var result = JsonConvert.SerializeObject(frm);
            LogInsert(table, "", result, "", "", "MassiveForm");
            return result;
        }
        public void LogInsert(string _table, string _where, string _arguments, string _columns, string _order, string _transactiontype)
        {
            if (ConfigurationManager.AppSettings["EnableDataLog"] == "true")
            {
                // Get call stack
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();

                var tbl = new Massive.DynamicModel(Conn, "FlyBase_DataLog");
                var _reference = stackTrace.GetFrame(1).GetMethod().Name;
                var _transaction = stackTrace.GetFrame(1).GetType().Name;
                var _user = HttpContext.Current.User.Identity.Name.ToString();
                var obj = new { TimeStamp = DateTime.Now.ToShortDateString(), TableName = _table, Arguments = JsonConvert.SerializeObject(_arguments), Columns = _columns, OrderClause = _order, RequestType = _reference, Reference = HttpContext.Current.Request.UserHostAddress, TransactionType = _transaction };
                tbl.Insert(obj);
            }
        }
        public string[] GetColumns(string table)
        {
            ArrayList obj = new ArrayList();
            var items = (new Massive.DynamicModel(Conn).Query(String.Format("SELECT COLUMN_NAME FROM DATA_BASE.information_schema.columns WHERE TABLE_NAME='{0}'", table))).ToList();
            items.ForEach(p =>
            {
                obj.Add(p.COLUMN_NAME);
            });
            return ((IEnumerable)obj).Cast<object>().Select(x => x.ToString()).ToArray();
        }
    }
    public class ClassServices
    {
        public static string Conn = {ConnectionStringName_WebConfig};
        public List<dynamic> Select(string tbl, string condition, string arguments = null)
        {
            var emptyObject = new object[] { };
            var args = (arguments == null ? emptyObject : JsonConvert.DeserializeObject<object[]>(arguments));
            return new Massive.DynamicModel(Conn, tbl).All(where: condition, args: args).ToList();
        }
        public List<dynamic> SelectByGuid(string tbl, string condition, Guid guid)
        {
            var args = guid;
            return new Massive.DynamicModel(Conn, tbl).All(where: condition, args: args).ToList();
        }
        public String Insert(dynamic table, string obj)
        {
            var tbl = new DynamicModel(Conn, table);
            var object_data = JsonConvert.DeserializeObject<object[]>(obj);
            return JsonConvert.SerializeObject(tbl.Insert(object_data));
        }
        public String Update(dynamic table, string condition, string obj)
        {
            var tbl = new DynamicModel(Conn, table);
            var object_data = JsonConvert.DeserializeObject<object[]>(obj);
            var arr_list = new Dictionary<string, string>();
            return JsonConvert.SerializeObject(tbl.Update(object_data, where: condition));
        }
        public String Delete(dynamic table, string condition)
        {
            var tbl = new DynamicModel(Conn, table);
            return JsonConvert.SerializeObject(tbl.Delete(where: condition));
        }
        public String Archive(dynamic table, string key)
        {
            var tbl = new DynamicModel(Conn, table);
            var obj = new { EndDate = DateTime.Now };
            return JsonConvert.SerializeObject(tbl.Update(obj, key));
        }
    }
    #endregion
    public static class Data
    {
        public static DataTable ToDataTable(this List<ExpandoObject> list, string tableName)
        {
            if (list == null || list.Count == 0) return null;
            //build columns
            var props = (IDictionary<string, object>)list[0];
            var t = new DataTable(tableName);
            foreach (var prop in props)
            {
                t.Columns.Add(new DataColumn(prop.Key, prop.Value.GetType()));
            }
            //add rows
            foreach (var row in list)
            {
                var data = t.NewRow();
                foreach (var prop in (IDictionary<string, object>)row)
                {
                    data[prop.Key] = prop.Value;
                }
                t.Rows.Add(data);
            }
            return t;
        }
    }
    public static class Ext
    {
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
    }
}
