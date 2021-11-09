namespace D365O_Addin_SimpleList
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Microsoft.Dynamics.AX.Metadata.Core;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
    using EnvDTE;
    using System.Globalization;
    using Microsoft.Dynamics.Framework.Tools.ProjectSystem;
    using Microsoft.Dynamics.AX.Metadata.MetaModel;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Say a few words about what your AddIn is going to do
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    // TODO: This addin will show when user right clicks on a form root node or table root node. 
    // If you need to specify any other element, change this AutomationNodeType value.
    // You can specify multiple DesignerMenuExportMetadata attributes to meet your needs
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    public class DesignerContextMenuAddIn : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "DesignerD365O_Addin_SimpleList";
        VSProjectNode activeProjectNode;
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
                ModelInfo modelInfo = activeProjectNode.GetProjectsModelInfo(true);

                if (e.SelectedElement is ITable)
                {
                    ITable table = e.SelectedElement as ITable;
                    AxTable axTable = DesignMetaModelService.Instance.GetTable(table.Name);
                    AxTableFieldGroup overview = null;

                    foreach (var AxTableFieldGroup in axTable.FieldGroups)
                    {
                        if (string.Equals(AxTableFieldGroup.Name, "Overview"))
                        {
                            overview = AxTableFieldGroup;
                        }
                    }
                    if (overview == null)
                    {
                        throw new Exception($"The field group Overview must be specified on table {axTable.Name}");
                    }

                    AxForm axForm = new AxForm
                    {
                        Name = axTable.Name
                    };
                    AxFormDataSourceRoot formDataSourceRoot = new AxFormDataSourceRoot
                    {
                        Name = axTable.Name,
                        Table = axTable.Name,
                        InsertIfEmpty = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.NoYes.No
                    };

                    Dictionary<string, AxTableField> axTableFieldDir = new Dictionary<string, AxTableField>();
                    foreach (var tableField in axTable.Fields)
                    {
                        AxFormDataSourceField axFormDataSourceField = new AxFormDataSourceField
                        {
                            Name = tableField.Name,
                            DataField = tableField.Name
                        };
                        formDataSourceRoot.Fields.Add(axFormDataSourceField);
                        axTableFieldDir.Add(tableField.Name, tableField);
                    }

                    axForm.AddDataSource(formDataSourceRoot);

                    AxFormDesign formDesign = axForm.Design;
                    formDesign.Pattern = "SimpleList";
                    formDesign.PatternVersion = "1.1";
                    formDesign.Style = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormStyle.SimpleList;
                    formDesign.Caption = axTable.Label;

                    AxFormActionPaneControl actionPaneControl = new AxFormActionPaneControl();
                    actionPaneControl.Name = "ActionPane";
                    formDesign.AddControl(actionPaneControl);

                    AxFormGroupControl FilterGroupControl = new AxFormGroupControl
                    {
                        Name = "QuickFilterGroup",
                        Pattern = "CustomAndQuickFilters",
                        PatternVersion = "1.1",
                        WidthMode = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormWidthHeightMode.SizeToAvailable
                    };
                    AxFormControl quickFilter = new AxFormControl();
                    quickFilter.Name = "QuickFilter";

                    AxFormControlExtension QuickFilter = new AxFormControlExtension();
                    QuickFilter.Name = "QuickFilterControl";
                    quickFilter.FormControlExtension = QuickFilter;
                    FilterGroupControl.AddControl(quickFilter);

                    formDesign.AddControl(FilterGroupControl);

                    AxFormGridControl gridControl = new AxFormGridControl
                    {
                        Name = "Grid",
                        DataSource = formDataSourceRoot.Name,
                        DataGroup = "Overview"
                    };

                    foreach (var field in overview.Fields)
                    {
                        if (!axTableFieldDir.ContainsKey(field.DataField))
                        {
                            continue;
                        }
                        AxTableField axTableField = axTableFieldDir[field.DataField];


                        if (axTableField is AxTableFieldString)
                        {
                            AxFormStringControl axFormControl = new AxFormStringControl();
                            axFormControl.Name = $"Overview_{field.Name}";
                            axFormControl.DataSource = formDataSourceRoot.Name;
                            axFormControl.DataField = field.Name;
                            gridControl.AddControl(axFormControl);
                        }

                        if (axTableField is AxTableFieldDate)
                        {
                            AxFormDateControl axFormControl = new AxFormDateControl
                            {
                                Name = $"Overview_{field.Name}",
                                DataSource = formDataSourceRoot.Name,
                                DataField = field.Name
                            };

                            gridControl.AddControl(axFormControl);
                        }
                        
                        if (axTableField is AxTableFieldEnum)
                        {
                            AxTableFieldEnum axTableFieldEnum = axTableField as AxTableFieldEnum;
                            if (string.Equals(axTableFieldEnum.EnumType, "NoYes"))
                            {
                                AxFormCheckBoxControl axFormControl = new AxFormCheckBoxControl
                                {
                                    Name = $"Overview_{field.Name}",
                                    DataSource = formDataSourceRoot.Name,
                                    DataField = field.Name
                                };
                                gridControl.AddControl(axFormControl);
                            }
                            else
                            {
                                AxFormComboBoxControl axFormControl = new AxFormComboBoxControl
                                {
                                    Name = $"Overview_{field.Name}",
                                    DataSource = formDataSourceRoot.Name,
                                    DataField = field.Name
                                };
                                gridControl.AddControl(axFormControl);
                            }

                        }

                        if (axTableField is AxTableFieldInt)
                        {
                            AxFormIntegerControl axFormControl = new AxFormIntegerControl
                            {
                                Name = $"Overview_{field.Name}",
                                DataSource = formDataSourceRoot.Name,
                                DataField = field.Name
                            };

                            gridControl.AddControl(axFormControl);
                        }

                        if (axTableField is AxTableFieldInt64)
                        {
                            AxFormInt64Control axFormControl = new AxFormInt64Control
                            {
                                Name = $"Overview_{field.Name}",
                                DataSource = formDataSourceRoot.Name,
                                DataField = field.Name
                            };

                            gridControl.AddControl(axFormControl);
                        }

                        if (axTableField is AxTableFieldReal)
                        {
                            AxFormRealControl axFormControl = new AxFormRealControl
                            {
                                Name = $"Overview_{field.Name}",
                                DataSource = formDataSourceRoot.Name,
                                DataField = field.Name
                            };

                            gridControl.AddControl(axFormControl);
                        }

                        if (axTableField is AxTableFieldUtcDateTime)
                        {
                            AxFormDateTimeControl axFormControl = new AxFormDateTimeControl
                            {
                                Name = $"Overview_{field.Name}",
                                DataSource = formDataSourceRoot.Name,
                                DataField = field.Name
                            };

                            gridControl.AddControl(axFormControl);
                        }

                        if (axTableField is AxTableFieldTime)
                        {
                            AxFormTimeControl axFormControl = new AxFormTimeControl
                            {
                                Name = $"Overview_{field.Name}",
                                DataSource = formDataSourceRoot.Name,
                                DataField = field.Name
                            };

                            gridControl.AddControl(axFormControl);
                        }

                        if (axTableField is AxTableFieldGuid)
                        {
                            AxFormGuidControl axFormControl = new AxFormGuidControl
                            {
                                Name = $"Overview_{field.Name}",
                                DataSource = formDataSourceRoot.Name,
                                DataField = field.Name
                            };

                            gridControl.AddControl(axFormControl);
                        }
                        
                    }
            
                    formDesign.AddControl(gridControl);

                    var metaModelService = DesignMetaModelService.Instance.CurrentMetaModelService;
                    metaModelService.CreateForm(axForm, new ModelSaveInfo(modelInfo));
                    activeProjectNode.AddModelElementsToProject(new List<MetadataReference>
                    {
                        new MetadataReference(axForm.Name, axForm.GetType())
                    });

                    AxMenuItemDisplay axMenuItemDisplay = new AxMenuItemDisplay
                    {
                        Name = axForm.Name,
                        Object = axForm.Name,
                        Label = axForm.Design.Caption
                    };
                    metaModelService.CreateMenuItemDisplay(axMenuItemDisplay, new ModelSaveInfo(modelInfo));
                    activeProjectNode.AddModelElementsToProject(new List<MetadataReference>
                    {
                        new MetadataReference(axMenuItemDisplay.Name, axMenuItemDisplay.GetType())
                    });
                }
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion

        static VSProjectNode GetActiveProjectNode(DTE dte)
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
        protected void appendToProject(AxForm _form)
        {
            activeProjectNode.AddModelElementsToProject(new List<MetadataReference>
                    {
                        new MetadataReference(_form.Name, _form.GetType())
                    });
            //var projectService = ServiceLocator.GetService(typeof(IDynamicsProjectService)) as IDynamicsProjectService;
            //projectService.AddElementToActiveProject(privilege);
        }
    }
}