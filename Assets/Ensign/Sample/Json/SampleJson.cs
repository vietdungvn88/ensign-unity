using System;
using System.Collections.Generic;
using UnityEngine;
using Ensign;
using Ensign.Unity.Sample;
using Ensign.Json;
using Ensign.Model.Safe;
using Ensign.Model;
using Ensign.Utils;

namespace Ensign.Unity.Sample
{
    public class SampleJsonSupportModel : AbstractData
    {
        public DataBind<int> bindValue = new DataBind<int>(1000);
        public int intValue;
        public GameObject target;

        public SafeBool boolSafe = true;
        public SafeDecimal decimalSafe;
        public SafeDouble doubleSafe;
        public SafeFloat floatSafe;
        public SafeInt intSafe;
        public SafeLong longSafe;
        public SafeSByte sbyteSafe;
        public SafeShort shortSafe;
        public SafeString stringSafe = "Test String";
        public SafeUInt uintSafe;
        public SafeLong ulongSafe;
        public SafeUShort ushortSafe;

        public Guid Guid { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public EnumType Roles { get; set; }
        public DateTime LastLogin { get; set; }

        public Ray RayInfo { get; set; }
        public Bounds Bound { get; set; }
        public Vector2 Point { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotate { get; set; }
        public Color Tone { get; set; }

        public List<SampleDataInfo> Interests { get; set; }
        public Dictionary<string, string> DictValues { get; set; }

        public enum EnumType
        {
            None = 0,
            Administrator = 1,
            Supermoderator = 2,
            Moderator = 3,
            Member = 4,
            Guest = 5
        }
        public class SampleDataInfo
        {
            public int Id;
            public string Name { get; set; }
        }
    }

    public class SampleJson : MonoBehaviour
    {
        void Start()
        {
            Test();
        }

        void Test()
        {
            SampleJsonSupportModel info = new SampleJsonSupportModel()
            {
                Guid = Guid.NewGuid(),
                UserId = 1,
                UserName = "vietdungvn88",
                Roles = SampleJsonSupportModel.EnumType.Administrator,
                LastLogin = DateTime.Now,
                Point = Vector2.one,
                Position = Vector3.one,
                Rotate = Quaternion.identity,
                Bound = new Bounds(Vector3.zero, new Vector3(200, 500)),
                RayInfo = new Ray(Vector3.zero, Vector3.up),
                Tone = Color.red,
                Interests = new List<SampleJsonSupportModel.SampleDataInfo>() { new SampleJsonSupportModel.SampleDataInfo() { Id = 1, Name = "Sample1" } },
                DictValues = new Dictionary<string, string>() { { "1", "Value1" } }
            };

            string jsonString = JsonUtil.Serialize(info);
            //Log.Info(jsonString.ToJsonFormat());

            info = JsonUtil.Deserialize<SampleJsonSupportModel>(jsonString);

            Ensign.Utils.Utilities.TestExecutionTime("Deserialize: ", () => JsonUtil.Deserialize<SampleJsonSupportModel>(jsonString));
            //Log.Warning(info.ToJsonFormat());
            info.PrintDebug();

            JsonObject obj = JsonUtil.Deserialize(jsonString);
            Log.Info(obj["Point"].ToString());
            Log.Info(obj["Point"]["x"].ToString());
        }
    }
}
