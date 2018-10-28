using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Massive.Extensions
{
    [ServiceContract(Name = "MassiveServices", Namespace="http://wehco.com/services")]
    public interface IMassive
    {
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetActiveRoute, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string ActiveData(string table, string where, string arguments = "", string orderby = "1", string columns = "*", string template = "");
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetSimpleRoute, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string MassiveSimple(string table, string where);
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetAllRoute, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        dynamic MassiveAll(string table, string where, string arguments = "", string columns = "*", string orderby = null);
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetQueryRoute, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string MassiveQuery(string query, string table, string access_id);
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetInsertRoute, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string MassiveInsert(string table, string arguments);
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetInsertForm, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string MassiveInsertForm(string table, string arguments);
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetDelete, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string MassiveDelete(string table, string where);
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetUpdateRoute, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string MassiveUpdate(string table, string where, string arguments);
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetArchive, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string MassiveArchive(string table, string where, string arguments);
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetForm, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string MassiveForm(string rform, string table);

        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetColumns, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string[] GetColumns(string table);

        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetTables, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string[] GetTables();

        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.GetViews, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string[] GetViews();
        
        [OperationContract]
        [return: MessageParameter(Name = "d")]
        [WebInvoke(UriTemplate = FlyBase.Routing.LogInsert, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string LogInsert(string table, string where, string arguments, string columns, string order, string transactiontype);
    }
}