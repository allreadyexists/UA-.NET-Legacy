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

#ifndef _COpcXmlAttribute_H_
#define _COpcXmlAttribute_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "COpcString.h"
#include "COpcArray.h"
#include "OpcXmlType.h"

//==============================================================================
// CLASS:   COpcXmlAttribute
// PURPOSE  Represents an XML attribute.

class OPCUTILS_API COpcXmlAttribute 
{
    OPC_CLASS_NEW_DELETE_ARRAY();

public:

    //==========================================================================
    // Public Operators

    // Constructor
    COpcXmlAttribute(IUnknown* ipUnknown = NULL);

    // Copy Constructor
    COpcXmlAttribute(const COpcXmlAttribute& cAttribute);
            
    // Destructor
    ~COpcXmlAttribute();

    // Assignment
    COpcXmlAttribute& operator=(IUnknown* ipUnknown);
    COpcXmlAttribute& operator=(const COpcXmlAttribute& cAttribute);

    // Accessor
    operator IXMLDOMAttribute*() const { return m_ipAttribute; }

    //==========================================================================
    // Public Methods
    
    // GetName
    COpcString GetName();
    	
	// Prefix
    COpcString GetPrefix();   
        
	// Namespace
	COpcString GetNamespace();

	// GetQualifiedName
	OpcXml::QName GetQualifiedName();

    // GetValue
    COpcString GetValue();
   
protected:

    //==========================================================================
    // Private Members

    IXMLDOMAttribute* m_ipAttribute;
};

//==============================================================================
// TYPE:    COpcXmlAttributeList
// PURPOSE: A list of elements.

typedef COpcArray<COpcXmlAttribute> COpcXmlAttributeList;

#endif // _COpcXmlAttribute_H_
