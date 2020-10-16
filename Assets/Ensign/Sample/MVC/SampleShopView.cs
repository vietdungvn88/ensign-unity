using Ensign;
using Ensign.Model;
using Ensign.Unity;
using Ensign.Unity.MVC;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ensign.Unity.Sample
{
    public class SampleShopModel : AbstractData
    {
        public Guid instanceId = System.Guid.NewGuid();
        public DataBind<int> money = new DataBind<int>(1000);
        public int intValue = 1;

        public SampleShopModel()
        {
            Log.Info($"Created SampleShopModel {this.ToJsonFormat()}");
        }
    }

    [Resource("ShopViewPrefab")]
    public class SampleShopView : View<SampleShopController, SampleShopModel>
    {
        [SerializeField] Text labelMoney;
        [SerializeField] GUIButton btnPurchase;
        [SerializeField] GUIButton btnClose;

        void Start()
        {
            Model.money.Subscribe((newValue) => BindText(newValue));
            Log.Warning($"==> View Subscribe: {Model.ToJsonFormat()}");

            btnClose.onClick = Hide;
            btnPurchase.onClick = Controller.PurchaseItem;
        }

        void BindText(int money)
        {
            labelMoney.text = "Money: " + money;
        }
    }

    [CacheController]
    public class SampleShopController : Controller<SampleShopController, SampleShopModel>
    {
        protected override void Init()
        {
            Model.money.Subscribe((newValue) => Log.Info(newValue));
            Log.Warning($"==> Controller Subscribe: {Model.ToJsonFormat()}");
        }

        public void PurchaseItem()
        {
            Log.Warning($"==> Controller Dispatch: {Model.ToJsonFormat()}");
            Model.intValue += 5;
            Model.money.Value -= 5;
        }
    }
}
