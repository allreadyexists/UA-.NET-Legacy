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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Opc.Ua.StackTest
{
    /// <summary>
    /// A class which displays the test parameters for a test case.
    /// </summary>
    public partial class TestParametersCtrl : Opc.Ua.Client.Controls.BaseListCtrl
    {
        /// <summary>
        /// Initializes GUI Components.
        /// Initializes column names.
        /// </summary>
        public TestParametersCtrl()
        {
            InitializeComponent();              
			SetColumns(m_ColumnNames);
        }

        #region Private Fields
       
        // An object of type TestCase.<see ref="TestCase"/>      
        private TestCase m_testcase;
       
		// The columns to display in the control.		
		private readonly object[][] m_ColumnNames = new object[][]
		{
			new object[] { "Name",  HorizontalAlignment.Left, null },  
			new object[] { "Value", HorizontalAlignment.Left,   null }
		};
		#endregion
        
        /// <summary>
        /// Displays a test case in the control.
        /// </summary>
        public void Initialize(TestCase testcase)
        {
            ItemsLV.Items.Clear();

            m_testcase = testcase;

            if (testcase != null)
            {
                AddParamater("TestID",   testcase.TestId);
                AddParamater("TestCase", testcase.Name);

                if (testcase.SeedSpecified)
                {
                    AddParamater("Seed", testcase.Seed);
                }
                
                if (testcase.StartSpecified)
                {
                    AddParamater("Start", testcase.Start);
                }
                
                if (testcase.CountSpecified)
                {
                    AddParamater("Count", testcase.Count);
                }

                AddParamater("SkipTest", testcase.SkipTest);

                if (testcase.Parameter != null)
                {
                    foreach (TestParameter parameter in testcase.Parameter)
                    {
                        AddItem(parameter);
                    }
                }
            }

            AdjustColumns();
        } 

        private void AddParamater(string name, object value)
        {
            TestParameter parameter = new TestParameter();

            parameter.Name  = name;
            parameter.Value = String.Format("{0}", value);

            AddItem(parameter);
        }

        #region Overridden Methods
        /// <see cref="Opc.Ua.Client.Controls.BaseListCtrl.EnableMenuItems" />
		protected override void EnableMenuItems(ListViewItem clickedItem)
		{
            // TBD
		}

        /// <see cref="Opc.Ua.Client.Controls.BaseListCtrl.UpdateItem(ListViewItem,object)" />
        protected override void UpdateItem(ListViewItem listItem, object item)
        {
			TestParameter parameter = item as TestParameter;

			if (parameter == null)
			{
				base.UpdateItem(listItem, item);
				return;
			}

			listItem.SubItems[0].Text = String.Format("{0}", parameter.Name);
			listItem.SubItems[1].Text = String.Format("{0}", parameter.Value);

            listItem.ImageKey = "SimpleItem";
			listItem.Tag = item;
        }
        #endregion
    }
}
