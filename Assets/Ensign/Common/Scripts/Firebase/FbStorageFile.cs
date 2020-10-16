#if UseFirebaseStorage
using System.Net;
using System.IO;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Storage;

namespace Ensign.Unity.Firebase
{
    public class FbStorageFile
    {
        private const string STORAGE_URL = "gs://xxx.appspot.com/";
        readonly FirebaseStorage _storage;

        public FbStorageFile()
        {
            _storage = FirebaseStorage.DefaultInstance;
        }

        /// <summary>
        /// Upload json file to Firebase
        /// </summary>
        /// <param name="name">"{User.UserId}"</param>
        /// <param name="jsonData">Json data upload</param>
        public void UploadFileFromMemory(string name, string jsonData)
        {
            if(name.Contains("."))
                throw new Exception("Name without extension file!");

            string fileName = $"{name}.json";
            // Create a storage reference from our storage service
            StorageReference storage_ref = _storage.GetReferenceFromUrl(STORAGE_URL);
            byte[] custom_bytes = Encoding.ASCII.GetBytes(jsonData);

            // Create a reference to the file you want to upload
            StorageReference test_ref = storage_ref.Child(fileName);

            // Create file metadata including the content type
            var new_metadata = new MetadataChange
            {
                ContentType = "application/json",
                ContentEncoding = "UTF-8"
            };

            // Upload the file to the path "images/rivers.jpg"
            test_ref.PutBytesAsync(custom_bytes, new_metadata, null, CancellationToken.None, null)
                .ContinueWith((Task<StorageMetadata> task) =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                        Log.Error(task.Exception.ToString());
                    else
                    {
                        // Metadata contains file metadata such as size, content-type, and download URL.
                        StorageMetadata metadata = task.Result;
                        string download_url = test_ref.GetDownloadUrlAsync().Result.ToString();
                        Log.Info("Uploaded to firebase: " + download_url);
                    }
                });
        }

        /// <summary>
        /// Download json content from Firebase
        /// </summary>
        /// <param name="name">File Name without extension</param>
        /// <param name="contentFile">Cotnent to storage</param>
        public void DownloadStorageFile(string name, Action<string> contentFile)
        {
            if(name.Contains("."))
                throw new Exception("Name without extension file!");

            string fileName = $"{name}.json";
            // Create a reference with an initial file path and name
            StorageReference path_reference = _storage.GetReference(fileName);
            
            path_reference.GetDownloadUrlAsync().ContinueWith((Task<Uri> task) =>
            {
                string url = string.Empty;

                if (!task.IsFaulted && !task.IsCanceled)
                {
                    url = task.Result.ToString();

                    ModuleHandler.Thread.RunAsync(() =>
                    {
                        string jsonString = new WebClient().DownloadString(url);
                        ModuleHandler.Thread.QueueOnMainThread(() =>
                        {
                            contentFile?.Invoke(jsonString);
                            Log.Info($"Download: {url}\n{jsonString}");
                        });
                    });
                }
            });
        }
    }
}
#endif