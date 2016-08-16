using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace XTDT.UWP.Common
{
    public static class SaveAndLoad
    {
        public static async Task SaveTextAsync(string fileName, string text)
        {
            StorageFile file =
                await
                    ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
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

        public static async Task<string> LoadTextAsync(string fileName)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,CreationCollisionOption.OpenIfExists);
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
    }
}
