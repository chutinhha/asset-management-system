﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.3053.
// 
namespace IrRfidUHFDemo.AssWebSrv {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="ASSETSRV")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public Service() {
            this.Url = "http://10.1.1.52/AssWebSrv/Service.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/HelloWorld", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginHelloWorld(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("HelloWorld", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndHelloWorld(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/GetAssListXml", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetAssListXml() {
            object[] results = this.Invoke("GetAssListXml", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetAssListXml(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetAssListXml", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Xml.XmlNode EndGetAssListXml(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/GetEmpXml", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetEmpXml() {
            object[] results = this.Invoke("GetEmpXml", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetEmpXml(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetEmpXml", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Xml.XmlNode EndGetEmpXml(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/GetAsncLogDataTable", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetAsncLogDataTable(string sClientId, int nMaxId) {
            object[] results = this.Invoke("GetAsncLogDataTable", new object[] {
                        sClientId,
                        nMaxId});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetAsncLogDataTable(string sClientId, int nMaxId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetAsncLogDataTable", new object[] {
                        sClientId,
                        nMaxId}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataTable EndGetAsncLogDataTable(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/GetUserDataTable", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetUserDataTable(string sClientId, int nMaxId) {
            object[] results = this.Invoke("GetUserDataTable", new object[] {
                        sClientId,
                        nMaxId});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetUserDataTable(string sClientId, int nMaxId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetUserDataTable", new object[] {
                        sClientId,
                        nMaxId}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataTable EndGetUserDataTable(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/GetDataTableBySql", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetDataTableBySql(string sSql) {
            object[] results = this.Invoke("GetDataTableBySql", new object[] {
                        sSql});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDataTableBySql(string sSql, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDataTableBySql", new object[] {
                        sSql}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataTable EndGetDataTableBySql(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/GetUserXml", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetUserXml() {
            object[] results = this.Invoke("GetUserXml", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetUserXml(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetUserXml", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Xml.XmlNode EndGetUserXml(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/ExecuteNoQueryTran", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ExecuteNoQueryTran(string[] SQLStringList, out string sErr) {
            object[] results = this.Invoke("ExecuteNoQueryTran", new object[] {
                        SQLStringList});
            sErr = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginExecuteNoQueryTran(string[] SQLStringList, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ExecuteNoQueryTran", new object[] {
                        SQLStringList}, callback, asyncState);
        }
        
        /// <remarks/>
        public bool EndExecuteNoQueryTran(System.IAsyncResult asyncResult, out string sErr) {
            object[] results = this.EndInvoke(asyncResult);
            sErr = ((string)(results[1]));
            return ((bool)(results[0]));
        }
    }
}
