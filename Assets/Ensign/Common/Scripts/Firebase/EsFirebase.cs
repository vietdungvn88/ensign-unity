#if UseFirebaseDB || UseFirebaseAuth || UseFirebaseRemote || UseFirebaseStorage || UseFirebaseAnalytic
using Ensign.Unity.Firebase;
using Firebase;
using Firebase.Extensions;
#endif
using System;
using Ensign;
using Ensign.Unity.Injection;

namespace Ensign.Unity
{
    /// <summary>
    /// Author: vietdungvn88@gmail.com
    /// Ensign handle Firebase
    /// Please donot edit this class for insert others logic into this class.
    /// </summary>
    public class EsFirebase : IEnsignServiceInjection
    {
        private static EsFirebase _instance;

        public static event Action OnFirebaseInited;

#if UseFirebaseAuth
        FbAuthentication _auth;
        public static FbAuthentication Auth { get { return _instance._auth; } }
#endif

#if UseFirebaseRemote
        FbRemoteConfig _config;
        public static FbRemoteConfig Config {get { return _instance._config; }}
#endif

#if UseFirebaseStorage
        FbStorageFile _storage;
        public static FbStorageFile Storage {get { return _instance._storage; }}
#endif

#if UseFirebaseDB
        FbDatabase _realtimeDb;
        public static FbDatabase Database {get { return _instance._realtimeDb; }}
#endif

        public void OnServiceInit()
        {
            _instance = this;
#if UseFirebaseDB || UseFirebaseAuth || UseFirebaseRemote || UseFirebaseStorage || UseFirebaseAnalytic
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    LoadServices();
                }
                else
                {
                    Log.Error("Init Firebase error: could not resolve all Firebase dependencies " + dependencyStatus);
                }
            });
#endif
        }

        void LoadServices()
        {
#if UseFirebaseAnalytic
            ModuleHandler.Analytic.Register(new FbAnalytic());
#endif

#if UseFirebaseRemote
            _config = new Firebase.FbRemoteConfig();
#endif

#if UseFirebaseAuth
            _auth = new FbAuthentication();
#endif

#if UseFirebaseStorage
            _storage = new FbStorageFile();
#endif

#if UseFirebaseDB
            _realtimeDb = new FbDatabase();
#endif

#if UseFirebaseDB || UseFirebaseAuth || UseFirebaseRemote || UseFirebaseStorage || UseFirebaseAnalytic
            Log.Warning("Successfully initialized Firebase services!");

        #if !UseFirebaseRemote
            OnFirebaseInited?.Invoke();
        #else
            _config.FetchServer((task) => OnFirebaseInited?.Invoke());
        #endif

#endif
        }
    }
}