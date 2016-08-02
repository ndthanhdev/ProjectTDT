using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTX.Dependencies;
using Windows.Storage;
using Windows.Storage.Streams;
using TDTX.UWP.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad_UWP))]

namespace TDTX.UWP.Dependencies
{
    public class SaveAndLoad_UWP : ISaveAndLoad
    {
        #region ISaveAndLoad implementation

        public async Task SaveTextAsync(string filename, string text)
        {
            StorageFile file =
                await
                    ApplicationData.Current.LocalFolder.CreateFileAsync(filename,
                        CreationCollisionOption.OpenIfExists);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                stream.SetLength(0);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(text);
                    writer.Flush();
                }
            }
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);

            string text = "";
            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }
            return text;
        }

        public async Task<bool> FileExists(string filename)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                using (var stream = await file.OpenStreamForReadAsync())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        await Task.Yield();
                    }
                }
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        #endregion
    }
}