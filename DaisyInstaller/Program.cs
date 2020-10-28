﻿using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Resources;

namespace DaisyInstaller
{
    static class Program
    {
        /* Versions of offices
         * Office 3.0 (Word 2.0c, Excel 4.0a, PowerPoint 3.0, Mail)
         * Office 4.0 (Word 6.0, Excel 4.0, PowerPoint 3.0)
         * Office 4.2 (Word 6.0, Excel 5.0, PowerPoint 4.0, « Microsoft Office Manager »)
         * Office 4.3 (Word 6.0, Excel 5.0, PowerPoint 4.0, Pro:Access 2.0)
         * Office 95/7.0 (Word 95, etc.)
         * Office 97/8.0 (Word 97, etc.)
         * Office 2000/9.0 (Word 2000, etc.)
         * Office XP/10.0 (Word 2002, etc.)
         * Office 2003/11.0 (Word 2003, etc.)
         * Office 2007/12.0 (Word 2007, etc.)
         * Office 2010/14.0 (Word 2010, etc.)
         * Office 2013/15.0 (Word 2013, etc.)
         * Office 2016/16.0 (Word 2016, etc.)
         * Office 2019/17.0 (Word 2019, etc.)
         */

        public static float minimalVersionSupport = 10.0f;
        public static float maximalVersionSupport = 14.0f;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Search for an existing office installation :
            //RegistryKey lKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office");
            //// search of MS Word intalling last version
            //RegistryKey lKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office\14.0\Word\InstallRoot");
            //if (lKey == null)
            //    lKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Word\InstallRoot");
            //if (lKey == null)
            //    lKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Word\InstallRoot");
            //if (lKey == null)
            //    lKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office\10.0\Word\InstallRoot");

#if X64INSTALLER
            bool installerIsForOffice32Bits = false;
#else
            bool installerIsForOffice32Bits = true;
#endif

            // If we want to check for Windows Arch, but we assume that windows is x64 as Microsoft is pushing to remove Windows x86 release.
            int bits = IntPtr.Size * 8; // 32 or 64, depending on executing arch


            bool officeIs64bits = true; 
            RegistryKey lKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office");
            RegistryKey wordRoot = null;
            float lastVersion = 0.0f;
            foreach (string subKey in lKey.GetSubKeyNames()) {
                // Check if the key name is a version number
                Regex versionNumber = new Regex("[0-9]+\\.[0-9]+");
                Match result = versionNumber.Match(subKey);
                if (result.Success) {
                    // if it is a superior versionCheck if it has a word subkey
                    float version = float.Parse(result.Value, CultureInfo.InvariantCulture.NumberFormat);
                    if (lastVersion < version) {
                        lastVersion = version;
                        RegistryKey wordKey = lKey.OpenSubKey(subKey + @"\Word\InstallRoot");
                        if (wordKey != null) wordRoot = wordKey;
                    }
                }
            }
            // Check for 32bits install on x64 system
            if(wordRoot == null) {
                officeIs64bits = false;
                lKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Office");
                lastVersion = 0.0f;
                foreach (string subKey in lKey.GetSubKeyNames()) {
                    // Check if the key name is a version number
                    Regex versionNumber = new Regex("[0-9]+\\.[0-9]+");
                    Match result = versionNumber.Match(subKey);
                    if (result.Success) {
                        // if it is a superior versionCheck if it has a word subkey
                        float version = float.Parse(result.Value, CultureInfo.InvariantCulture.NumberFormat);
                        if (lastVersion < version) {
                            lastVersion = version;
                            RegistryKey wordKey = lKey.OpenSubKey(subKey + @"\Word\InstallRoot");
                            if (wordKey != null) wordRoot = wordKey;
                        }
                    }
                }
            }
            string warning = "";
            string error = "";
            bool keepInstall = true;
            if (wordRoot == null) {
                warning = "Microsoft Word was not found in your system registry.\r\nDo you want to continue anyway and install the addin for Office " + (installerIsForOffice32Bits ? "32Bits" : "64Bits") + "?" ;
                warning += "\r\n\r\nPlease check your office \"bit\" version to ensure you have the correct installer (link is in your clipboard):\r\n https://support.microsoft.com/en-us/office/about-office-what-version-of-office-am-i-using-932788b8-a3ce-44bf-bb09-e334518b8b19";
                Clipboard.SetText("https://support.microsoft.com/en-us/office/about-office-what-version-of-office-am-i-using-932788b8-a3ce-44bf-bb09-e334518b8b19");
            } else if (!(installerIsForOffice32Bits ^ officeIs64bits)) {
                error = "This installer is for Office " + (installerIsForOffice32Bits ? "32Bits" : "64Bits") + " while Office " + (officeIs64bits ? "64Bits" : "32Bits") + " was found on your system.\r\nPlease download the installer for Office " + (officeIs64bits ? "64Bits" : "32Bits") + ".";
            } else if (lastVersion < minimalVersionSupport || lastVersion > maximalVersionSupport) {
                warning = "This addin officially supports Microsoft Word from Office XP, up to Office 2010.\r\nA newer version of word has beend found on your system but may not load this addin correctly.\r\nDo you want to continue anyway ?";
            }

            if(error.Length > 0) {
                MessageBox.Show(error, "Wrong installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                keepInstall = false;
            } else if (warning.Length > 0) {
                keepInstall = MessageBox.Show(warning, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }

            // Launch the install process
            if (keepInstall) {
                // start DaisyAddinForWordSetup.msi
                string daisySetupPath = Path.Combine(Path.GetTempPath(), "DaisyAddinForWordSetup.msi");
                File.WriteAllBytes(daisySetupPath, Properties.Resources.DaisyAddinForWordSetup);

                Process.Start(daisySetupPath);
            } else return;
            
        }
    }
}
