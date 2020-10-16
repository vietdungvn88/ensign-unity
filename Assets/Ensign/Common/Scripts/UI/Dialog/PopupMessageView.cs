using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ensign;
using Ensign.Unity.Dialog;

namespace Ensign.Unity
{
    [ResourceAttribute("PopupMessage")]
    public sealed class PopupMessageView : PopupBaseView<PopupMessageView, DialogMessage>
    {
        [SerializeField]
        private Text lblTitle;
        [SerializeField]
        private Text lblContent;
        [SerializeField]
        private GUIButton btnClose;

        protected override void SetData()
        {
            if (!string.IsNullOrEmpty(dialog.Title))
                lblTitle.text = dialog.Title;
            lblContent.text = dialog.Content;
            btnClose.SetCaption(dialog.ButtonName(0));
            btnClose.onClick = ClickClose;
        }

        void ClickClose()
        {
            if (dialog.ButtonClick != null)
                dialog.ButtonClick(null);

            Hide();
        }

        protected override void OnHide()
        {
            if (dialog.OnDestroy != null)
                dialog.OnDestroy();
        }
    }
}