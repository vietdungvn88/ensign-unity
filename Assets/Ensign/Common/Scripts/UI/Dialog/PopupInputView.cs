using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ensign;
using Ensign.Unity.Dialog;

namespace Ensign.Unity
{
    [ResourceAttribute("PopupInput")]
    public class PopupInputView : PopupBaseView<PopupInputView, DialogInput>
    {
        [SerializeField]
        private Text lblTitle;
        [SerializeField]
        private Text lblLabel;
        [SerializeField]
        private InputField lblInput;
        [SerializeField]
        private GUIButton btnClose;
        [SerializeField]
        private GUIButton btnConfim;

        void Awake()
        {
            lblInput.onValueChanged.AddListener((value) => dialog.Input.Value = value);
        }

        protected override void SetData()
        {
            lblTitle.text = dialog.Title;
            lblLabel.text = dialog.Label;
            lblInput.text = dialog.DefaultValue;
            btnConfim.SetCaption(dialog.ButtonName(true));

            btnClose.onClick = this.ClickClose;
            btnConfim.onClick = this.ClickConfirm;
            btnClose.SetVisible(!dialog.IsRequired);

        }

        void ClickClose()
        {
            if (dialog.ButtonClick != null)
                dialog.ButtonClick(null);

            Hide();
        }

        void ClickConfirm()
        {
            if(!dialog.IsValid())
                return;

            if (dialog.ButtonClick != null)
                dialog.ButtonClick(lblInput.text);

            Hide();
        }
    }
}