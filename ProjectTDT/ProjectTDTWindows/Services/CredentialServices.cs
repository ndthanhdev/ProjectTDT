using System;
using System.Collections.Generic;
using System.Text;
using Windows.Security.Credentials;

namespace ProjectTDTWindows.Services
{
    public class CredentialServices
    {
        const string ResourceName = "ProjectTDT";
        public static PasswordCredential GetCredentialFromLocker()
        {
            PasswordCredential credential = new PasswordCredential(ResourceName, "User", "Password");
            PasswordVault Vault = new PasswordVault();
            var Credentials = Vault.FindAllByResource(ResourceName);
            if (Credentials.Count > 0)
            {
                credential = Credentials[0];
                if (Credentials.Count > 1)
                    for (int i = 1; i < Credentials.Count; i++)
                        Vault.Remove(Credentials[i]);
            }
            credential.RetrievePassword();
            return credential;
        }
        public static void SetCredential(string UserName, string Password)
        {
            PasswordVault Vault = new PasswordVault();
            var tmp = Vault.FindAllByResource(ResourceName);
            for (int i = 0; i < tmp.Count; i++)
                Vault.Remove(tmp[i]);
            Vault.Add(new PasswordCredential(ResourceName, UserName, Password));

        }
    }
}
