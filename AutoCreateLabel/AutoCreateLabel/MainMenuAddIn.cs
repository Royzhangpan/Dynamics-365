namespace Addin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

    using Microsoft.Dynamics.Framework.Tools.ProjectSystem;
    using Microsoft.Dynamics.Framework.Tools.ProjectSupport;
    using Microsoft.Dynamics.AX.Metadata.MetaModel;
    using EnvDTE;
    using System.Globalization;
    using AutoCreateLabel;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Created new labels automaticlly when prefixing them as @@@
    /// </summary>
    [Export(typeof(IMainMenu))]
    public class MainMenuAddIn : MainMenuBase
    {
        #region Member variables
        private const string addinName = "Addin";
        private VSProjectNode activeProjectNode;
        ModelInfo modelInfo;
        #endregion

        #region Properties
        /// <summary>
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return "Create label";
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return MainMenuAddIn.addinName;
            }
        }

        #endregion

        #region Callbacks
        /// <summary>
        /// Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinEventArgs e)
        {
            try
            {
                DTE dte = CoreUtility.ServiceProvider.GetService(typeof(DTE)) as DTE;
                if (dte == null)
                {
                    throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "No service for DTE found. The DTE must be registered as a service for using this API.", new object[0]));
                }
                activeProjectNode = DesignerContextMenuAddIn.GetActiveProjectNode(dte);
                modelInfo = activeProjectNode.GetProjectsModelInfo(true);
                
                UserInterface userInterface = new UserInterface();
                userInterface.LabelFile.DataSource = getLabelFiles();
                userInterface.SecondLanguage.DataSource = Microsoft.Dynamics.Ax.Xpp.LabelHelper.GetInstalledLanguages();
                userInterface.SecondLanguage.Text = "zh-hans";

                userInterface.ShowDialog();

                if (!userInterface.closeOk)
                {
                    return;
                }
                Building.CreateLabels labels = Building.CreateLabels.construct(null);
                labels.labelManager.LABELFILEID = userInterface.LabelFile.Text;

                labels.labelManager.labelId = userInterface.LabelId.Text;
                labels.labelManager.label_enus = userInterface.enusText.Text;
                labels.labelManager.secLanguage = userInterface.SecondLanguage.Text;
                labels.labelManager.secLabel = userInterface.SecText.Text;
                labels.run();


                CoreUtility.DisplayInfo(labels.getLoggingMessage());
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion

        public List<String> getLabelFiles()
        {
            List<String> labelFields = new List<string>();
            string filePath = DesignMetaModelService.Instance.GetModelStorePathForModel(modelInfo) + "\\AxLabelFile";

            if (Directory.Exists(filePath))
            {
                var folder = new DirectoryInfo(filePath);

                foreach (var labelFile in folder.GetFiles("*.xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(labelFile.FullName);
                    string labelId = doc.SelectSingleNode("AxLabelFile/LabelFileId").InnerText;
                    if (!labelFields.Exists(t => t == labelId))
                    {
                        labelFields.Add(labelId);
                    }
                }
            }
            return labelFields;
        }
    }
}
