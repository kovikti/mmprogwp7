﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.Phone.ServiceReference, version 3.7.0.0
// 
namespace WPClient.MMProgService {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MyMessageDTO", Namespace="http://schemas.datacontract.org/2004/07/MMProgServiceLib")]
    public partial class MyMessageDTO : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Guid IdField;
        
        private byte[] ImageDataField;
        
        private string OwnerField;
        
        private int RotationField;
        
        private string TextField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] ImageData {
            get {
                return this.ImageDataField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageDataField, value) != true)) {
                    this.ImageDataField = value;
                    this.RaisePropertyChanged("ImageData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Owner {
            get {
                return this.OwnerField;
            }
            set {
                if ((object.ReferenceEquals(this.OwnerField, value) != true)) {
                    this.OwnerField = value;
                    this.RaisePropertyChanged("Owner");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Rotation {
            get {
                return this.RotationField;
            }
            set {
                if ((this.RotationField.Equals(value) != true)) {
                    this.RotationField = value;
                    this.RaisePropertyChanged("Rotation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MMProgService.IMMProgService")]
    public interface IMMProgService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IMMProgService/SendMessageToServer", ReplyAction="http://tempuri.org/IMMProgService/SendMessageToServerResponse")]
        System.IAsyncResult BeginSendMessageToServer(WPClient.MMProgService.MyMessageDTO message, System.AsyncCallback callback, object asyncState);
        
        void EndSendMessageToServer(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMMProgServiceChannel : WPClient.MMProgService.IMMProgService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MMProgServiceClient : System.ServiceModel.ClientBase<WPClient.MMProgService.IMMProgService>, WPClient.MMProgService.IMMProgService {
        
        private BeginOperationDelegate onBeginSendMessageToServerDelegate;
        
        private EndOperationDelegate onEndSendMessageToServerDelegate;
        
        private System.Threading.SendOrPostCallback onSendMessageToServerCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public MMProgServiceClient() {
        }
        
        public MMProgServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MMProgServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MMProgServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MMProgServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> SendMessageToServerCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult WPClient.MMProgService.IMMProgService.BeginSendMessageToServer(WPClient.MMProgService.MyMessageDTO message, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSendMessageToServer(message, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void WPClient.MMProgService.IMMProgService.EndSendMessageToServer(System.IAsyncResult result) {
            base.Channel.EndSendMessageToServer(result);
        }
        
        private System.IAsyncResult OnBeginSendMessageToServer(object[] inValues, System.AsyncCallback callback, object asyncState) {
            WPClient.MMProgService.MyMessageDTO message = ((WPClient.MMProgService.MyMessageDTO)(inValues[0]));
            return ((WPClient.MMProgService.IMMProgService)(this)).BeginSendMessageToServer(message, callback, asyncState);
        }
        
        private object[] OnEndSendMessageToServer(System.IAsyncResult result) {
            ((WPClient.MMProgService.IMMProgService)(this)).EndSendMessageToServer(result);
            return null;
        }
        
        private void OnSendMessageToServerCompleted(object state) {
            if ((this.SendMessageToServerCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SendMessageToServerCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SendMessageToServerAsync(WPClient.MMProgService.MyMessageDTO message) {
            this.SendMessageToServerAsync(message, null);
        }
        
        public void SendMessageToServerAsync(WPClient.MMProgService.MyMessageDTO message, object userState) {
            if ((this.onBeginSendMessageToServerDelegate == null)) {
                this.onBeginSendMessageToServerDelegate = new BeginOperationDelegate(this.OnBeginSendMessageToServer);
            }
            if ((this.onEndSendMessageToServerDelegate == null)) {
                this.onEndSendMessageToServerDelegate = new EndOperationDelegate(this.OnEndSendMessageToServer);
            }
            if ((this.onSendMessageToServerCompletedDelegate == null)) {
                this.onSendMessageToServerCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSendMessageToServerCompleted);
            }
            base.InvokeAsync(this.onBeginSendMessageToServerDelegate, new object[] {
                        message}, this.onEndSendMessageToServerDelegate, this.onSendMessageToServerCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override WPClient.MMProgService.IMMProgService CreateChannel() {
            return new MMProgServiceClientChannel(this);
        }
        
        private class MMProgServiceClientChannel : ChannelBase<WPClient.MMProgService.IMMProgService>, WPClient.MMProgService.IMMProgService {
            
            public MMProgServiceClientChannel(System.ServiceModel.ClientBase<WPClient.MMProgService.IMMProgService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginSendMessageToServer(WPClient.MMProgService.MyMessageDTO message, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = message;
                System.IAsyncResult _result = base.BeginInvoke("SendMessageToServer", _args, callback, asyncState);
                return _result;
            }
            
            public void EndSendMessageToServer(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("SendMessageToServer", _args, result);
            }
        }
    }
}
