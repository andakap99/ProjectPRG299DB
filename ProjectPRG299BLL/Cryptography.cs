using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ProjectPRG299BLL
{
    public class Cryptography
    {
        public enum CryptMethod { ENCRYPT, DECRYPT }
        public enum CryptClass { AES, RC2, RIJ, DES, TDES }
        public class Generic
        {
            public enum CryptMethod {  ENCRYPT, DECRYPT  }
            public enum CryptClass {  AES, RC2, RIJ, DES, TDES }
            public object Crypt(CryptMethod _method, CryptClass _class, object _input, string _key)
            {
                SymmetricAlgorithm control;
                switch(_class)
                {
                    case CryptClass.AES:
                        control = new AesManaged();
                        break;
                    case CryptClass.RC2:
                        control = new RC2CryptoServiceProvider();
                        break;
                    case CryptClass.RIJ:
                        control = new RijndaelManaged();
                        break;
                    case CryptClass.DES:
                        control = new DESCryptoServiceProvider();
                        break;
                    case CryptClass.TDES:
                        control = new TripleDESCryptoServiceProvider();
                        break;
                    default:
                        return false;
                        

                }

                control.Key = UTF8Encoding.UTF8.GetBytes(_key);
                control.Padding = PaddingMode.PKCS7;
                control.Mode = CipherMode.ECB;

                ICryptoTransform cTransform = null;
                byte[] resultArray;

                if(_method == CryptMethod.ENCRYPT)
                {
                    cTransform = control.CreateEncryptor();
                }
                else if(_method == CryptMethod.DECRYPT)
                {
                    cTransform = control.CreateDecryptor();
                }

                if (_input is string)
                {
                    byte[] inputArray = UTF32Encoding.UTF8.GetBytes(_input as string);
                    resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                    control.Clear();
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
                else if (_input is byte[])
                {
                    resultArray = cTransform.TransformFinalBlock((_input as byte[]), 0, (_input as byte[]).Length);
                    control.Clear();
                    return resultArray;
                }
                return false;
            }
        }
    }
}
