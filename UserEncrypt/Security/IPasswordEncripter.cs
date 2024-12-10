using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserEncrypt.Security
{
    public interface IPasswordEncripter
    {
        string Encript(string password, List<byte[]> keys);
        string Decript(string encryptedPassword, List<byte[]> keys);
    }
}