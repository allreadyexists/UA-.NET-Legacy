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
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Opc.Ua.Configuration;

namespace Opc.Ua
{
    /// <summary>
    /// Manages a set of user roles.
    /// </summary>
    public class UserRoleManager
    {
        /// <summary>
        /// Initializes the manager to use the specified directory.
        /// </summary>
        public UserRoleManager(string directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException("directory");
            }

            directory = Utils.GetAbsoluteDirectoryPath(directory, false, false, false);

            if (directory == null)
            {
                throw new ArgumentException("Specified user user role directory does not exist.", "directory");
            }

            m_directory = new DirectoryInfo(directory);
        }

        /// <summary>
        /// Enumerates the available roles.
        /// </summary>
        public string[] EnumerateRoles()
        {
            List<string> templates = new List<string>();

            foreach (FileInfo file in m_directory.GetFiles("*" + m_FileExtension))
            {
                templates.Add(file.Name.Substring(0, file.Name.Length - file.Extension.Length));
            }

            return templates.ToArray();
        }

        /// <summary>
        /// Returns true if the current Windows user has access to the the specified template.
        /// </summary>
        public bool HasAccess(string template)
        {
            string filePath = Utils.GetAbsoluteFilePath(m_directory.FullName + template + m_FileExtension, false, false, false);

            // nothing more to do if no file.
            if (filePath == null)
            {
                return false;
            }

            // check if account has access to semaphore file.
            try
            {
                using (Stream ostrm = File.OpenRead(filePath))
                {
                    ostrm.Close();
                }

                // access granted.
                return true;
            }
            catch (Exception)
            {
                // no access or no file.
            }

            return false;
        }

        /// <summary>
        /// Creates a user role file.
        /// </summary>
        public static void CreateRole(string directory, string roleName, params WellKnownSidType[] sids)
        {
            string filePath = directory + " \\" + roleName + m_FileExtension;
            AccessTemplateManager.CreateFile(filePath, sids);
        }

        /// <summary>
        /// Deletes a user role file.
        /// </summary>
        public static void DeleteRole(string directory, string roleName)
        {
            string filePath = directory + " \\" + roleName + m_FileExtension;
            AccessTemplateManager.DeleteFile(filePath);
        }

        private const string m_FileExtension = ".access";
        private DirectoryInfo m_directory;
    }
}
