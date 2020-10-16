using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ensign
{
    public class GUITabButton : MonoBehaviour
    {
        [SerializeField] private Color colorSelected;
        [SerializeField] private Color colorNonSelect;
        [SerializeField] private Transform tabTransform;
        public string tabParam;

        public Text Label { get  {  return GetComponentInChildren<Text>(); } }

        public void OnTabActive()
        {
            Label.fontStyle = FontStyle.Bold;
            tabTransform.gameObject.SetActive(true);
            GetComponent<Image>().color = colorSelected;
        }

        public void OnTabDeactive()
        {
            Label.fontStyle = FontStyle.Normal;
            tabTransform.gameObject.SetActive(false);
            GetComponent<Image>().color = colorNonSelect;
        }
    }
}