using Ensign.Model;
using Ensign.Unity.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace Ensign.Unity.Sample
{
    public class SampleDataModel : AbstractData
    {
        public DataBind<int> bindValue = new DataBind<int>(1000);
        public int intValue = 0;
    }

    public class SampleView : View<SampleController, SampleDataModel>
    {
        [SerializeField] GUIButton btnMainScene;
        [SerializeField] GUIButton btnLoadOtherView;
        [SerializeField] Text lblText;

        void Awake()
        {
            btnMainScene.onClick = ()=> EsSceneManager.LoadScene(Define.ScreenName.MainScreen);

            btnLoadOtherView.onClick = () =>
            {
                Controller.IncreaseBindValue();

                EsSceneManager.GetOrCreateView<SampleShopView>().Show();
            };

            //Auto bind on value change
            Model.bindValue.Subscribe((value) => BindText(value));
        }

        void BindText(int value)
        {
            lblText.text = $"Value: {value + Model.intValue}";
        }
    }

    public class SampleController : Controller<SampleController, SampleDataModel>
    {
        public void IncreaseBindValue()
        {
            Model.bindValue.Value++;
        }
    }
}
