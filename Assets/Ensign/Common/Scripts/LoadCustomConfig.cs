using Ensign.Unity.Dialog;
using Ensign.Unity.Injection;
using Ensign.Unity.Network.Socket;
using Ensign.Unity.MVC;
using System;

namespace Ensign.Unity
{
    public class LoadCustomConfig : IEnsignUnityProject
    {
        ISocket _socket = null;

        public ISocket Socket
        {
            get
            {
                // if (_socket == null)
                //     _socket = new ESSampleSocket();
                return _socket;
            }
        }

        public Type AttachUIButton()
        {
            return typeof(GUIButton);
        }

        public void OnLoadAppConfig()
        {
            // CustomTypes.Register();
            // SceneHandler.Instance.Load();
        }

        public void ShowDialog(IDialogData dialog)
        {
            ModuleHandler.Thread.QueueOnMainThread(() =>
            {
                if (dialog is DialogConfirm)
                {
                    EsSceneManager.GetOrCreateView<PopupConfirmView>(null).Show(dialog as DialogConfirm);
                }
                else if (dialog is DialogInput)
                {
                    EsSceneManager.GetOrCreateView<PopupInputView>(null).Show(dialog as DialogInput);
                }
                else
                {
                    EsSceneManager.GetOrCreateView<PopupMessageView>(null).Show(dialog as DialogMessage);
                }
            });
        }
    }
}
