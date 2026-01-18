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

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Opc.Ua
{
	/// <summary>
	/// The SoftwareCertificate class.
	/// </summary>
	public partial class SoftwareCertificate
	{
        /// <summary>
        /// The SignedSoftwareCertificate that contains the SoftwareCertificate
        /// </summary>
        public X509Certificate2 SignedCertificate
        {
            get { return m_signedCertificate;  } 
            set { m_signedCertificate = value; }
        }

        private X509Certificate2 m_signedCertificate;

        /// <summary>
        /// Validates a software certificate.
        /// </summary>
        public static ServiceResult Validate(
            CertificateValidator validator,
            byte[] signedCertificate, 
            out SoftwareCertificate softwareCertificate)
        {
            softwareCertificate = null;

            // validate the certificate.
            X509Certificate2 certificate = null;

            try
            {
                certificate = CertificateFactory.Create(signedCertificate, true);
                validator.Validate(certificate);
            }
            catch (Exception e)
            {
                return ServiceResult.Create(e, StatusCodes.BadDecodingError, "Could not decode software certificate body.");
            }

            // find the software certficate.
            byte[] encodedData = null;

            foreach (X509Extension extension in certificate.Extensions)
            {
                if (extension.Oid.Value == "0.0.0.0.0")
                {
                    encodedData = extension.RawData;
                    break;
                }
            }

            if (encodedData == null)
            {
                return ServiceResult.Create(StatusCodes.BadCertificateInvalid, "Could not find extension containing the software certficate.");
            }

            try
            {
                MemoryStream istrm = new MemoryStream(encodedData, false);
                DataContractSerializer serializer = new DataContractSerializer(typeof(SoftwareCertificate));
                softwareCertificate = (SoftwareCertificate)serializer.ReadObject(istrm);
                softwareCertificate.SignedCertificate = certificate;
            }
            catch (Exception e)
            {
                return ServiceResult.Create(e, StatusCodes.BadCertificateInvalid, "Certificate does not contain a valid SoftwareCertificate body.");
            }

            // certificate is valid.
            return ServiceResult.Good;
        }
	}
}
