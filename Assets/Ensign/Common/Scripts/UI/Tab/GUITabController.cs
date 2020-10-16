using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ensign
{
    public class GUITabController : MonoBehaviour
    {
        [SerializeField] GUITabButton[] tabs;

        public DataBind<int> tabIndex = new DataBind<int>(0);
        public string selectedValue;

        GUITabButton tabActive;

        void Awake()
        {
            for (int i = 0; i < tabs.Length;i++)
            {
                tabs[i].OnTabDeactive();
                GUIButton.AddClick<GUIButton>(tabs[i].gameObject, OnTabClicked);
            }

            tabIndex.Subscribe((index) => OnTabChanged(index));
        }

        void OnTabClicked(GameObject obj)
        {
            int index = Array.FindIndex<GUITabButton>(tabs, tab => tab.gameObject == obj);
            tabIndex.Value = index;
        }

        public void SetActiveTab(int index, bool value)
        {
            tabs[index].OnTabDeactive();
            tabs[index].gameObject.SetActive(value);
        }

        void OnTabChanged(int index)
        {
            if(tabActive != null)
                tabActive.OnTabDeactive();

            tabActive = tabs[index];
            selectedValue = tabActive.tabParam;
            tabs[index].OnTabActive();
        }

    }
}