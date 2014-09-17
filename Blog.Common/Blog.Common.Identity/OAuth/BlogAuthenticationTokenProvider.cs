using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Blog.Common.Identity.OAuth
{
    public class BlogAuthenticationTokenProvider : ISecureDataFormat<AuthenticationTicket>
    {
        #region Fields

        private readonly TicketSerializer _serializer;
        private readonly IDataProtector _protector;
        private readonly ITextEncoder _encoder;

        #endregion Fields
        
        #region Constructors

        public BlogAuthenticationTokenProvider(string key)
        {
            _serializer = new TicketSerializer();
            _protector = new AesDataProtectorProvider(key);
            _encoder = TextEncodings.Base64Url;
        }

        #endregion Constructors

        #region ISecureDataFormat<AuthenticationTicket> Members

        public string Protect(AuthenticationTicket ticket)
        {
            var ticketData = _serializer.Serialize(ticket);
            var protectedData = _protector.Protect(ticketData);
            var protectedString = _encoder.Encode(protectedData);
            return protectedString;
        }

        public AuthenticationTicket Unprotect(string text)
        {
            var protectedData = _encoder.Decode(text);
            var ticketData = _protector.Unprotect(protectedData);
            var ticket = _serializer.Deserialize(ticketData);
            return ticket;
        }

        #endregion ISecureDataFormat<AuthenticationTicket> Members
    }

    internal class AesDataProtectorProvider : IDataProtector
    {
        #region Fields

        private readonly byte[] _key;

        #endregion Fields

        #region Constructors

        public AesDataProtectorProvider(string key)
        {
            using (var sha1 = new SHA256Managed())
            {
                _key = sha1.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }

        #endregion Constructors

        #region IDataProtector Methods

        public byte[] Protect(byte[] data)
        {
            byte[] dataHash;
            using (var sha = new SHA256Managed())
            {
                dataHash = sha.ComputeHash(data);
            }

            using (var aesAlg = new AesManaged())
            {
                aesAlg.Key = _key;
                aesAlg.GenerateIV();

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                using (var msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(aesAlg.IV, 0, 16);

                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var bwEncrypt = new BinaryWriter(csEncrypt))
                    {
                        bwEncrypt.Write(dataHash);
                        bwEncrypt.Write(data.Length);
                        bwEncrypt.Write(data);
                    }
                    var protectedData = msEncrypt.ToArray();
                    return protectedData;
                }
            }
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            using (var aesAlg = new AesManaged())
            {
                aesAlg.Key = _key;

                using (var msDecrypt = new MemoryStream(protectedData))
                {
                    var iv = new byte[16];
                    msDecrypt.Read(iv, 0, 16);

                    aesAlg.IV = iv;

                    using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var brDecrypt = new BinaryReader(csDecrypt))
                    {
                        var signature = brDecrypt.ReadBytes(32);
                        var len = brDecrypt.ReadInt32();
                        var data = brDecrypt.ReadBytes(len);

                        byte[] dataHash;
                        using (var sha = new SHA256Managed())
                        {
                            dataHash = sha.ComputeHash(data);
                        }

                        if (!dataHash.SequenceEqual(signature))
                            throw new SecurityException("Signature does not match the computed hash");

                        return data;
                    }
                }
            }
        }

        #endregion IDataProtector Methods
    }
}
