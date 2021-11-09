namespace AutoCreateLabel
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Microsoft.Dynamics.AX.Metadata.Core;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.LabelFiles;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Security;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
    using Microsoft.Dynamics.Framework.Tools.Labels;
    using EnvDTE;
    using System.Globalization;
    using Microsoft.Dynamics.Framework.Tools.ProjectSystem;
    using Microsoft.Dynamics.Framework.Tools.ProjectSupport;
    using Microsoft.Dynamics.AX.Metadata.MetaModel;
    using System.Collections.Generic;
    using Microsoft.Dynamics.Ax.Xpp.Metadata;
    using System.IO;
    using System.Xml;
    /// <summary>
    /// TODO: Say a few words about what your AddIn is going to do
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    // TODO: This addin will show when user right clicks on a form root node or table root node. 
    // If you need to specify any other element, change this AutomationNodeType value.
    // You can specify multiple DesignerMenuExportMetadata attributes to meet your needs
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseField))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IEdtBase))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnum))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnumExtension))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnumValue))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IMenuItem))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IView))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IViewBaseField))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFieldGroupBase))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFormControl))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ISecurityPrivilege))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowHierarchyAssignmentProvider))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowApproval))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowTask))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowCategory))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowTemplate))]
    public class DesignerContextMenuAddIn : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "DesignerAutoCreateLabel";
        public VSProjectNode activeProjectNode;
        public ModelInfo modelInfo;
        #endregion

        #region Properties
        /// <summary>
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return AddinResources.DesignerAddinCaption;
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return DesignerContextMenuAddIn.addinName;
            }
        }
        #endregion

        #region Callbacks
        /// <summary>
        /// Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinDesignerEventArgs e)
        {
            try
            {
                DTE dte = CoreUtility.ServiceProvider.GetService(typeof(DTE)) as DTE;
                if (dte == null)
                {
                    throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "No service for DTE found. The DTE must be registered as a service for using this API.", new object[0]));
                }
                activeProjectNode = GetActiveProjectNode(dte);
                modelInfo = activeProjectNode.GetProjectsModelInfo(true);
                
                UserInterface userInterface = new UserInterface();
                userInterface.LabelFile.DataSource = this.getLabelFiles() ;
                userInterface.SecondLanguage.DataSource = Microsoft.Dynamics.Ax.Xpp.LabelHelper.GetInstalledLanguages();
                userInterface.SecondLanguage.Text = "zh-hans";
               
                userInterface.ShowDialog();

                if (!userInterface.closeOk)
                {
                    return;
                }
                Building.CreateLabels labels = Building.CreateLabels.construct(e.SelectedElement as NamedElement);
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
        public  List<String> getLabelFiles()
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
        public static VSProjectNode GetActiveProjectNode(DTE dte)
        {
            Array array = dte.ActiveSolutionProjects as Array;
            if (array != null && array.Length > 0)
            {
                Project project = array.GetValue(0) as Project;
                if (project != null)
                {
                    return project.Object as VSProjectNode;
                }
            }
            return null;
        }
    }
}