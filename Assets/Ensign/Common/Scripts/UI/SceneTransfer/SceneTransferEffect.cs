using System.Collections;
using UnityEngine.SceneManagement;
using Ensign.Unity.Injection;
using UnityEngine;

namespace Ensign.Unity
{
    public class SceneTransferEffect : IEnsignUnitySceneTranfer
    {
        public IEnumerator OnLoadSceneStart(string fromScene, string toScene, LoadSceneMode mode)
        {
            LoadingView.GetInstance().SetLoading(true).SetText("Changing Screen").Show();
            yield return new WaitForSeconds(0.3f);
        }

        public IEnumerator OnLoadSceneFinish(string fromScene, string toScene, LoadSceneMode mode)
        {
            LoadingView.GetInstance().SetText("Almost done");
            yield return new WaitForSeconds(0.3f);
            LoadingView.GetInstance().SetLoading(false).Hide();
        }
    }
}
