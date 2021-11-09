using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Security;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;

namespace Building
{
    /// <summary>
    /// Base class to create new labels
    /// </summary>
    abstract public class CreateLabels
    {
        public Labeling.LabelManager labelManager;
        /// <summary>
        /// Initialize class
        /// </summary>
        public CreateLabels()
        {
            labelManager = new Labeling.LabelManager();
        }

        /// <summary>
        /// Point to all label properties and child elements label properties
        /// </summary>
        abstract public void run();

        /// <summary>
        /// Contructs a classes based on the element type
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        static public CreateLabels construct(NamedElement element)
        {
            // TODO: Add here new elements constructors

            if (element == null)
            {
                return new CreateLabels_NewLabel();
            }
            if (element is BaseField)
            {
                return new CreateLabels_BaseField(element as BaseField);
            }

            if (element is FormControl)
            {
                return new CreateLabels_FormControl(element as FormControl);
            }

            if (element is ViewBaseField)
            {
                return new CreateLabels_ViewField(element as ViewBaseField);
            }
            switch (element.GetType().Name)
            {
                case "Table":
                    return new CreateLabels_Table(element as Table);
                case "View":
                    return new CreateLabels_View(element as View);
                case "FieldGroup":
                    return new CreateLabels_FieldGroup(element as FieldGroupBase);

                case "EdtBase":
                case "EdtString":
                case "EdtContainer":
                case "EdtDate":
                case "EdtEnum":
                case "EdtDateTime":
                case "EdtGuid":
                case "EdtReal":
                case "EdtInt":
                case "EdtInt64":
                    return new CreateLabels_Edt(element as EdtBase);

                case "BaseEnum":
                    return new CreateLabels_BaseEnum(element as BaseEnum);

                case "BaseEnumExtension":
                    return new CreateLabels_BaseEnumExtension(element as BaseEnumExtension);

                case "MenuItem":
                case "MenuItemAction":
                case "MenuItemDisplay":
                case "MenuItemOutput":
                    return new CreateLabels_MenuItem(element as MenuItem);

                case "Form":
                    return new CreateLabels_Form(element as Form);
                case "SecurityPrivilege":
                    return new CreateLabels_SecurityPrivilege(element as SecurityPrivilege);

                case "WorkflowHierarchyAssignmentProvider":
                    return new CreateLabels_WorkflowHierarchyAssignmentProvider(element as WorkflowHierarchyAssignmentProvider);

                case "WorkflowApproval":
                    return new CreateLabels_WorkflowApproval(element as WorkflowApproval);

                case "WorkflowCategory":
                    return new CreateLabels_WorkflowCategory(element as WorkflowCategory);

                case "WorkflowTask":
                    return new CreateLabels_WorkflowTask(element as WorkflowTask);

                case "WorkflowTemplate"://WorkflowType Object
                    return new CreateLabels_WorkflowType(element as WorkflowTemplate);

                default:
                    throw new NotImplementedException($"The type {element.GetType().Name} is not implemented.");
            }
        }

        /// <summary>
        /// Get logging message
        /// </summary>
        /// <returns>Logging message</returns>
        public string getLoggingMessage()
        {
            return this.labelManager.getLoggingMessage();
        }
    }

    public class CreateLabels_NewLabel : CreateLabels
    {
        public CreateLabels_NewLabel()
        { }
        public override void run()
        {

            System.Windows.Forms.Clipboard.SetDataObject(this.labelManager.createLabel());
        }
    }
    /// <summary>
    /// Creates labels to table and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: table, fields, field groups</remarks>
    public class CreateLabels_Table : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current table
        /// </summary>
        protected Table table;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="table">Selected table element</param>
        public CreateLabels_Table(Table table)
        {
            this.table = table;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.table.Label = this.labelManager.createLabel();
            
        }
    }
    public class CreateLabels_FieldGroup : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current table
        /// </summary>
        protected FieldGroupBase fieldGroup;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="table">Selected table element</param>
        public CreateLabels_FieldGroup(FieldGroupBase fieldGroup)
        {
            this.fieldGroup = fieldGroup;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.fieldGroup.Label = this.labelManager.createLabel();

        }
    }
    public class CreateLabels_BaseField : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current table
        /// </summary>
        protected BaseField field;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="table">Selected table element</param>
        public CreateLabels_BaseField(BaseField field)
        {
            this.field = field;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.field.Label = this.labelManager.createLabel();

        }
    }
    /// <summary>
    /// Creates labels to table and child elements
    /// </summary>
    public class CreateLabels_Edt : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current Edt
        /// </summary>
        protected EdtBase edt;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="edt">Selected edt element</param>
        public CreateLabels_Edt(EdtBase edt)
        {
            this.edt = edt;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.edt.Label = this.labelManager.createLabel();
            
        }
    }

    /// <summary>
    /// Creates labels to BaseEnum and child elements
    /// </summary>
    public class CreateLabels_BaseEnum : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current BaseEnum
        /// </summary>
        protected BaseEnum baseEnum;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="baseEnum">Selected baseenum element</param>
        public CreateLabels_BaseEnum(BaseEnum baseEnum)
        {
            this.baseEnum = baseEnum;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.baseEnum.Label = this.labelManager.createLabel();
            
        }
    }
    public class CreateLabels_BaseEnumValue : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current BaseEnum
        /// </summary>
        protected BaseEnumValue baseEnumValue;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="baseEnum">Selected baseenum element</param>
        public CreateLabels_BaseEnumValue(BaseEnumValue baseEnumValue)
        {
            this.baseEnumValue = baseEnumValue;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.baseEnumValue.Label = this.labelManager.createLabel();

        }
    }
    /// <summary>
    /// Creates labels to BaseEnum extension and child elements
    /// </summary>
    public class CreateLabels_BaseEnumExtension : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current BaseEnum extension
        /// </summary>
        protected BaseEnumExtension baseEnumExtension;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="baseEnumExtension">Selected baseenum extension element</param>
        public CreateLabels_BaseEnumExtension(BaseEnumExtension baseEnumExtension)
        {
            this.baseEnumExtension = baseEnumExtension;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Elements labels
            foreach (BaseEnumValue values in this.baseEnumExtension.BaseEnumValues)
            {
                values.Label = this.labelManager.createLabel();
            }
        }
    }

    /// <summary>
    /// Creates labels to MenuItem and child elements
    /// </summary>
    public class CreateLabels_MenuItem : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current menuitem
        /// </summary>
        protected MenuItem menuItem;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="menuItem">Selected menuitem element</param>
        public CreateLabels_MenuItem(MenuItem menuItem)
        {
            this.menuItem = menuItem;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.menuItem.Label = this.labelManager.createLabel();

            // Help text
            this.menuItem.HelpText = this.labelManager.createLabel();
        }
    }

    /// <summary>
    /// Creates labels to view and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: view, fields, field groups</remarks>
    public class CreateLabels_View : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current view
        /// </summary>
        protected View view;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="view">Selected view element</param>
        public CreateLabels_View(View view)
        {
            this.view = view;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.view.Label = this.labelManager.createLabel();
            
        }
    }

    public class CreateLabels_ViewField : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current view
        /// </summary>
        protected ViewBaseField field;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="view">Selected view element</param>
        public CreateLabels_ViewField(ViewBaseField field)
        {
            this.field = field;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            field.Label = this.labelManager.createLabel();
        }
    }

    /// <summary>
    /// Creates labels to form and child elements
    /// </summary>
    /// <remarks>Currently, the following elements are covered: design and controls</remarks>
    public class CreateLabels_Form : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current view
        /// </summary>
        protected Form form;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="form">Selected form element</param>
        public CreateLabels_Form(Form form)
        {
            this.form = form;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Caption
            this.form.FormDesign.Caption = this.labelManager.createLabel();
        }
    }

    public class CreateLabels_FormControl : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current view
        /// </summary>
        protected FormControl control;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="form">Selected form element</param>
        public CreateLabels_FormControl(FormControl formControl)
        {
            this.control = formControl;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            switch (control.Type)
            {
                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.String:
                    FormStringControl stringControl = control as FormStringControl;
                    stringControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.CheckBox:
                    FormCheckBoxControl checkboxControl = control as FormCheckBoxControl;
                    checkboxControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Group:
                    FormGroupControl groupControl = control as FormGroupControl;
                    groupControl.Caption = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Button:
                    FormButtonControl buttonControl = control as FormButtonControl;
                    buttonControl.Text = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Real:
                    FormRealControl realControl = control as FormRealControl;
                    realControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Integer:
                    FormIntegerControl integerControl = control as FormIntegerControl;
                    integerControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ComboBox:
                    FormComboBoxControl comboboxControl = control as FormComboBoxControl;
                    comboboxControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Image:
                    FormImageControl imageControl = control as FormImageControl;
                    imageControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Date:
                    FormDateControl dateControl = control as FormDateControl;
                    dateControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.RadioButton:
                    FormRadioButtonControl radioControl = control as FormRadioButtonControl;
                    radioControl.Caption = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ButtonGroup:
                    FormButtonGroupControl buttonGroupCaption = control as FormButtonGroupControl;
                    buttonGroupCaption.Caption = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.TabPage:
                    FormTabPageControl tabpageControl = control as FormTabPageControl;
                    tabpageControl.Caption = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.CommandButton:
                    FormCommandButtonControl commandbuttonControl = control as FormCommandButtonControl;
                    commandbuttonControl.Text = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.MenuButton:
                    FormMenuButtonControl menubuttonControl = control as FormMenuButtonControl;
                    menubuttonControl.Text = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.MenuFunctionButton:
                    FormMenuFunctionButtonControl menufunctionControl = control as FormMenuFunctionButtonControl;
                    menufunctionControl.Text = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ListBox:
                    FormListBoxControl listboxControl = control as FormListBoxControl;
                    listboxControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Time:
                    FormTimeControl timeControl = control as FormTimeControl;
                    timeControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ButtonSeparator:
                    FormButtonSeparatorControl buttonseparatorControl = control as FormButtonSeparatorControl;
                    buttonseparatorControl.Text = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Guid:
                    FormGuidControl guidControl = control as FormGuidControl;
                    guidControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.Int64:
                    FormInt64Control int64Control = control as FormInt64Control;
                    int64Control.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.DateTime:
                    FormDateTimeControl datetimeControl = control as FormDateTimeControl;
                    datetimeControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ActionPane:
                    FormActionPaneControl actionpaneControl = control as FormActionPaneControl;
                    actionpaneControl.Caption = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ActionPaneTab:
                    FormActionPaneTabControl actionpanetabControl = control as FormActionPaneTabControl;
                    actionpanetabControl.Caption = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.SegmentedEntry:
                    FormSegmentedEntryControl segmentedEntryControl = control as FormSegmentedEntryControl;
                    segmentedEntryControl.Label = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.DropDialogButton:
                    FormDropDialogButtonControl dropDialogButtonControl = control as FormDropDialogButtonControl;
                    dropDialogButtonControl.Text = this.labelManager.createLabel();
                    break;

                case Microsoft.Dynamics.AX.Metadata.Core.MetaModel.FormControlType.ReferenceGroup:
                    FormReferenceGroupControl referenceGroupControl = control as FormReferenceGroupControl;
                    referenceGroupControl.Label = this.labelManager.createLabel();
                    break;

                default:
                    throw new NotImplementedException($"Form control type {control.Type.ToString()} is not implemented.");
            }
        }
    }

    /// <summary>
    /// Creates labels to security privileges and child elements
    /// </summary>
    public class CreateLabels_SecurityPrivilege : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current menuitem
        /// </summary>
        protected SecurityPrivilege securityPrivilege;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="securityPrivilege">Selected SecurityPrivilege element</param>
        public CreateLabels_SecurityPrivilege(SecurityPrivilege securityPrivilege)
        {
            this.securityPrivilege = securityPrivilege;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.securityPrivilege.Label = this.labelManager.createLabel();
        }
    }

    /// <summary>
    /// Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowHierarchyAssignmentProvider : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current provider
        /// </summary>
        protected WorkflowHierarchyAssignmentProvider provider;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="provider">Selected provider element</param>
        public CreateLabels_WorkflowHierarchyAssignmentProvider(WorkflowHierarchyAssignmentProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.provider.Label = this.labelManager.createLabel();
            
        }
    }

    /// <summary>
    /// Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowApproval : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current approval
        /// </summary>
        protected WorkflowApproval approval;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="approval">Selected approval element</param>
        public CreateLabels_WorkflowApproval(WorkflowApproval approval)
        {
            this.approval = approval;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.approval.Label = this.labelManager.createLabel();
            
        }
    }
    /// <summary>
    /// Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowCategory : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current category
        /// </summary>
        protected WorkflowCategory category;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="category">Selected category element</param>
        public CreateLabels_WorkflowCategory(WorkflowCategory category)
        {
            this.category = category;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.category.Label = this.labelManager.createLabel();
            
        }
    }

    /// <summary>
    /// Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowTask : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current task
        /// </summary>
        protected WorkflowTask task;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="category">Selected task element</param>
        public CreateLabels_WorkflowTask(WorkflowTask task)
        {
            this.task = task;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.task.Label = this.labelManager.createLabel();
            
        }
    }

    /// <summary>
    /// Creates labels to workflow object
    /// </summary>
    public class CreateLabels_WorkflowType : CreateLabels
    {
        /// <summary>
        /// Global variable representing the current type
        /// </summary>
        protected WorkflowTemplate type;

        /// <summary>
        /// Initiaze the global variable
        /// </summary>
        /// <param name="type">Selected task element</param>
        public CreateLabels_WorkflowType(WorkflowTemplate type)
        {
            this.type = type;
        }

        /// <summary>
        /// Run the process
        /// </summary>
        public override void run()
        {
            // Label
            this.type.Label = this.labelManager.createLabel();
            
        }
    }

}
