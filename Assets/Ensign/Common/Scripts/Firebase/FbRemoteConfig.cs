#if UseFirebaseRemote
using System.Diagnostics;
using System;
using Ensign;
using System.Linq;
using Firebase.Extensions;
using Firebase.InstanceId;
using Firebase.RemoteConfig;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase;
using UnityEngine;

namespace Ensign.Unity.Firebase
{
    public class FbRemoteConfig
    {
        public bool IsFetched { get; private set; }

        public bool IsLoaded { get { return FirebaseRemoteConfig.ActivateFetched(); } }

        public void FetchServer(Action<Task> callback)
        {
            IsFetched = false;
            if(Application.internetReachability == NetworkReachability.NotReachable)
            {
                FetchedFinish(null, callback);
            }
            else
            {
                //Wait timeout when connect to Firebase
                Task.Delay(TimeSpan.FromSeconds(30)).ContinueWithOnMainThread(task => FetchedFinish(task, callback));
                //Request to firebase get DefaultConfig
                FirebaseInstanceId.DefaultInstance.GetTokenAsync().ContinueWith(
                    task =>
                    {
                        if (task.IsCompleted)
                        {
                            Log.Info(string.Format("Instance ID Token: {0}", task.Result));
                            FirebaseRemoteConfig.FetchAsync(TimeSpan.Zero).ContinueWithOnMainThread(taskFetch =>
                            {
                                FetchedFinish(taskFetch, callback);
                                LoadRemoteConfig(taskFetch);
                            });
                        }
                        else
                        {
                            IsFetched = true;
                            Log.Info("Can't get remote config.");
                        }
                    }
                );
            }
        }

        void FetchedFinish(Task task, Action<Task> callback)
        {
            if(!IsFetched)
            {
                IsFetched = true;
                callback?.Invoke(task);
            }
        }

        public void SetDefaultConfig(Dictionary<string, object> defaults)
        {
            if (defaults == null)
                defaults = new Dictionary<string, object>();
            FirebaseRemoteConfig.SetDefaults(defaults);
        }

        public Dictionary<string, string> CurrentConfig
        {
            get
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (string key in FirebaseRemoteConfig.Keys)
                    dict.Add(key, FirebaseRemoteConfig.GetValue(key).StringValue);
                return dict;
            }
        }

        public void LoadConfigAync(Action<bool, Dictionary<string, string>> callback)
        {
            EnsignApplication.Instance.ActionWaitUntil(() => IsFetched, () =>
            {
                callback?.Invoke(IsLoaded, CurrentConfig);
            });
        }

        private void LoadRemoteConfig(Task task)
        {
            Log.Info("Active: " + FirebaseRemoteConfig.ActivateFetched());
            foreach (string key in FirebaseRemoteConfig.Keys)
            {
                Log.Info($"{key}={FirebaseRemoteConfig.GetValue(key).StringValue}");
            }
        }

        public string GetValue(string key)
        {
            return FirebaseRemoteConfig.GetValue(key).StringValue;
        }
        public long GetLong(string key)
        {
            return FirebaseRemoteConfig.GetValue(key).LongValue;
        }
        public bool GetBool(string key)
        {
            return FirebaseRemoteConfig.GetValue(key).BooleanValue;
        }
        public double GetDouble(string key)
        {
            return FirebaseRemoteConfig.GetValue(key).DoubleValue;
        }
        public byte[] GetByteArray(string key)
        {
            return FirebaseRemoteConfig.GetValue(key).ByteArrayValue.ToArray();
        }
    }
}
#endif