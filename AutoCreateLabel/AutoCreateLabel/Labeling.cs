using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.Labels;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;

namespace Labeling
{
    public class LabelManager
    {
        public string LABELFILEID { set; get; }// TODO: change to your label file name
        public string labelId { set; get; }
        public string label_enus { set; get; }
        public string secLabel{ set; get; }
        public string secLanguage { set; get; }
        private Logging.Logging logging { get; }

        private List<AxLabelFile> labelFiles;

        protected EnvDTE.DTE dte;
        protected VSApplicationContext context;

        public LabelManager()
        {
            this.logging = new Logging.Logging();
            this.labelFiles = new List<AxLabelFile>();

            this.dte = CoreUtility.ServiceProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            this.context = new VSApplicationContext(dte);
            
        }
        

        /// <summary>
        /// If property text is prefixed, then create a new label.
        /// Otherwise, return the very same property text value
        /// </summary>
        /// <param name="propertyText">Label text from element property (label, help text, caption, etc)</param>
        /// <returns>The new label id created</returns>
        public string createLabel()
        {
            AxLabelFile labelfile = LabelManager.currentModel().GetLabelFile($"{LABELFILEID}_{"en-US"}");
            
            LabelControllerFactory factory = new LabelControllerFactory();
            LabelEditorController labelEditorController = factory.GetOrCreateLabelController(labelfile, context);

            if (!labelEditorController.Exists(labelId))
            {
                labelEditorController.Insert(labelId, label_enus, string.Empty);
                labelEditorController.Save();

                this.log(labelId, label_enus, labelfile.Name);
            }

            labelfile = LabelManager.currentModel().GetLabelFile($"{LABELFILEID}_{secLanguage}");

            factory = new LabelControllerFactory();
            labelEditorController = factory.GetOrCreateLabelController(labelfile, context);

            if (!labelEditorController.Exists(labelId))
            {
                labelEditorController.Insert(labelId, secLabel, string.Empty);
                labelEditorController.Save();

                this.log(labelId, secLabel, labelfile.Name);
            }
            return $"@{LABELFILEID}:{labelId}";
            
        }

        /// <summary>
        /// Add log message
        /// </summary>
        /// <param name="labelId">Label id</param>
        /// <param name="label">Label text</param>
        /// <param name="labelFileName">Label file name</param>
        private void log(string labelId, string label, string labelFileName)
        {
            Logging.Log singleLog = new Logging.Log();

            singleLog.labelId = labelId;
            singleLog.label = label;
            singleLog.labelFile = labelFileName;

            this.logging.add(singleLog);
        }
        
        /// <summary>
        /// Get formated log message
        /// </summary>
        /// <returns>Log message</returns>
        public string getLoggingMessage()
        {
            return this.logging.getLogging();
        }

        /// <summary>
        /// Used to find metadata on current model
        /// </summary>
        /// <returns>MetaModelService instance</returns>
        /// <remarks>For sure there is a better way to do it</remarks>
        static protected Microsoft.Dynamics.AX.Metadata.Service.IMetaModelService currentModel()
        {
            return DesignMetaModelService.Instance.CurrentMetaModelService;
        }

        /// <summary>
        /// Used to find metadata on model (by model name)
        /// </summary>
        /// <param name="modelName">Model name</param>
        /// <returns>MetaModelService instance</returns>
        /// /// <remarks>For sure there is a better way to do it</remarks>
        /*static protected Microsoft.Dynamics.AX.Metadata.Service.IMetaModelService model(string modelName)
        {
            var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            var metaModelService = metaModelProviders.GetMetaModelService(modelName);

            return metaModelService;
        }*/
    }
}
