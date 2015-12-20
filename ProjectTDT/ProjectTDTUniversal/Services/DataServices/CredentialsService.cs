using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace ProjectTDTUniversal.Services.DataServices
{
    /// <summary>
    /// Only one or not account is stored on credential vault
    /// </summary>
    public static class CredentialsService
    {
        const string ResourceName = "ProjectTDT";
        public static PasswordCredential GetCredential()
        {
            PasswordCredential result = null;
            try
            {
                PasswordVault Vault = new PasswordVault();
                foreach (PasswordCredential pc in Vault.FindAllByResource(ResourceName))
                {
                    if (result == null)
                        result = pc;
                    else
                        Vault.Remove(pc);
                }
                result.RetrievePassword();
            }
            catch// (Exception ex)
            {
                result = new PasswordCredential(ResourceName, "MSSV", "MK");               
            }
            return result;
        }
        public static bool SetCredential(string UserName, string Password)
        {
            PasswordVault Vault = new PasswordVault();
            PasswordCredential value=new PasswordCredential(ResourceName,"MSSV","MK");
            try
            {
                value = new PasswordCredential(ResourceName, UserName, Password);
                foreach (PasswordCredential pc in Vault.FindAllByResource(ResourceName))                   
                        Vault.Remove(pc);                
            }
            catch(ArgumentException)
            {
                return false;
            }            
            finally
            {
                Vault.Add(value);
            }
            return true;
        }
    }
}
