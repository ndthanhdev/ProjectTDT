using ProjectTDTUniversal.Common;
using ProjectTDTUniversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace ProjectTDTUniversal.Services.DataServices
{
    public partial class  Transporter
    {

        /// <summary>
        /// Login and set user's Name
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Login()
        {
            PasswordCredential account = CredentialsService.GetCredential();
            await Transport(TemplatesForm.Login, account.UserName, account.Password);

            string userFullName = StringHelper.RegexString(HttpRepository.Content, TemplatesRegexPatterns.GetUserFullName);

            var ss = StringHelper.RegexStrings(userFullName, TemplatesRegexPatterns.GetWord);
            userFullName = string.Join(" ", ss);

            SettingsServices.SettingsService.Instance.UserName = userFullName;
            return !string.IsNullOrEmpty(SettingsServices.SettingsService.Instance.UserName);
        }

        public async Task<IEnumerable<Notify>> GetNotify()
        {
            await Transport(TemplatesForm.GetNotify);
            List<string> ss = new List<string>(StringHelper.RegexStrings(HttpRepository.Content, TemplatesRegexPatterns.GetNotify));
            return null;
        }

    }
}
