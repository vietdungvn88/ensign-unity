#if UseFirebaseAuth
using Firebase.Auth;

namespace Ensign.Unity.Firebase
{
    public class FbAuthentication
    {
        readonly FirebaseAuth _auth;
        FirebaseUser _user;

        public FirebaseUser User { get { return _user; } }

        public FbAuthentication()
        {
            _auth = FirebaseAuth.DefaultInstance;
            _auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }

        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (_auth.CurrentUser != _user)
            {
                bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
                if (!signedIn && _user != null)
                    Log.Warning("AuthState Signed out " + _user.UserId);

                _user = _auth.CurrentUser;
                if (signedIn)
                    Log.Warning("AuthState Signed in " + _user.UserId);
            }
            else
            {
                SignInWithToken(UnityEngine.SystemInfo.deviceUniqueIdentifier);
            }
        }

        public void SignOut()
        {
            _auth.SignOut();
        }

        public void SignInWithToken(string token)
        {
            _auth.SignInWithCustomTokenAsync(token).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Log.Warning($"SignInWithToken fail: {(task.Exception != null ? task.Exception.Message : task.Status.ToString())}");
                    return;
                }

                FirebaseUser newUser = task.Result;
                Log.Warning($"User signed in successfully: {newUser.DisplayName}/{newUser.UserId}");
            });
        }

        public void LinkUser(Credential credential)
        {
            _auth.CurrentUser.LinkWithCredentialAsync(credential).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                    return;

                FirebaseUser newUser = task.Result;
                Log.Warning($"Link user successfully: {newUser.DisplayName}/{newUser.UserId}");
            });
        }

        public Credential CredentialEmail(string email, string password)
        {
            return EmailAuthProvider.GetCredential(email, password);
        }
        public Credential CredentialFacebook(string accessToken)
        {
            return FacebookAuthProvider.GetCredential(accessToken);
        }
        public Credential CredentialGoogle(string idToken, string accessToken)
        {
            return GoogleAuthProvider.GetCredential(idToken, accessToken);
        }
        public Credential CredentialGithub(string accessToken)
        {
            return GitHubAuthProvider.GetCredential(accessToken);
        }
        public Credential CredentialTwitter(string accessToken, string secret)
        {
            return TwitterAuthProvider.GetCredential(accessToken, secret);
        }
        public Credential CredentialPlayGame(string authCode)
        {
            return PlayGamesAuthProvider.GetCredential(authCode);
        }
        public Credential CredentialApple(string appleIdToken, string rawNonce)
        {
            return OAuthProvider.GetCredential("apple.com", appleIdToken, rawNonce, null);
        }
    }
}
#endif