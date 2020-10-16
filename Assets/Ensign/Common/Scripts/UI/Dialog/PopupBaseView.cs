using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ensign;
using Ensign.Unity.Dialog;
using Ensign.Unity.MVC;

namespace Ensign.Unity
{
    public abstract class PopupBaseView<T, R> : BaseView where T : PopupBaseView<T, R> where R : IDialogData
    {
        protected R dialog;

        public void Show(R dialog)
        {
            this.dialog = dialog;
            this.SetData();
            this.Show();
        }

        protected abstract void SetData();

        protected new void Hide()
        {
            if (null != this.gameObject)
            {
                if (dialog.OnDestroy != null)
                    dialog.OnDestroy();

                this.OnHide();

                base.Hide();
            }
        }

        protected virtual void OnHide() { }
    }
}