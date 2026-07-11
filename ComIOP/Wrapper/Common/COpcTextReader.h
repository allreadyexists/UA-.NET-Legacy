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

#ifndef _COpcTextReader_H
#define _COpcTextReader_H

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "OpcDefs.h"
#include "COpcText.h"

//==============================================================================
// CLASS:   COpcTextReader
// PURPOSE: Extracts tokens from a stream.

class OPCUTILS_API COpcTextReader
{
    OPC_CLASS_NEW_DELETE();

public:

    //==========================================================================
    // Operators

    // Constructor
    COpcTextReader(const COpcString& cBuffer);  
    COpcTextReader(LPCSTR szBuffer, UINT uLength = -1);  
    COpcTextReader(LPCWSTR szBuffer, UINT uLength = -1);  
 
    // Destructor
    ~COpcTextReader(); 

    //==========================================================================
    // Public Methods
  
    // GetNext
    bool GetNext(COpcText& cText);

    // GetBuf
    LPCWSTR GetBuf() const { return m_szBuf; }

private:

    //==========================================================================
    // Private Methods

    // ReadData
    bool ReadData();

    // FindToken
    bool FindToken(COpcText& cText);

    // FindLiteral
    bool FindLiteral(COpcText& cText);

    // FindNonWhitespace
    bool FindNonWhitespace(COpcText& cText);

    // FindWhitespace
    bool FindWhitespace(COpcText& cText);
    
    // FindDelimited
    bool FindDelimited(COpcText& cText);

    // FindEnclosed
    bool FindEnclosed(COpcText& cText);

    // CheckForHalt
    bool CheckForHalt(COpcText& cText, UINT uIndex);
    
    // CheckForDelim
    bool CheckForDelim(COpcText& cText, UINT uIndex);

    // SkipWhitespace
    UINT SkipWhitespace(COpcText& cText);

    // CopyData
    void CopyData(COpcText& cText, UINT uStart, UINT uEnd);

    //==========================================================================
    // Private Members

    LPWSTR m_szBuf;
    UINT   m_uLength;
    UINT   m_uEndOfData;
};

#endif //ndef _COpcTextReader_H
