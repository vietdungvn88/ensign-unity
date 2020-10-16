#if UseFirebaseDB
using System;
using Firebase;
using UnityEngine;
using Firebase.Unity.Editor;
using Firebase.Database;

namespace Ensign.Unity.Firebase
{
    /// <summary>
    /// Clone form RunRunRun code, nonimplement!!!!
    /// </summary>
    public class FbDatabase
    {
        const string FB_DATABASE_URL = "https://xxx.firebaseio.com/";
        const string FB_P12_FILE_NAME = "xxx.p12";
        const string FB_P12_PASSWORD = "notasecret";
        const string FB_SERVICE_ACCOUNT_EMAIL = "xxx@appspot.gserviceaccount.com";

        readonly DatabaseReference _referenceRoot;
        public DatabaseReference Root { get { return _referenceRoot; } }

        public FbDatabase()
        {
            // Set these values before calling into the realtime database.
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(FB_DATABASE_URL);
#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IOS
            FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail(FB_SERVICE_ACCOUNT_EMAIL);
            FirebaseApp.DefaultInstance.SetEditorP12FileName(FB_P12_FILE_NAME);
            FirebaseApp.DefaultInstance.SetEditorP12Password(FB_P12_PASSWORD);
#endif
            _referenceRoot = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public DatabaseReference GetReference(string path, Action<bool, DataSnapshot> callback)
        {
            DatabaseReference _reference = FirebaseDatabase.DefaultInstance.GetReference(path);
            _reference.GetValueAsync().ContinueWith(task =>
            {
                if(task.IsFaulted || task.IsCanceled)
                {
                    callback?.Invoke(false, null);
                }
                else
                {
                    callback?.Invoke(true, task.Result);
                }
            });
            return _reference;
        }
    }
}
#endif