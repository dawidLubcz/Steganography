using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Lab2_cz2
{
    class CAES
    {
        private string m_sIV = "qwertyuiopasdfgh"; //16 chars - 128 bytes
        private string m_sSalt = "mnbvcxzlkjhgfdsapoiuytrewq123456"; // 32 chars, 256 bytes
        private int m_iKeySize = 32;
        private int m_iIVSize = 16;

        public bool setKey(string a_sKey)
        {
            bool fRetVal = true;
            
           if (a_sKey.Length == m_iKeySize)
               m_sSalt = a_sKey;
           else
               fRetVal = false;

            return fRetVal;
        }

        public bool setKeyAndIV(string a_sIV)
        {
            bool fRetVal = true;

            if (a_sIV.Length == m_iIVSize)
                m_sIV = a_sIV;
            else
                fRetVal = false;

            return fRetVal;
        }

        public string encrypt(string a_sDecrypted, string a_sPassword = null)
        {
            string sRetVal  = null;
            byte[] _aIV     = null;
            byte[] _aKey    = null;

            if (a_sPassword != null)
            {
                Rfc2898DeriveBytes keyGen = new Rfc2898DeriveBytes(a_sPassword, Encoding.ASCII.GetBytes(m_sSalt));
                _aKey = keyGen.GetBytes(m_iKeySize);
                _aIV = keyGen.GetBytes(m_iIVSize);
            }
            else
            {
                _aKey = Encoding.ASCII.GetBytes(m_sSalt);
                _aIV  = Encoding.ASCII.GetBytes(m_sIV);
            }

            byte[] textInBytes = Encoding.ASCII.GetBytes(a_sDecrypted);
            AesCryptoServiceProvider _oAESProv = new AesCryptoServiceProvider();
            _oAESProv.BlockSize = m_iIVSize * 8;
            _oAESProv.KeySize = m_iKeySize * 8;
            _oAESProv.IV = _aIV;
            _oAESProv.Key = _aKey;
            
            ICryptoTransform _oCryptoTransform = _oAESProv.CreateEncryptor();
            byte[] _encrypted = _oCryptoTransform.TransformFinalBlock(textInBytes, 0, textInBytes.Length);
            sRetVal = Convert.ToBase64String(_encrypted);

            return sRetVal;
        }

        public string decrypt(string a_sEncrypted, string a_sPassword = null)
        {
            string sRetVal = null;

            byte[] _aIV = null;
            byte[] _aKey = null;

            if (a_sPassword != null)
            {
                Rfc2898DeriveBytes keyGen = new Rfc2898DeriveBytes(a_sPassword, Encoding.ASCII.GetBytes(m_sSalt));
                _aKey = keyGen.GetBytes(m_iKeySize);
                _aIV = keyGen.GetBytes(m_iIVSize);
            }
            else
            {
                _aKey = Encoding.ASCII.GetBytes(m_sSalt);
                _aIV = Encoding.ASCII.GetBytes(m_sIV);
            }

            try
            {
                byte[] textInBytes = Convert.FromBase64String(a_sEncrypted);
                AesCryptoServiceProvider _oAESProv = new AesCryptoServiceProvider();
                _oAESProv.BlockSize = m_iIVSize * 8;
                _oAESProv.KeySize = m_iKeySize * 8;
                _oAESProv.IV = _aIV;
                _oAESProv.Key = _aKey;

                ICryptoTransform _oCryptoTransform = _oAESProv.CreateDecryptor();
                byte[] _decrypted = _oCryptoTransform.TransformFinalBlock(textInBytes, 0, textInBytes.Length);
                sRetVal = Encoding.ASCII.GetString(_decrypted);
            }
            catch(Exception ex)
            { }           

            return sRetVal;
        }
    }
}
