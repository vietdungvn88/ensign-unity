using UnityEngine;
using Ensign;
using Ensign.Unity.MVC;
using Ensign.Unity.Sample;
using System.Collections.Generic;

namespace Ensign.Unity.Sample
{
    public class SampleMainScene : MonoBehaviour
    {
        [SerializeField]
        private GUIButton btnLoadLargeScene;
        [SerializeField]
        private GUIButton btnLoadMVCScene;
        [SerializeField]
        private GUIButton btnLoadLazyListScene;
        [SerializeField]
        private GUIButton btnLoadJsonScene;
        [SerializeField]
        private GUIButton btnLoadAPIScene;

        void Awake()
        {
            btnLoadLargeScene.onClick = () => EsSceneManager.LoadScene(Define.ScreenName.SampleLarge);

            btnLoadMVCScene.onClick = () => EsSceneManager.LoadScene(Define.ScreenName.SampleMVC);

            btnLoadLazyListScene.onClick = () => EsSceneManager.LoadScene(Define.ScreenName.SampleLazyList);

            btnLoadJsonScene.onClick = () => EsSceneManager.LoadScene(Define.ScreenName.SampleJson);

            btnLoadAPIScene.onClick = () => EsSceneManager.LoadScene(Define.ScreenName.SampleAPI);
        }
    }
}