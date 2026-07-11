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
using System.Reflection;
using Opc.Ua.Client.Controls;
using Opc.Ua.Configuration;

namespace Opc.Ua.Sample
{
    public partial class SamplePublisherForm : Opc.Ua.Sample.Controls.PublisherForm
    {
        public SamplePublisherForm()
        {
            InitializeComponent();
        }

        public SamplePublisherForm(
            ApplicationInstance application, 
            Opc.Ua.Sample.Controls.PublisherForm masterForm, 
            ApplicationConfiguration configuration)
        :
            base(configuration.CreateMessageContext(), application, masterForm, configuration)
        {
            InitializeComponent();
            
            base.BrowseCTRL.MethodCalled += new Opc.Ua.Sample.Controls.MethodCalledEventHandler(BrowseCTRL_MethodCalled);

            if (!configuration.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                configuration.CertificateValidator.CertificateValidation += new CertificateValidationEventHandler(CertificateValidator_CertificateValidation);
            }
        }

        void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new CertificateValidationEventHandler(CertificateValidator_CertificateValidation), validator, e);
                return;
            }

            try
            {
                GuiUtils.HandleCertificateValidationError(this, validator, e);
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        void BrowseCTRL_MethodCalled(object sender, Opc.Ua.Sample.Controls.MethodCalledEventArgs e)
        {            
            try
            {
                // TBD
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
