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
using System.Windows.Forms;
using Opc.Ua;

namespace Quickstarts.AlarmConditionClient
{
    /// <summary>
    /// Prompts the select a response for a Dialog Condition.
    /// </summary>
    public partial class DialogResponseDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public DialogResponseDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Prompts the user to enter a comment.
        /// </summary>
        public int ShowDialog(DialogConditionState dialog)
        {
            // set the prompt.
            PromptLB.Text = Utils.Format("{0}", BaseVariableState.GetValue(dialog.Prompt));

            Dictionary<DialogResult,int> resultMapping = new Dictionary<DialogResult, int>();

            // configure the buttons.
            LocalizedText[] options = BaseVariableState.GetValue(dialog.ResponseOptionSet);
            
            switch (options.Length)
            {
                case 1:
                {
                    OkBTN.Text = Utils.Format("{0}", options[0]);

                    ButtonsPN.ColumnStyles[0].Width = 50;
                    ButtonsPN.ColumnStyles[1].Width = 0;
                    ButtonsPN.ColumnStyles[2].Width = 0;
                    ButtonsPN.ColumnStyles[3].Width = 0;
                    ButtonsPN.ColumnStyles[4].Width = 50;
                    
                    resultMapping.Add(DialogResult.OK, 0);
                    break;
                }

                case 2:
                {
                    OkBTN.Text = Utils.Format("{0}", options[0]);
                    Response2BTN.Text = Utils.Format("{0}", options[1]);

                    ButtonsPN.ColumnStyles[0].Width = 33;
                    ButtonsPN.ColumnStyles[1].Width = 0;
                    ButtonsPN.ColumnStyles[2].Width = 34;
                    ButtonsPN.ColumnStyles[3].Width = 0;
                    ButtonsPN.ColumnStyles[4].Width = 33;

                    resultMapping.Add(DialogResult.OK, 0);
                    resultMapping.Add(DialogResult.Retry, 1);
                    break;
                }

                case 3:
                {
                    OkBTN.Text = Utils.Format("{0}", options[0]);
                    Response1BTN.Text = Utils.Format("{0}", options[1]);
                    Response3BTN.Text = Utils.Format("{0}", options[2]);

                    ButtonsPN.ColumnStyles[0].Width = 25;
                    ButtonsPN.ColumnStyles[1].Width = 25;
                    ButtonsPN.ColumnStyles[2].Width = 0;
                    ButtonsPN.ColumnStyles[3].Width = 25;
                    ButtonsPN.ColumnStyles[4].Width = 25;

                    resultMapping.Add(DialogResult.OK, 0);
                    resultMapping.Add(DialogResult.Abort, 1);
                    resultMapping.Add(DialogResult.Ignore, 2);
                    break;
                }

                case 4:
                {
                    OkBTN.Text = Utils.Format("{0}", options[0]);
                    Response1BTN.Text = Utils.Format("{0}", options[1]);
                    Response2BTN.Text = Utils.Format("{0}", options[2]);
                    Response3BTN.Text = Utils.Format("{0}", options[3]);

                    ButtonsPN.ColumnStyles[0].Width = 20;
                    ButtonsPN.ColumnStyles[1].Width = 20;
                    ButtonsPN.ColumnStyles[2].Width = 20;
                    ButtonsPN.ColumnStyles[3].Width = 20;
                    ButtonsPN.ColumnStyles[4].Width = 20;

                    resultMapping.Add(DialogResult.OK, 0);
                    resultMapping.Add(DialogResult.Abort, 1);
                    resultMapping.Add(DialogResult.Retry, 2);
                    resultMapping.Add(DialogResult.Ignore, 3);
                    break;
                }
            }

            // display the dialog.
            DialogResult result = ShowDialog();

            // map the response.
            int selectedResponse = -1;

            if (!resultMapping.TryGetValue(result, out selectedResponse))
            {
                return -1;
            }

            return selectedResponse;
        }
        #endregion
    }
}
