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

#ifndef _OpcUa_XmlWriter_H_
#define _OpcUa_XmlWriter_H_ 1

#ifdef OPCUA_HAVE_XMLAPI

OPCUA_BEGIN_EXTERN_C

struct _OpcUa_XmlWriter;
struct _OpcUa_XmlReader;

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnStartElement)(
    struct _OpcUa_XmlWriter*    a_pXmlWriter,
    OpcUa_StringA               a_sNamespacePrefix,
    OpcUa_StringA               a_sElementName,
    OpcUa_StringA               a_sNamespaceUri);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_StartElement(
    struct _OpcUa_XmlWriter*    a_pXmlWriter,
    OpcUa_StringA               a_sNamespacePrefix,
    OpcUa_StringA               a_sElementName,
    OpcUa_StringA               a_sNamespaceUri);

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnEndElement)(
    struct _OpcUa_XmlWriter*    a_pXmlWriter);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_EndElement(
    struct _OpcUa_XmlWriter*    a_pXmlWriter);

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnWriteAttribute)(
    struct _OpcUa_XmlWriter*    a_pXmlWriter,
    OpcUa_StringA               a_sNamespacePrefix,
    OpcUa_StringA               a_sAttributeName,
    OpcUa_StringA               a_sNamespaceUri,
    OpcUa_StringA               a_sAttributeValue);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_WriteAttribute(
    struct _OpcUa_XmlWriter*    a_pXmlWriter,
    OpcUa_StringA               a_sNamespacePrefix,
    OpcUa_StringA               a_sAttributeName,
    OpcUa_StringA               a_sNamespaceUri,
    OpcUa_StringA               a_sAttributeValue);

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnWriteString)(
    struct _OpcUa_XmlWriter*    a_pXmlWriter,
    OpcUa_StringA               a_sValue);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_WriteString(
    struct _OpcUa_XmlWriter*    a_pXmlWriter,
    OpcUa_StringA               a_sValue);

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnWriteFormatted)(
    struct _OpcUa_XmlWriter*    a_pXmlWriter,
    OpcUa_StringA               a_sFormat,
    OpcUa_P_VA_List             a_pArguments);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_WriteFormatted(
    struct _OpcUa_XmlWriter*    a_pXmlWriter,
    OpcUa_StringA               a_sFormat,
                                ...);

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnWriteRaw)(
    struct _OpcUa_XmlWriter*     a_pXmlWriter,
    OpcUa_Byte*                 a_pRawData,
    OpcUa_UInt32                a_uDataLength);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_WriteRaw(
    struct _OpcUa_XmlWriter*     a_pXmlWriter,
    OpcUa_Byte*                  a_pRawData,
    OpcUa_UInt32                 a_uDataLength);

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnWriteNode)(
    struct _OpcUa_XmlWriter*     a_pXmlWriter,
    struct _OpcUa_XmlReader*     a_pXmlReader);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_WriteNode(
    struct _OpcUa_XmlWriter*     a_pXmlWriter,
    struct _OpcUa_XmlReader*     a_pXmlReader);

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnFlush)(
    struct _OpcUa_XmlWriter*    a_pXmlWriter);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_Flush(
    struct _OpcUa_XmlWriter*    a_pXmlWriter);

typedef OpcUa_StatusCode (OpcUa_XmlWriter_PfnClose)(
    struct _OpcUa_XmlWriter*    a_pXmlWriter);

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_Close(
    struct _OpcUa_XmlWriter*    a_pXmlWriter);

typedef struct _OpcUa_XmlWriter
{
    OpcUa_Void*                         Handle;
    OpcUa_XmlWriter_PfnStartElement*    StartElement;
    OpcUa_XmlWriter_PfnEndElement*      EndElement;
    OpcUa_XmlWriter_PfnWriteAttribute*  WriteAttribute;
    OpcUa_XmlWriter_PfnWriteString*     WriteString;
    OpcUa_XmlWriter_PfnWriteFormatted*  WriteFormatted;
    OpcUa_XmlWriter_PfnWriteRaw*        WriteRaw;
    OpcUa_XmlWriter_PfnWriteNode*       WriteNode;
    OpcUa_XmlWriter_PfnFlush*           Flush;
    OpcUa_XmlWriter_PfnClose*           Close;
} OpcUa_XmlWriter;

struct _OpcUa_OutputStream;

OPCUA_EXPORT OpcUa_StatusCode OpcUa_XmlWriter_Create(
    struct _OpcUa_XmlWriter**   a_ppXmlWriter,
    struct _OpcUa_OutputStream* a_pOutputStream);

OPCUA_EXPORT OpcUa_Void OpcUa_XmlWriter_Delete(
    struct _OpcUa_XmlWriter**   a_ppXmlWriter);

OPCUA_END_EXTERN_C

#endif /* OPCUA_HAVE_XMLAPI */
#endif /* _OpcUa_XmlWriter_H_ */
