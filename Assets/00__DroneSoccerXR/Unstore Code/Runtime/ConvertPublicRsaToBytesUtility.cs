﻿using System;
using System.Security.Cryptography;

public class ConvertPublicRsaToBytesUtility {




    public static void ParsePublicRsaKeyToBytesWithModule(string publicRsaKey, out byte[] publicKeyBytes)
    {
        RSAParameters rsaParams;
        using (var rsa = RSA.Create())
        {
            rsa.FromXmlString(publicRsaKey);
            rsaParams = rsa.ExportParameters(false);
        }

        byte[] modulus = rsaParams.Modulus;
        byte[] exponent = rsaParams.Exponent;
        publicKeyBytes = new byte[modulus.Length + exponent.Length];
        Buffer.BlockCopy(modulus, 0, publicKeyBytes, 0, modulus.Length);
        Buffer.BlockCopy(exponent, 0, publicKeyBytes, modulus.Length, exponent.Length);
    }
    public static void ParsePublicRsaKeyToBytesWithoutModule(string publicRsaKey, out byte[] publicKeyBytes)
    {
        RSAParameters rsaParams;
        using (var rsa = RSA.Create())
        {
            rsa.FromXmlString(publicRsaKey);
            rsaParams = rsa.ExportParameters(false);
        }

        
        byte[] exponent = rsaParams.Exponent;
        publicKeyBytes = new byte[ exponent.Length];
        Buffer.BlockCopy(exponent, 0, publicKeyBytes, 0, exponent.Length);

    }

}