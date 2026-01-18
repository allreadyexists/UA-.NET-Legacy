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

#ifndef _OpcUa_Credentials_H_
#define _OpcUa_Credentials_H_ 1

OPCUA_BEGIN_EXTERN_C

/*============================================================================
 * OpcUa_UserNameCredential
 *
 * A username/password credential.
 *
 * Name     - the name of the user.
 * Password - the password (could be hashed). 
 *===========================================================================*/
typedef struct _OpcUa_UserNameCredential
{
    OpcUa_String Name;
    OpcUa_String Password;
}
OpcUa_UserNameCredential;

/*============================================================================
 * OpcUa_X509Credentials
 *
 * An X509 certificate credential.
 *
 * Subject       - the subject of the certificate.
 * Thumbprint    - the thumbprint of the certificate.
 * Password      - the password required to access the private key.
 * StoreLocation - the location of the certificate store.
 * StoreName     - the name of the certificate store.
 *===========================================================================*/
typedef struct _OpcUa_X509Credential
{
    OpcUa_String Subject;
    OpcUa_String Thumbprint;
    OpcUa_String Password;
    OpcUa_String StoreLocation;
    OpcUa_String StoreName;
}
OpcUa_X509Credential;

/*============================================================================
 * OpcUa_ActualCredential
 *
 * An actually used credential.
 *===========================================================================*/
typedef struct _OpcUa_ActualCredential
{
    OpcUa_ByteString*               pClientCertificate;
    OpcUa_ByteString*               pClientPrivateKey;
    OpcUa_ByteString*               pServerCertificate;
    OpcUa_Void*                     pkiConfig;
    OpcUa_String*                   pRequestedSecurityPolicyUri;
    OpcUa_Int32                     nRequestedLifetime;
    OpcUa_MessageSecurityMode       messageSecurityMode;
}
OpcUa_ActualCredential;

/*============================================================================
 * OpcUa_CredentialType
 *===========================================================================*/
typedef enum _OpcUa_CredentialType
{
    OpcUa_CredentialType_UserName = 0x1,
    OpcUa_CredentialType_X509     = 0x2,
    OpcUa_CredentialType_Actual   = 0x4
}
OpcUa_CredentialType;

/*============================================================================
 * OpcUa_ClientCredential
 *
 * A union of all possible credential types.
 *
 * Type     - the type of credential.
 * UserName - a username/password credential.
 * X509     - an X509 certificate credential.
 *===========================================================================*/
typedef struct _OpcUa_ClientCredential
{
    OpcUa_CredentialType     Type;
    
    union
    {
    OpcUa_UserNameCredential UserName;
    OpcUa_X509Credential     X509;
    OpcUa_ActualCredential   TheActuallyUsedCredential;
    }
    Credential;
}
OpcUa_ClientCredential;

OPCUA_END_EXTERN_C

#endif /* _OpcUa_Credentials_H_ */
