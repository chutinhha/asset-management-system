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
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3053.
// 
#pragma warning disable 1591

namespace AssMngSys.WebSrv {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="ASSETSRV")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAssListXmlOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEmpListXmlOperationCompleted;
        
        private System.Threading.SendOrPostCallback ExecuteNoQueryTranOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddAssLogOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::AssMngSys.Properties.Settings.Default.AssMngSys_WebSrv_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event GetAssListXmlCompletedEventHandler GetAssListXmlCompleted;
        
        /// <remarks/>
        public event GetEmpListXmlCompletedEventHandler GetEmpListXmlCompleted;
        
        /// <remarks/>
        public event ExecuteNoQueryTranCompletedEventHandler ExecuteNoQueryTranCompleted;
        
        /// <remarks/>
        public event AddAssLogCompletedEventHandler AddAssLogCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/HelloWorld", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/GetAssListXml", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetAssListXml() {
            object[] results = this.Invoke("GetAssListXml", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetAssListXmlAsync() {
            this.GetAssListXmlAsync(null);
        }
        
        /// <remarks/>
        public void GetAssListXmlAsync(object userState) {
            if ((this.GetAssListXmlOperationCompleted == null)) {
                this.GetAssListXmlOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAssListXmlOperationCompleted);
            }
            this.InvokeAsync("GetAssListXml", new object[0], this.GetAssListXmlOperationCompleted, userState);
        }
        
        private void OnGetAssListXmlOperationCompleted(object arg) {
            if ((this.GetAssListXmlCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAssListXmlCompleted(this, new GetAssListXmlCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/GetEmpListXml", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetEmpListXml() {
            object[] results = this.Invoke("GetEmpListXml", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetEmpListXmlAsync() {
            this.GetEmpListXmlAsync(null);
        }
        
        /// <remarks/>
        public void GetEmpListXmlAsync(object userState) {
            if ((this.GetEmpListXmlOperationCompleted == null)) {
                this.GetEmpListXmlOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEmpListXmlOperationCompleted);
            }
            this.InvokeAsync("GetEmpListXml", new object[0], this.GetEmpListXmlOperationCompleted, userState);
        }
        
        private void OnGetEmpListXmlOperationCompleted(object arg) {
            if ((this.GetEmpListXmlCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEmpListXmlCompleted(this, new GetEmpListXmlCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/ExecuteNoQueryTran", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ExecuteNoQueryTran(string[] SQLStringList) {
            object[] results = this.Invoke("ExecuteNoQueryTran", new object[] {
                        SQLStringList});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ExecuteNoQueryTranAsync(string[] SQLStringList) {
            this.ExecuteNoQueryTranAsync(SQLStringList, null);
        }
        
        /// <remarks/>
        public void ExecuteNoQueryTranAsync(string[] SQLStringList, object userState) {
            if ((this.ExecuteNoQueryTranOperationCompleted == null)) {
                this.ExecuteNoQueryTranOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExecuteNoQueryTranOperationCompleted);
            }
            this.InvokeAsync("ExecuteNoQueryTran", new object[] {
                        SQLStringList}, this.ExecuteNoQueryTranOperationCompleted, userState);
        }
        
        private void OnExecuteNoQueryTranOperationCompleted(object arg) {
            if ((this.ExecuteNoQueryTranCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExecuteNoQueryTranCompleted(this, new ExecuteNoQueryTranCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("ASSETSRV/AddAssLog", RequestNamespace="ASSETSRV", ResponseNamespace="ASSETSRV", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddAssLog(string sTyp, string sDept, string sMan, string sAddr, string sReason, string sCompany, string sUserName, string[] listPid, out string sErr) {
            object[] results = this.Invoke("AddAssLog", new object[] {
                        sTyp,
                        sDept,
                        sMan,
                        sAddr,
                        sReason,
                        sCompany,
                        sUserName,
                        listPid});
            sErr = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddAssLogAsync(string sTyp, string sDept, string sMan, string sAddr, string sReason, string sCompany, string sUserName, string[] listPid) {
            this.AddAssLogAsync(sTyp, sDept, sMan, sAddr, sReason, sCompany, sUserName, listPid, null);
        }
        
        /// <remarks/>
        public void AddAssLogAsync(string sTyp, string sDept, string sMan, string sAddr, string sReason, string sCompany, string sUserName, string[] listPid, object userState) {
            if ((this.AddAssLogOperationCompleted == null)) {
                this.AddAssLogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddAssLogOperationCompleted);
            }
            this.InvokeAsync("AddAssLog", new object[] {
                        sTyp,
                        sDept,
                        sMan,
                        sAddr,
                        sReason,
                        sCompany,
                        sUserName,
                        listPid}, this.AddAssLogOperationCompleted, userState);
        }
        
        private void OnAddAssLogOperationCompleted(object arg) {
            if ((this.AddAssLogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddAssLogCompleted(this, new AddAssLogCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetAssListXmlCompletedEventHandler(object sender, GetAssListXmlCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAssListXmlCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAssListXmlCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetEmpListXmlCompletedEventHandler(object sender, GetEmpListXmlCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEmpListXmlCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEmpListXmlCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void ExecuteNoQueryTranCompletedEventHandler(object sender, ExecuteNoQueryTranCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExecuteNoQueryTranCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExecuteNoQueryTranCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void AddAssLogCompletedEventHandler(object sender, AddAssLogCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddAssLogCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddAssLogCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string sErr {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591