using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ensign;
using Ensign.Unity.MVC;
using UnityEngine.UI;

namespace Ensign.Unity
{
    [ResourceAttribute("Loading")]
    public class LoadingView : BaseView
    {
        [SerializeField]
        private Texture loadingTexture;
        [SerializeField]
        private GameObject blockInput;
        [SerializeField]
        private Text lblText;
        [SerializeField]
        private float size = 120f;
        [SerializeField]
        private float rotSpeed = 300f;
        [SerializeField]
        private float timeUpdateText = 0.5f;

        public string text = "Loading";

#if UNITY_EDITOR
        [SerializeField]
        private bool isLoading;
#endif
        bool _isLoading;
        private float rotAngle = 0f;

        void Update()
        {
#if UNITY_EDITOR
            SetLoading(isLoading);
#endif

            if (_isLoading)
            {
                rotAngle += rotSpeed * Time.deltaTime;

                if(lblText != null && !string.IsNullOrWhiteSpace(text))
                {
                    countdownUpdateText -= Time.deltaTime;
                    if (countdownUpdateText <= 0)
                    {
                        if (txtExtenstion == ".")
                            txtExtenstion = "..";
                        else if (txtExtenstion == "..")
                            txtExtenstion = "...";
                        else
                            txtExtenstion = ".";
                        
                        countdownUpdateText = timeUpdateText;
                    }
                    lblText.text = text + txtExtenstion;
                }
            }
        }
        float countdownUpdateText = 0;
        string txtExtenstion=string.Empty;

        private void OnGUI()
        {
            if (_isLoading)
            {
                Vector2 pivot = new Vector2(Screen.width / 2, Screen.height / 2);
                GUIUtility.RotateAroundPivot(rotAngle % 360, pivot);
                GUI.DrawTexture(new Rect((Screen.width - size) / 2, (Screen.height - size) / 2, size, size), loadingTexture);
            }
        }

        public LoadingView SetText(string text)
        {
            this.text = text;
            return this;
        }

        public LoadingView SetLoading(bool value)
        {
            if (_isLoading != value)
            {
                lblText.text = "Loading";
#if UNITY_EDITOR
                isLoading = value;
#endif
                SetBlockInput(value);
                _isLoading = value;
            }
            return this;
        }

        public LoadingView SetBlockInput(bool value)
        {
            if (blockInput != null)
                blockInput.SetActive(value);
            return this;
        }

        public static LoadingView GetInstance()
        {
            return EsSceneManager.GetOrCreateView<LoadingView>(null);
        }
    }
}
