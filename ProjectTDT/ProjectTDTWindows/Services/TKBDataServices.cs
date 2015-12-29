using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ProjectTDTWindows.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.Storage;

namespace ProjectTDTWindows.Services
{
    public class TKBDataServices
    {
        private static string FileName
        {
            get
            {
                string s = CredentialServices.GetCredentialFromLocker().UserName;
                if (s == "User")
                    return null;
                return s;
            }
        }
        public static async Task<IEnumerable<Semester>> Get()
        {
            try
            {                
                List<Semester> result = new List<Semester>();
                if (FileName == null)
                    return result;
                StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, Windows.Storage.CreationCollisionOption.OpenIfExists);
                string source = await FileIO.ReadTextAsync(file);
                var temp = JsonConvert.DeserializeObject<IEnumerable<Semester>>(source);
                if (temp != null) result.AddRange(temp);
                return result;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return new List<Semester>();
            }
        }
        public static async Task Save(IEnumerable<Semester> Data)
        {
            if (FileName != null)
            {
                StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                string source = JsonConvert.SerializeObject(Data, Formatting.Indented);
                await FileIO.WriteTextAsync(file, source);
            }
        }
        public static async Task<Dictionary<string, SemesterInforModel>> GetSemesterDictionary()
        {
            StorageFile file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/SemesterDictionary.txt"));
            string source = await FileIO.ReadTextAsync(file);
            return JsonConvert.DeserializeObject<Dictionary<string, SemesterInforModel>>(source);
        }
        public static async Task Delete()
        {
            var files = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFilesAsync();
            for (int i = 0; i < files.Count; i++)
                await files[i].DeleteAsync();
        }
    }
}

