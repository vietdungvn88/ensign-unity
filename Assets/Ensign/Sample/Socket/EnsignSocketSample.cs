using Ensign.Unity.Utils;
using Ensign.Unity.Network.Socket;
using System;
using UnityEngine;

namespace Ensign.Unity.Sample
{
    internal class ESSampleSocket : SocketAbstract
    {
        bool isConnected;

        public override bool IsConnected
        {
            get { return isConnected; }
        }

        public override void Connect()
        {
            ModuleHandler.Thread.QueueOnMainThread(() =>
            {
                base.Connect();
                isConnected = true;
                if (!IsConnected)
                    Reconnect();
            });
        }

        public override void OnUpdate()
        {
        }

        public override void Disconnect()
        {
            base.Disconnect();
            isConnected = false;
        }

        public override void Reconnect()
        {
            
        }

        public override void Request(ISocketRequest request)
        {
            ESSocketRequest myRequest = (ESSocketRequest)request;

            UnityEngine.Debug.LogFormat("{2} type: {0}] - {1}\n{3}", myRequest.Resquest.Code.ToString(), JsonUtil.Serialize(myRequest.Resquest.ToParameters()),
                    GetTextColor(UnityEngine.Color.yellow, "Request"), 
                    DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:fff")
            );
        }

        public override void Close()
        {
        }

        void ListenerDelegate(IEventBase evt)
        {
            ISocketResponse response = new ESSocketResponse(evt);

            UnityEngine.Debug.LogFormat("{5}{2} type [{0}] {3} - {1} - {4}", evt.EventType, JsonUtil.Serialize(evt.Data),
                    GetTextColor(UnityEngine.Color.cyan, "Response"), evt.PluginName, (evt.Status ? "" : evt.Error), (evt.IsResponse ? "[FromMe]" : string.Empty));

            DispatchResponse(evt.EventType, response);
        }

        public static string GetTextColor(Color color, string titleColor)
        {
            return "<color=" + ColorUtil.ColorToHex(color) + ">" + titleColor + "</color>";
        }
    }
}
