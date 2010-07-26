﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Cosmos.Build.Common;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio;

namespace Cosmos.VS.Package {
	[Guid(Guids.BuildPage)]
	public partial class BuildPage : ConfigurationBase {
        public static TargetHost CurrentBuildTarget = TargetHost.QEMU;
		public static event EventHandler BuildTargetChanged;

		protected static void OnBuildTargetChanged(Object sender, EventArgs e) {
			if (BuildPage.BuildTargetChanged != null) { 
                BuildPage.BuildTargetChanged(sender, e); 
            }
		}

		public BuildPage() {
			InitializeComponent();

			textOutputPath.TextChanged += delegate(Object sender, EventArgs e) {
				string value = textOutputPath.Text;
                if (!string.Equals(value, mProps.OutputPath, StringComparison.InvariantCultureIgnoreCase)) {
                    mProps.OutputPath = textOutputPath.Text;
					IsDirty = true;
				}
			};

            comboTarget.Items.AddRange(EnumValue.GetEnumValues(typeof(TargetHost), true));
            comboTarget.SelectedIndexChanged += delegate(Object sender, EventArgs e) {
				var value = (TargetHost)((EnumValue)comboTarget.SelectedItem).Value;
                if (value != mProps.Target) {
                    mProps.Target = value;
					IsDirty = true;

					CurrentBuildTarget = value;
					OnBuildTargetChanged(this, EventArgs.Empty);
				}
			};

            comboFramework.Items.AddRange(EnumValue.GetEnumValues(typeof(Framework), true));
            comboFramework.SelectedIndexChanged += delegate(Object sender, EventArgs e) {
				var value = (Framework)((EnumValue)comboFramework.SelectedItem).Value;
                if (value != mProps.Framework) {
                    mProps.Framework = value;
					IsDirty = true;
				}
			};

            checkUseInternalAssembler.CheckedChanged += delegate(Object sender, EventArgs e) {
				bool value = checkUseInternalAssembler.Checked;
                if (value != mProps.UseInternalAssembler) {
                    mProps.UseInternalAssembler = value;
					IsDirty = true;
				}
			};
		}

		protected BuildProperties mProps = new BuildProperties();
		public override PropertiesBase Properties { 
            get { return mProps; } 
        }

		protected override void FillProperties() {
			base.FillProperties();

            mProps.Reset();
            
            mProps.SetProperty("OutputPath", GetConfigProperty("OutputPath"));
            textOutputPath.Text = mProps.OutputPath;

            mProps.SetProperty("BuildTarget", GetConfigProperty("BuildTarget"));
            comboTarget.SelectedItem = EnumValue.Find(comboTarget.Items, mProps.Target);
            // We need to manually trigger it once, because the indexchanged event compares
            // it against the source, and they will of course be the same.
            CurrentBuildTarget = (TargetHost)((EnumValue)comboTarget.SelectedItem).Value;
            OnBuildTargetChanged(this, EventArgs.Empty);

            mProps.SetProperty("Framework", GetConfigProperty("Framework"));
            comboFramework.SelectedItem = EnumValue.Find(comboFramework.Items, mProps.Framework);

            mProps.SetProperty("UseInternalAssembler", GetConfigProperty("UseInternalAssembler"));
            checkUseInternalAssembler.Checked = mProps.UseInternalAssembler;
		}

		private void OutputBrowse_Click(object sender, EventArgs e) {
			string folderPath = String.Empty;
			var dialog = new FolderBrowserDialog();
			dialog.ShowNewFolderButton = true;

			folderPath = textOutputPath.Text;
			if ((String.IsNullOrEmpty(folderPath) == false) && (folderPath.IndexOfAny(System.IO.Path.GetInvalidPathChars()) == -1)) {
				if (System.IO.Path.IsPathRooted(folderPath) == false) { 
                    folderPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Project.FullName), folderPath); 
                }

				while ((System.IO.Directory.Exists(folderPath) == false) && (String.IsNullOrEmpty(folderPath) == false)) {
					int index = -1;
					index = folderPath.IndexOfAny(new Char[] { System.IO.Path.PathSeparator, System.IO.Path.AltDirectorySeparatorChar });
					if (index > -1) {
						folderPath = folderPath.Substring(0, index - 1);
					} else { 
                        folderPath = String.Empty; 
                    }
				}

				if (String.IsNullOrEmpty(folderPath) == true) {
                    folderPath = System.IO.Path.GetDirectoryName(Project.FullName);
                }
			} else {
				folderPath = System.IO.Path.GetDirectoryName(Project.FullName);
			}

			dialog.SelectedPath = folderPath;
            dialog.Description = "Select build output path";

			if (dialog.ShowDialog() == DialogResult.OK) {
                textOutputPath.Text = dialog.SelectedPath; 
            }
		}

        private void comboTarget_SelectedIndexChanged(object sender, EventArgs e) {
            var xEnumValue = (EnumValue)comboTarget.SelectedItem;
            var xValue = (TargetHost)xEnumValue.Value;
            if (!(xValue == TargetHost.VMWareWorkstation || xValue == TargetHost.QEMU)) {
                MessageBox.Show("This type is temporarily unsupported.");
            }
        }

	}
}