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

#ifndef _OpcUa_PKIProvider_H_
#define _OpcUa_PKIProvider_H_ 1

#include <opcua_p_pki.h>

OPCUA_BEGIN_EXTERN_C

struct _OpcUa_PKIProvider;
/** 
  @brief Validates a given X509 certificate object.

   Validation:
   - Subject/Issuer
   - Path
   - Certificate Revocation List (CRL)
   - Certificate Trust List (CTL)

  @param pPKI                     [in]  The pki handle.
  @param pCertificate             [in]  The certificate that should be validated. (DER encoded ByteString)
  @param pCertificateStore        [in]  The certificate store that validates the passed in certificate.

  @param pValidationCode          [out] The validation code, that gives information about the validation result.
*/
typedef OpcUa_StatusCode (OpcUa_PKIProvider_PfnValidateCertificate)(  
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_ByteString*           pCertificate,
    OpcUa_Void*                 pCertificateStore,
    OpcUa_Int*                  pValidationCode); /* Validation return code. */

/** 
  @brief Validates a given X509 certificate object.
 
   Validation:
   - Subject/Issuer
   - Path
   - Certificate Revocation List (CRL)
   - Certificate Trust List (CTL)

  @param pPKI                     [in]  The pki handle.
  @param pCertificate             [in]  The certificate that should be validated.(DER encoded ByteString)
  @param pCertificateStore        [in]  The certificate store that validates the passed in certificate.

  @param pValidationCode          [out] The validation code, that gives information about the validation result.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_PKIProvider_ValidateCertificate(
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_ByteString*           pCertificate,
    OpcUa_Void*                 pCertificateStore,
    OpcUa_Int*                  pValidationCode); /* Validation return code. */


/** 
  @brief Creates a certificate store object.

  @param pPKI                         [in]  The pki handle.
  
  @param ppCertificateStore           [out] The handle to the certificate store.
*/
typedef OpcUa_StatusCode (OpcUa_PKIProvider_PfnOpenCertificateStore)(  
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_Void**                ppCertificateStore); /* type depends on store implementation */

/** 
  @brief Creates a certificate store object.

  @param pPKI                         [in]  The PKI handle.
  
  @param ppCertificateStore           [out] The handle to the certificate store.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_PKIProvider_OpenCertificateStore(
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_Void**                ppCertificateStore); /* type depends on store implementation */

/** 
  @brief imports a given certificate into given certificate store.

  @param pPKI                     [in]  The pki handle.
  @param pCertificate             [in]  The certificate that should be imported.
  @param pCertificateStore        [in]  The certificate store that should store the passed in certificate.

  @param pCertificateIndex        [int/out] The index that indicates the store location of the certificate within the certificate store.
*/
typedef OpcUa_StatusCode (OpcUa_PKIProvider_PfnSaveCertificate)(  
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_ByteString*           pCertificate,
    OpcUa_Void*                 pCertificateStore,
    OpcUa_Void*                 pSaveHandle);

/** 
  @brief imports a given certificate into given certificate store.
 
  @param pPKI                     [in]  The PKI handle.
  @param pCertificate             [in]  The certificate that should be imported.
  @param pCertificateStore        [in]  The certificate store that should store the passed in certificate.

  @param pCertificateIndex        [in/out] The index that indicates the store location of the certificate within the certificate store.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_PKIProvider_SaveCertificate(
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_ByteString*           pCertificate,
    OpcUa_Void*                 pCertificateStore,
    OpcUa_Void*                 pSaveHandle);



/** 
  @brief imports a given certificate into given certificate store.
 
  @param pPKI                     [in]  The pki handle.
  @param pCertificate             [in]  The certificate that should be imported.
  @param pCertificateStore        [in]  The certificate store that should store the passed in certificate.

  @param pCertificateIndex        [out] The index that indicates the store location of the certificate within the certificate store.
*/
typedef OpcUa_StatusCode (OpcUa_PKIProvider_PfnLoadCertificate)(  
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_Void*                 pLoadHandle,
    OpcUa_Void*                 pCertificateStore,
    OpcUa_ByteString*           pCertificate);

/** 
  @brief imports a given certificate into given certificate store.
 
  @param pPKI                     [in]  The PKI handle.
  @param pCertificate             [in]  The certificate that should be imported.
  @param pCertificateStore        [in]  The certificate store that should store the passed in certificate.

  @param pCertificateIndex        [out] The index that indicates the store location of the certificate within the certificate store.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_PKIProvider_LoadCertificate(
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_Void*                 pLoadHandle,
    OpcUa_Void*                 pCertificateStore,
    OpcUa_ByteString*           pCertificate);


/** 
  @brief frees a certificate store object.

  @param pProvider             [in]  The crypto provider handle.

  @param pCertificateStore     [out] The certificate store object.
*/
typedef OpcUa_StatusCode (OpcUa_PKIProvider_PfnCloseCertificateStore)(  
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_Void**                ppCertificateStore); /* type depends on store implementation */


/** 
  @brief frees a certificate store object.

  @param pProvider             [in]  The crypto provider handle.

  @param pCertificateStore     [out] The certificate store object.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_PKIProvider_CloseCertificateStore(
    struct _OpcUa_PKIProvider*  pPKI,
    OpcUa_Void**                ppCertificateStore); /* type depends on store implementation */


/** 
  @brief frees a certificate store object.

  @param pProvider             [in]  The crypto provider handle.

  @param pCertificateStore     [out] The certificate store object.
*/
typedef OpcUa_StatusCode (OpcUa_PKIProvider_PfnLoadPrivateKeyFromFile)(
    OpcUa_StringA               privateKeyFile,
    OpcUa_P_FileFormat          fileFormat,
    OpcUa_StringA               password,
    OpcUa_ByteString*           pPrivateKey);

/** 
  @brief frees a certificate store object.

  @param pProvider             [in]  The crypto provider handle.

  @param pCertificateStore     [out] The certificate store object.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_PKIProvider_LoadPrivateKeyFromFile(
    OpcUa_StringA               privateKeyFile,
    OpcUa_P_FileFormat          fileFormat,
    OpcUa_StringA               password,
    OpcUa_ByteString*           pPrivateKey);

typedef struct _OpcUa_PKIProvider
{
    OpcUa_Handle                                 Handle; /* Certificate Store */
    OpcUa_PKIProvider_PfnValidateCertificate*    ValidateCertificate;
    OpcUa_PKIProvider_PfnLoadPrivateKeyFromFile* LoadPrivateKeyFromFile;
    OpcUa_PKIProvider_PfnOpenCertificateStore*   OpenCertificateStore;
    OpcUa_PKIProvider_PfnSaveCertificate*        SaveCertificate;
    OpcUa_PKIProvider_PfnLoadCertificate*        LoadCertificate;
    OpcUa_PKIProvider_PfnCloseCertificateStore*  CloseCertificateStore;
}
OpcUa_PKIProvider;

OPCUA_END_EXTERN_C

#endif
