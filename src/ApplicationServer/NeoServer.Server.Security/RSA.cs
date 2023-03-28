using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;

namespace NeoServer.Server.Security;

public static class Rsa
{
    private static AsymmetricCipherKeyPair _asymmetricCipherKeyPair;

    public static byte[] Decrypt(byte[] data)
    {
        try
        {
            var e = new RsaEngine();
            e.Init(false, _asymmetricCipherKeyPair.Private);

            return e.ProcessBlock(data, 0, data.Length); // tamanho do length é grande!!
        }
        catch (Exception _)
        {
            return Array.Empty<byte>();
        }
    }

    public static void LoadPem(string basePath)
    {
        AsymmetricCipherKeyPair keyPair;

        using (var reader = File.OpenText(@$"{basePath}/key.pem"))
        {
            keyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

            _asymmetricCipherKeyPair = keyPair;
        }
    }
}