using UnityEngine;
using UnityEngine.UI;
using Ensign.Unity.Dialog;

namespace Ensign.Unity
{
    [ResourceAttribute("PopupConfirm")]
    public class PopupConfirmView : PopupBaseView<PopupConfirmView, DialogConfirm>
    {
        [SerializeField]
        private Text lblTitle;
        [SerializeField]
        private Text lblContent;
        [SerializeField]
        private GUIButton btnYes;
        [SerializeField]
        private GUIButton btnNo;
        [SerializeField]
        private GUIButton btnClose;

        protected override void SetData()
        {
            if (!string.IsNullOrEmpty(dialog.Title))
                lblTitle.text = dialog.Title;
            lblContent.text = dialog.Content;
            btnNo.SetCaption(dialog.ButtonName(false));
            btnYes.SetCaption(dialog.ButtonName(true));
            btnYes.onClick = OnClickYes;
            btnNo.onClick = OnClickNo;
            btnClose.SetVisible(!dialog.IsRequired);
            btnClose.onClick = OnClickClose;
        }

        void OnClickYes()
        {
            if(!dialog.IsValid())
                return;

            if (dialog.ButtonClick != null)
                dialog.ButtonClick(true);

            Hide();
        }

        void OnClickNo()
        {
            if (dialog.ButtonClick != null)
                dialog.ButtonClick(false);

            Hide();
        }

        void OnClickClose()
        {
            Hide();
        }

        protected override void OnHide()
        {
            if (dialog.ButtonClick != null)
                dialog.ButtonClick(null);
        }
    }
}