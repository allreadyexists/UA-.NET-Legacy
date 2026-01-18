/*
* Copyright (c) 2005-2026 - OPC Foundation
* 
* All Rights Reserved.
* 
* NOTICE:  All information contained herein is, and remains the property of 
* OPC Foundation. The intellectual and technical concepts contained 
* herein are proprietary to OPC Foundation and may be covered by 
* U.S. and Foreign Patents, patents in process, and are protected by trade secret 
* or copyright law. Dissemination of this information or reproduction of this 
* material is strictly forbidden unless prior written permission is obtained 
* from OPC Foundation.
*/

using Opc.Ua.Security.Certificates.Common;
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua.Security.Certificates
{
    /// <summary>
    /// Describes the three required fields of a X509 Certificate and CRL.
    /// </summary>
    public class X509Signature
    {
        /// <summary>
        /// The field contains the ASN.1 data to be signed.
        /// </summary>
        public byte[] Tbs { get; private set; }
        /// <summary>
        /// The signature of the data.
        /// </summary>
        public byte[] Signature { get; private set; }
        /// <summary>
        /// The encoded signature algorithm that was used for signing.
        /// </summary>
        public byte[] SignatureAlgorithmIdentifier { get; private set; }
        /// <summary>
        /// The signature algorithm as Oid string.
        /// </summary>
        public string SignatureAlgorithm { get; private set; }
        /// <summary>
        /// The hash algorithm used for signing.
        /// </summary>
        public HashAlgorithmName Name { get; private set; }
        /// <summary>
        /// Initialize and decode the sequence with binary ASN.1 encoded CRL or certificate.
        /// </summary>
        /// <param name="signedBlob"></param>
        public X509Signature(byte[] signedBlob)
        {
            Decode(signedBlob);
        }

        /// <summary>
        /// Decoder for the signature sequence.
        /// </summary>
        /// <param name="crl">The encoded CRL or certificate sequence.</param>
        private void Decode(byte[] crl)
        {
            int bufferSize = crl.Length;
            IntPtr pBuffer = Marshal.AllocHGlobal(bufferSize);

            try
            {
                
                Marshal.Copy(crl, 0, pBuffer, bufferSize);

                Win32.CERT_SIGNED_CONTENT_INFO signedCrl = Win32.Decode_CERT_SIGNED_CONTENT_INFO(pBuffer, crl.Length);

                if (signedCrl.ToBeSigned.pbData != IntPtr.Zero)
                {
                    // Tbs encoded data
                    Tbs = new byte[signedCrl.ToBeSigned.cbData];
                    Marshal.Copy(signedCrl.ToBeSigned.pbData, Tbs, 0, signedCrl.ToBeSigned.cbData);

                    // Signature Algorithm Identifier
                    SignatureAlgorithm = signedCrl.SignatureAlgorithm.pszObjId;
                    Name = Oids.GetHashAlgorithmName(SignatureAlgorithm);

                    //Signature
                    Signature = new byte[signedCrl.Signature.cbData];
                    Marshal.Copy(signedCrl.Signature.pbData, Signature, 0, signedCrl.Signature.cbData);

                    return;
                }

                throw new CryptographicException("No valid data in the X509 signature.");
            }
            catch (CryptographicException ace)
            {
                throw new CryptographicException("Failed to decode the X509 signature.", ace);
            }
            finally
            {
                if (pBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pBuffer);
                }
            }
        }

        /// <summary>
        /// Verify the signature with the public key of the signer.
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns>true if the signature is valid.</returns>
        public bool Verify(X509Certificate2 certificate)
        {
            switch (SignatureAlgorithm)
            {
                case Oids.RsaPkcs1Sha1:
                case Oids.RsaPkcs1Sha256:
                case Oids.RsaPkcs1Sha384:
                case Oids.RsaPkcs1Sha512:
                case Oids.ECDsaWithSha1:
                case Oids.ECDsaWithSha256:
                case Oids.ECDsaWithSha384:
                case Oids.ECDsaWithSha512:
                    return VerifySignature(certificate);

                default:
                    throw new CryptographicException("Failed to verify signature due to unknown signature algorithm.");
            }
        }

        /// <summary>
        /// Verifies the signature on the CRL or certificate.
        /// </summary>
        private bool VerifySignature(X509Certificate2 certificate)
        {
         
            byte[] certBytes = certificate.GetRawCertData();
            int bufferSize = certBytes.Length;
            IntPtr pBuffer = Marshal.AllocHGlobal(bufferSize);
            Marshal.Copy(certBytes, 0, pBuffer, bufferSize);

            try
            {
                Win32.CERT_CONTEXT context = (Win32.CERT_CONTEXT)Marshal.PtrToStructure(certificate.Handle, typeof(Win32.CERT_CONTEXT));
                Win32.CERT_INFO info = (Win32.CERT_INFO)Marshal.PtrToStructure(context.pCertInfo, typeof(Win32.CERT_INFO));

                int bResult = Win32.CryptVerifyCertificateSignature(
                    IntPtr.Zero,
                    Win32.X509_ASN_ENCODING,
                    pBuffer,
                    bufferSize,
                    ref info.SubjectPublicKeyInfo);

                if (bResult == 0)
                {
                    throw new CryptographicException("Failed to verify signature due to unknown signature algorithm.");
                }
            }
            finally
            {
                if (pBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pBuffer);
                }
            }
            return true;
        }
    }
}
