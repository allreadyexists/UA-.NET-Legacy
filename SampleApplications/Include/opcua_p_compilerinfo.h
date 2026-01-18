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

#ifndef _OpcUa_P_CompilerInfo_H_
#define _OpcUa_P_CompilerInfo_H_ 1

/* compiler information constants */
#define OPCUA_P_COMPILERNAME_UNKNOWN    "Unknown Compiler"
#define OPCUA_P_COMPILERNAME_MSVC       "Microsoft Visual C/C++"
#define OPCUA_P_COMPILERNAME_MINGNUW    "GNU C++/MINGW"
#define OPCUA_P_COMPILERNAME_GNU        "GNU C++"
#define OPCUA_P_COMPILERNAME_INTEL      "Intel C++"

/* check for known compilers */
#if defined(_MSC_VER)

  /* compiler name */
# if defined(__INTEL_COMPILER)
#  define OPCUA_P_COMPILERNAME OPCUA_P_COMPILERNAME_INTEL
# else
#  define OPCUA_P_COMPILERNAME OPCUA_P_COMPILERNAME_MSVC
# endif
  /* compiler version */
# define OPCUA_P_COMPILERVERSION OPCUA_TOSTRING(_MSC_VER)

#elif defined(__GNUC__)

  /* compiler name */
# if defined(__MINGW32__)
#  define OPCUA_P_COMPILERNAME OPCUA_P_COMPILERNAME_MINGNUW
# elif defined(__INTEL_COMPILER)
#  define OPCUA_P_COMPILERNAME OPCUA_P_COMPILERNAME_INTEL
# else
#  define OPCUA_P_COMPILERNAME OPCUA_P_COMPILERNAME_GNU
# endif
  /* compiler version */
# define OPCUA_P_COMPILERVERSION OPCUA_TOSTRING(__GNUC__)"."OPCUA_TOSTRING(__GNUC_MINOR__)

#else /* compiler */

/* compiler unknown */
# define OPCUA_P_COMPILERNAME       OPCUA_P_COMPILERNAME_UNKNOWN
# define OPCUA_P_COMPILERVERSION    "0"

#endif /* compiler */

/* create defines used by the stack */
#define OPCUA_P_COMPILERINFO OPCUA_P_COMPILERNAME " " OPCUA_P_COMPILERVERSION

#endif /* _OpcUa_P_CompilerInfo_H_ */
