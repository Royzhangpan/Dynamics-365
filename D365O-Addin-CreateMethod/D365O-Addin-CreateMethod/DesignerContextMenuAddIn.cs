namespace D365O_Addin_CreateMethod
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Microsoft.Dynamics.AX.Metadata.Core;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
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
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IClassItem))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    public class DesignerContextMenuAddIn : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "DesignerD365O_Addin_CreateMethod";
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
                VSProjectNode  activeProjectNode = GetActiveProjectNode(dte);
                ModelInfo modelInfo = activeProjectNode.GetProjectsModelInfo(true);
                
                UserInterface userInterface = new UserInterface(e);

                userInterface.ShowDialog();
                if (userInterface.closeOk != true)
                {
                    return;
                }

                List<string> classMethod = userInterface.methods.classMethod;
                List<string> tableMethod = userInterface.methods.tableMethod;
                List<parmMethodContract> parmMethod = userInterface.methods.parmMethod;

                if (e.SelectedElement is IClassItem)
                {
                    IClassItem classItem = e.SelectedElement as IClassItem;
                    AxClass axClass = DesignMetaModelService.Instance.GetClass(classItem.Name);

                    

                    #region add pack and unpack

                    if (classMethod != null &&
                       classMethod.Contains("pack") &&
                       !axClass.Methods.Contains("pack") && 
                       !axClass.Methods.Contains("unpack"))
                    {
                        string classDeclaration = axClass.Declaration;

                        classDeclaration = classDeclaration.Insert(classDeclaration.Length - 1, AddinResources.packMacro.Replace(@"\t", "\t"));
                        axClass.Declaration = classDeclaration;
                        AxMethod axMethod = new AxMethod();
                        axMethod.Name = "pack";
                        axMethod.Source = AddinResources.pack.Replace(@"\t", "\t");
                        axClass.AddMethod(axMethod);

                        axMethod = new AxMethod();
                        axMethod.Name = "unpack";
                        axMethod.Source = AddinResources.unpack.Replace(@"\t", "\t");
                        axClass.AddMethod(axMethod);
                    }

                    if (classMethod != null &&
                        classMethod.Contains("run"))
                    {
                        AxMethod axMethod = new AxMethod();
                        axMethod.Name = "run";
                        axMethod.Source = AddinResources.run.Replace(@"\t", "\t");
                        axClass.AddMethod(axMethod);
                    }
                    #endregion
                    #region add parm method for class

                    if (parmMethod != null)
                    {
                        foreach (parmMethodContract contract in parmMethod)
                        {
                            if (axClass.Methods.Contains(contract.methodName))
                            {
                                continue;
                            }
                            AxMethod axMethod = new AxMethod();
                            AxClassMemberVariable axClassMemberVariable = new AxClassMemberVariable();
                            axClassMemberVariable.Name = contract.methodName.First().ToString().ToLower() + contract.methodName.Substring(1);
                            axClassMemberVariable.Type = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.CompilerBaseType.ExtendedDataType;
                            axClassMemberVariable.TypeName = contract.edtName;


                            axClass.AddMember(axClassMemberVariable);

                            string classDeclaration = axClass.Declaration;
                            int version = classDeclaration.IndexOf("#");
                            if (version > 0)
                            {
                                classDeclaration = classDeclaration.Insert(version - 1, string.Format("\t{0}\t\t{1};\n", contract.edtName, axClassMemberVariable.Name));
                            }
                            else
                            {
                                classDeclaration = classDeclaration.Insert(classDeclaration.Length, string.Format("\t{0}\t\t{1};\n", contract.edtName, axClassMemberVariable.Name));
                            }

                            if (contract.pack)
                            {
                                int n1, n2;
                                n1 = classDeclaration.IndexOf("#localmacro.CurrentList", 0) + "#localmacro.CurrentList".Length;   //开始位置
                                n2 = classDeclaration.IndexOf("#endmacro", n1);               //结束位置
                                string packList = classDeclaration.Substring(n1, n2 - n1);
                                var packVar = packList.Replace("\t", "").Replace("\n", "").Replace("\r", "").Replace(" ", "").Split(new char[] { ',' }).ToList();

                                string newPackList = string.Empty;
                                if (!packVar.Contains(axClassMemberVariable.Name))
                                {
                                    packVar.Add(axClassMemberVariable.Name);
                                    int i = 1;
                                    foreach (string varName in packVar)
                                    {
                                        if (i < packVar.Count && packVar.Count > 1)
                                        {
                                            newPackList += "\n\t\t" + varName + ",";
                                        }
                                        else
                                        {
                                            newPackList += "\n\t\t" + varName + "\n\t";
                                        }
                                        i++;
                                    }
                                }

                                classDeclaration = classDeclaration.Replace(packList, newPackList);
                            }
                            axClass.Declaration = classDeclaration;

                            if (!string.IsNullOrEmpty(contract.dataMember))
                            {
                                AxAttribute axAttribute = new AxAttribute();
                                AxAttributeParameter axAttributeParameter = new AxAttributeParameter();
                                axAttributeParameter.Type = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.AttributeParameterBaseType.String;
                                axAttributeParameter.TypeValue = contract.dataMember;
                                axAttribute.Name = "DataMember";
                                axAttribute.AddParameter(axAttributeParameter);
                                axMethod.AddAttribute(axAttribute);
                            }

                            AxMethodParameter axMethodParameter = new AxMethodParameter();
                            axMethodParameter.Type = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.CompilerBaseType.ExtendedDataType;
                            axMethodParameter.TypeName = contract.edtName;
                            axMethodParameter.DefaultValue = axClassMemberVariable.Name;
                            axMethodParameter.Name = string.Format("_{0}", contract.methodName);


                            AxMethodReturnType axMethodReturnType = new AxMethodReturnType();
                            axMethodReturnType.TypeName = contract.edtName;
                            axMethodReturnType.Type = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.CompilerBaseType.ExtendedDataType;

                            axMethod.AddParameter(axMethodParameter);
                            axMethod.Name = "parm" + contract.methodName.First().ToString().ToUpper() + contract.methodName.Substring(1);
                            axMethod.Name = axMethod.Name.Replace(" ", string.Empty);
                            axMethod.ReturnType = axMethodReturnType;

                            if (!string.IsNullOrEmpty(contract.dataMember))
                            {
                                axMethod.Source = AddinResources.parm.Replace(@"\t", "\t").Replace("{MemberValue}", string.Format("'{0}'", contract.dataMember)).Replace(
                                                    "{DataType}", axMethodReturnType.TypeName).Replace(
                                                    "{MethodName}", axMethod.Name).Replace(
                                                    "{VarName}", axClassMemberVariable.Name);
                            }
                            else
                            {
                                axMethod.Source = AddinResources.parmNoDataMember.Replace(@"\t", "\t").Replace(
                                                    "{DataType}", axMethodReturnType.TypeName).Replace(
                                                    "{MethodName}", axMethod.Name).Replace(
                                                    "{VarName}", axClassMemberVariable.Name);
                            }



                            axClass.AddMethod(axMethod);
                        }
                    }
                    #endregion;

                    #region Add construct method for class

                    if (classMethod != null &&
                        classMethod.Contains("construct") &&
                        !axClass.Methods.Contains("construct"))
                    {
                        AxMethod axMethod = new AxMethod();
                        AxMethodReturnType axMethodReturnType = new AxMethodReturnType();
                        axMethodReturnType.TypeName = classItem.Name;
                        axMethodReturnType.Type = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.CompilerBaseType.Class;
                        axMethod.Name = "construct";
                        axMethod.IsStatic = true;
                        axMethod.ReturnType = axMethodReturnType;
                        axMethod.Source = AddinResources.construct.Replace(@"\t", "\t").Replace("{className}", classItem.Name);
                        axClass.AddMethod(axMethod);
                    }
                    #endregion

                    #region Add main method for class
                    if (classMethod != null &&
                        classMethod.Contains("main") &&
                        !axClass.Methods.Contains("main"))
                    {
                        AxMethodReturnType axMethodReturnType = new AxMethodReturnType();
                        axMethodReturnType.TypeName = classItem.Name;
                        axMethodReturnType.Type = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.CompilerBaseType.Void;

                        AxMethodParameter axMethodParameter = new AxMethodParameter();
                        axMethodParameter.Name = "_args";
                        axMethodParameter.Type = Microsoft.Dynamics.AX.Metadata.Core.MetaModel.CompilerBaseType.Class;
                        axMethodParameter.TypeName = "Args";

                        AxMethod axMethod = new AxMethod();
                        axMethod.Name = "main";
                        axMethod.IsStatic = true;
                        axMethod.ReturnType = axMethodReturnType;
                        axMethod.AddParameter(axMethodParameter);
                        string varName = classItem.Name.Replace("HSO", string.Empty);
                        varName = varName.First().ToString().ToLower() + varName.Substring(1);
                        axMethod.Source = AddinResources.main.Replace(@"\t", "\t").Replace("{className}", classItem.Name).Replace("{varName}", varName);
                        axClass.AddMethod(axMethod);
                    }
                    #endregion
                    var metaModelService = DesignMetaModelService.Instance.CurrentMetaModelService;
                    metaModelService.UpdateClass(axClass, new ModelSaveInfo(modelInfo));
                }
                if (e.SelectedElement is ITable)
                {
                    ITable table = e.SelectedElement as ITable;
                    AxTable axTable = DesignMetaModelService.Instance.GetTable(table.Name);
                    AxTableIndex axTableIndex = null;
                    foreach (AxTableIndex index in axTable.Indexes)
                    {
                        if (string.Equals(index.Name, axTable.PrimaryIndex))
                        {
                            axTableIndex = index;
                        }
                    }
                    string axTabelVar = axTable.Name.Replace("HSO", "").First().ToString().ToLower() + axTable.Name.Replace("HSO", "").Substring(1);
                    if (axTableIndex == null)
                    {
                        throw new Exception(string.Format("Please set a primary index for the table {0} first.", axTable.Name));
                    }
                    List<AxTableField> primaryFields = new List<AxTableField>();
                    foreach (AxTableIndexField indexField in axTableIndex.Fields)
                    {
                        foreach (AxTableField tableField in axTable.Fields)
                        {
                            if (string.Equals(tableField.Name, indexField.Name))
                            {
                                primaryFields.Add(tableField); 
                            }
                        }
                    }

                    #region find method
                    if (tableMethod != null &&
                        tableMethod.Contains("find"))
                    {
                        string parms = string.Empty;
                        string fieldNames = string.Empty;
                        string where = string.Empty;

                        foreach (AxTableField axTableField in primaryFields)
                        {
                            string axfieldNameVar = axTableField.Name.First().ToString().ToLower() + axTableField.Name.Substring(1);
                            parms += axTableField.ExtendedDataType + " _" + axfieldNameVar + ", ";
                            if (string.IsNullOrEmpty(fieldNames))
                            {
                                fieldNames = "_" + axfieldNameVar;
                            }
                            else
                            {
                                fieldNames += " && _" + axfieldNameVar;
                            }

                            if (string.IsNullOrEmpty(where))
                            {
                                where = "where " + axTabelVar + "." + axTableField.Name + " == _" + axfieldNameVar;
                            }
                            else
                            {
                                where += "\n\t\t\t\t&& " + axTabelVar + "." + axTableField.Name + " == _" + axfieldNameVar;
                            }
                        }
                        AxMethod axMethod = new AxMethod();
                        axMethod.Name = "find";
                        axMethod.Source = AddinResources.find.Replace(@"\t", "\t").Replace("{TableName}", axTable.Name)
                                                             .Replace("{VarName}", axTabelVar)
                                                             .Replace("{parms}", parms)
                                                             .Replace("{FieldNames}", fieldNames)
                                                             .Replace("{Where}", where);

                        axTable.AddMethod(axMethod);
                                                            
                    }
                    #endregion

                    #region exist method
                    if (tableMethod != null
                        && tableMethod.Contains("exist"))
                    {
                        string parms = string.Empty;
                        string fieldNames = string.Empty;
                        string where = string.Empty;
                        foreach (AxTableField axTableField in primaryFields)
                        {
                            string axfieldNameVar = axTableField.Name.First().ToString().ToLower() + axTableField.Name.Substring(1);

                            if (string.IsNullOrEmpty(parms))
                            {
                                parms = axTableField.ExtendedDataType + " _" + axfieldNameVar;
                            }
                            else
                            {
                                parms += ", " + axTableField.ExtendedDataType + " _" + axfieldNameVar;
                            }
                            if (string.IsNullOrEmpty(fieldNames))
                            {
                                fieldNames = "_" + axfieldNameVar;
                            }
                            else
                            {
                                fieldNames += " && _" + axfieldNameVar;
                            }

                            if (string.IsNullOrEmpty(where))
                            {
                                where = "where " + axTable.Name + "." + axTableField.Name + " == _" + axfieldNameVar;
                            }
                            else
                            {
                                where += "\n\t\t\t\t&& " + axTable.Name + "." + axTableField.Name + " == _" + axfieldNameVar;
                            }
                        }
                        string source = "\tstatic boolean exist(" + parms + ")\n\t{\n\t\treturn " + fieldNames + " && (select firstonly RecId from " + axTable.Name + "\n\t\t\tindex hint " + axTable.PrimaryIndex
                                + "\n\t\t\t " + where + ").RecId !=0;\n\t}";

                        AxMethod axMethod = new AxMethod();
                        axMethod.Name = "exist";
                        axMethod.Source = source;

                        axTable.AddMethod(axMethod);
                    }
                    #endregion

                    var metaModelService = DesignMetaModelService.Instance.CurrentMetaModelService;
                    metaModelService.UpdateTable(axTable, new ModelSaveInfo(modelInfo));
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
    }
}