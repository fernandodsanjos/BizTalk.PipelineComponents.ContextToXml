using System;
using System.Collections.Generic;
using System.Resources;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.ScalableTransformation;
using Microsoft.XLANGs.RuntimeTypes;
using BizTalkComponents.Utils;
using System.Globalization;

namespace BizTalkComponents.PipelineComponents
{
    /// <summary>
    ///  Transforms original message stream using streaming scalable transformation via provided map specification.
    /// </summary>
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    [System.Runtime.InteropServices.Guid("45A34C0D-8D73-45fd-960D-DB365CD56379")]
    public partial class ContextToXml : IBaseComponent
    {
       
       

        public ContextToXml()
        {
           
            
        }

        #region IComponent Members

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            MemoryStream mem = new MemoryStream();
            XmlWriter wtr = XmlWriter.Create(mem);

            uint propCount = pInMsg.Context.CountProperties;
            string name = String.Empty;
            string ns = String.Empty;

            wtr.WriteStartDocument();
            wtr.WriteStartElement("ContextCollection", "http://BizTalk.PipelineComponents.Context");

            for (int i = 0; i < propCount; i++)
            {
                wtr.WriteStartElement("Context");

                object obj = pInMsg.Context.ReadAt(i ,out name,out ns);

                TypeCode typeCode = Type.GetTypeCode(obj.GetType());

                wtr.WriteAttributeString("Name", name);
                wtr.WriteAttributeString("Type", typeCode.ToString());
                wtr.WriteAttributeString("Namespace", ns);

                wtr.WriteString(Convert.ToString(obj));

                wtr.WriteEndElement();
            }

            wtr.WriteEndElement();
            wtr.WriteEndDocument();

            wtr.Flush();
            mem.Position = 0;

            pInMsg.BodyPart.Data = mem;

            return pInMsg;
        }



        #endregion

        #region Private methods

       

      
        #endregion

        /// <summary>
        /// Loads configuration property for component.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="errlog">Error status (not used in this code).</param>
        public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
        {
            return;

        }

        /// <summary>
        /// Saves current component configuration into the property bag.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="fClearDirty">Not used.</param>
        /// <param name="fSaveAllProperties">Not used.</param>
        public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
            return;

        }
       
    }


    
}
