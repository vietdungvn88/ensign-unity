using Ensign.Json;
using Ensign.Utils;
using Ensign.Model;
using Ensign.Model.Safe;
using Ensign.Unity.MVC;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ensign.Unity.Sample
{
    public enum EnumSampleType
    {
        None = 0,
        Administrator = 1,
        Supermoderator = 2,
        Moderator = 3,
        Member = 4,
        Guest = 5
    }

    [Serializable]
    public class ModelPrimitiveType : AbstractData
    {
        public int intValue = 5;
        public double doubleValue = 99999;
        public string stringValue = "Test String";
    }

    [Serializable]
    public class ModelListDictType : AbstractData//SerializableData
    {
        public List<ModelPrimitiveType> listValue = new List<ModelPrimitiveType>() { new ModelPrimitiveType(), new ModelPrimitiveType(), new ModelPrimitiveType() };
        public Dictionary<string, string> dictValues = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }, { "key3", "value3" } };

        public ModelListDictType()
        {
            for(int i=0;i<1000;i++)
            {
                listValue.Add(new ModelPrimitiveType());
                dictValues.Add(i.ToString(), "");
            }
        }
    }
    public class ModelDateEnumType : AbstractData
    {
        public DateTime dateTime = new DateTime(2020, 06, 13);
        //public DateTimeOffset dateTimeOffset = new DateTime(2020, 06, 13);
        public Guid Guid = Guid.NewGuid();
        public EnumSampleType enumValue = EnumSampleType.Administrator;
    }
    public class ModelDataBindType : AbstractData
    {
        public DataBind<int> intBind = new DataBind<int>(500);
        public DataBind<string> stringBind = new DataBind<string>("Test String");
        public DataBind<ModelPrimitiveType> modelBind = new DataBind<ModelPrimitiveType>(new ModelPrimitiveType());
    }
    public class ModelSafeType : AbstractData
    {
        public SafeBool boolSafe = true;
        public SafeDecimal decimalSafe;
        public SafeDouble doubleSafe;
        public SafeFloat floatSafe;
        public SafeInt intSafe;
        public SafeLong longSafe;
        public SafeSByte sbyteSafe;
        public SafeShort shortSafe;
        public SafeString stringSafe = "This is Safe String";
        public SafeUInt uintSafe;
        public SafeLong ulongSafe;
        public SafeUShort ushortSafe;
    }
    public class ModelUnityType : AbstractData
    {
        public Ray RayInfo;
        public Bounds Bound;
        public Vector2 Point;
        public Vector3 Position;
        public Quaternion Rotate;
        public Color Tone;
    }


    public class SampleJsonView : MonoBehaviour
    {
        [SerializeField]
        private Text lblTime;
        [SerializeField]
        private Text lblOutput;

        [SerializeField]
        private GUIButton btnSerializeUnityJson;
        [SerializeField]
        private GUIButton btnSerializeMiniJson;
        [SerializeField]
        private GUIButton btnSerializeLitJson;
        [SerializeField]
        private GUIButton btnSerializeEnsignJson;

        [SerializeField]
        private GUIButton btnDeserializeUnityJson;
        [SerializeField]
        private GUIButton btnDeserializeMiniJson;
        [SerializeField]
        private GUIButton btnDeserializeLitJson;
        [SerializeField]
        private GUIButton btnDeserializeEnsignJson;

        [SerializeField] Toggle togglePrimitive;
        [SerializeField] Toggle toggleListDict;
        [SerializeField] Toggle toggleDateTime;
        [SerializeField] Toggle toggleDataBind;
        [SerializeField] Toggle toggleSafeType;
        [SerializeField] Toggle toggleUnityEngine;

        [SerializeField] Toggle toggleOneTime;
        [SerializeField] Toggle toggleHundred;
        [SerializeField] Toggle toggleThousand;
        [SerializeField] Toggle toggleMillion;

        [SerializeField]
        private GUIButton btnBackScene;


        IDataModel model = new ModelPrimitiveType();
        string jsonString = "{\"intValue\":5,\"doubleValue\":1.79769313486232E+303,\"stringValue\":\"Test String\"}";
        int numberTimes = 1;

        void Start()
        {
            btnBackScene.onClick = () => EsSceneManager.LoadScenePrev();

            togglePrimitive.onValueChanged.AddListener((isChecked) => { if (isChecked) { model = new ModelPrimitiveType(); jsonString = model.ToJson(); } });
            toggleListDict.onValueChanged.AddListener((isChecked) => { if (isChecked) { model = new ModelListDictType(); jsonString = model.ToJson(); } });
            toggleDateTime.onValueChanged.AddListener((isChecked) => { if (isChecked) { model = new ModelDateEnumType(); jsonString = model.ToJson(); } });
            toggleDataBind.onValueChanged.AddListener((isChecked) => { if (isChecked) { model = new ModelDataBindType(); jsonString = model.ToJson(); } });
            toggleUnityEngine.onValueChanged.AddListener((isChecked) => { if (isChecked) { model = new ModelUnityType(); jsonString = model.ToJson(); } });
            toggleSafeType.onValueChanged.AddListener((isChecked) => { if (isChecked) { model = new ModelSafeType(); jsonString = model.ToJson(); } });

            toggleOneTime.onValueChanged.AddListener((isChecked) => { if (isChecked) { numberTimes = 1; } });
            toggleHundred.onValueChanged.AddListener((isChecked) => { if (isChecked) { numberTimes = 100; } });
            toggleThousand.onValueChanged.AddListener((isChecked) => { if (isChecked) { numberTimes = 10000; } });
            toggleMillion.onValueChanged.AddListener((isChecked) => { if (isChecked) { numberTimes = 1000000; } });


            btnSerializeUnityJson.onClick = () =>
            {
                TimeSpan timeSpan;
                object text = null;
                try
                {
                    timeSpan = Ensign.Utils.Utilities.ExecutionTime(() =>
                    {
                        for (int i = 0; i < numberTimes; i++)
                            text = JsonUtility.ToJson(model);
                    });
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                }
                finally
                {
                    Bind(timeSpan, text);
                }
            };

            btnSerializeMiniJson.onClick = () =>
            {
                TimeSpan timeSpan;
                object text = null;
                try
                {
                    timeSpan = Ensign.Utils.Utilities.ExecutionTime(() =>
                    {
                        for (int i = 0; i < numberTimes; i++)
                            text = ThirdParty.MiniJSON.Json.Serialize(model);
                    });
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                }
                finally
                {
                    Bind(timeSpan, text);
                }
            };

            btnSerializeLitJson.onClick = () =>
            {
                TimeSpan timeSpan;
                object text = null;
                try
                {
                    timeSpan = Ensign.Utils.Utilities.ExecutionTime(() =>
                    {
                        for (int i = 0; i < numberTimes; i++)
                            text = ThirdParty.LitJson.JsonMapper.ToJson(model);
                    });
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                }
                finally
                {
                    Bind(timeSpan, text);
                }
            };

            btnSerializeEnsignJson.onClick = () =>
            {
                TimeSpan timeSpan;
                object text = null;
                try
                {
                    JsonOption parameters = new JsonOption()
                    {
                        IsIncludeProperty = false,
                        UseUnicode = false
                    };
                    parameters.IgnoreAttributes.Clear();

                    timeSpan = Ensign.Utils.Utilities.ExecutionTime(() =>
                    {
                        for (int i = 0; i < numberTimes; i++)
                            text = JsonUtil.Serialize(model, parameters);
                    });
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                }
                finally
                {
                    Bind(timeSpan, text);
                }
            };


            btnDeserializeUnityJson.onClick = () =>
            {
                TimeSpan timeSpan;
                object text = null;
                try
                {
                    timeSpan = Ensign.Utils.Utilities.ExecutionTime(() =>
                    {
                        for (int i = 0; i < numberTimes; i++)
                            JsonUtility.FromJson(jsonString, model.GetType());
                    });
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                }
                finally
                {
                    Bind(timeSpan, text);
                }
            };

            btnDeserializeMiniJson.onClick = () =>
            {
                TimeSpan timeSpan;
                object text = null;
                try
                {
                    timeSpan = Ensign.Utils.Utilities.ExecutionTime(() =>
                    {
                        for (int i = 0; i < numberTimes; i++)
                            ThirdParty.MiniJSON.Json.Deserialize(jsonString);
                    });
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                }
                finally
                {
                    Bind(timeSpan, text);
                }
            };

            btnDeserializeLitJson.onClick = () =>
            {
                TimeSpan timeSpan;
                object text = null;
                try
                {
                    timeSpan = Ensign.Utils.Utilities.ExecutionTime(() =>
                    {
                        for (int i = 0; i < numberTimes; i++)
                            ThirdParty.LitJson.JsonMapper.ToObject(jsonString, model.GetType());
                    });
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                }
                finally
                {
                    Bind(timeSpan, text);
                }
            };

            btnDeserializeEnsignJson.onClick = () =>
            {
                TimeSpan timeSpan;
                object text = null;
                try
                {
                    timeSpan = Ensign.Utils.Utilities.ExecutionTime(() =>
                    {
                        for (int i = 0; i < numberTimes; i++)
                            JsonUtil.Deserialize(jsonString);
                    });
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                }
                finally
                {
                    Bind(timeSpan, text);
                }
            };

            Compress();
        }

        void Bind(TimeSpan timeSpan, object output)
        {
            if (timeSpan.TotalMinutes > 1)
                lblTime.text = $"Exec: {timeSpan.TotalMinutes:0.000}minute";
            else if (timeSpan.TotalSeconds > 1)
                lblTime.text = $"Exec: {timeSpan.TotalSeconds:0.000}s";
            else
                lblTime.text = $"Exec: {timeSpan.TotalMilliseconds:0.000}ms";

            if (output != null)
            {
                Log.Info($"{lblTime.text}\n{output}");
                lblOutput.text = output.ToJsonFormat();
            }
        }

        void Compress()
        {
            TimeSpan cal;
            byte[] dataByte = new byte[0];
            ModelListDictType data = new ModelListDictType();
            string testString = data.ToJson();
            ModelListDictType decompressModel = null;

            #region Compress Any Object into bytes
            cal = Ensign.Utils.Utilities.ExecutionTime(() => dataByte = System.Text.Encoding.ASCII.GetBytes(testString));
            Log.Info($"Normal: {dataByte.Length} in {cal.TotalMilliseconds:0.000}ms");
            
            cal = Ensign.Utils.Utilities.ExecutionTime(() => dataByte = data.Compress(CompressType.Gzip));
            Log.Info($"Compress Gzip: {dataByte.Length} in {cal.TotalMilliseconds:0.000}ms");

            cal = Ensign.Utils.Utilities.ExecutionTime(() => decompressModel = dataByte.DecompressToModel<ModelListDictType>(CompressType.Gzip));
            Log.Info($"Decompress Gzip in {cal.TotalMilliseconds:0.000}ms\n{decompressModel.ToJsonFormat()}");

            cal = Ensign.Utils.Utilities.ExecutionTime(() => dataByte = data.Compress(CompressType.SevenZip, compressLevel: 10));
            Log.Info($"Compress 7Zip: {dataByte.Length} in {cal.TotalMilliseconds:0.000}ms");

            cal = Ensign.Utils.Utilities.ExecutionTime(() => decompressModel = dataByte.DecompressToModel<ModelListDictType>(CompressType.SevenZip));
            Log.Info($"Decompress 7Zip in {cal.TotalMilliseconds:0.000}ms\n{decompressModel.ToJsonFormat()}");

            #endregion
            JsonObject jsonObject = JsonUtil.Deserialize(testString);
            Log.Info(jsonObject["listValue"][0]);
            Log.Info(jsonObject["listValue"][1]["stringValue"]);
            Log.Info(jsonObject["listValue"][1]["intValue"].TryGetValue<int>());
            Log.Info(jsonObject["dictValues"]["key1"]);
        }
    }
}