using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Daisy.DaisyConverter.DaisyConverterLib
{
    public class BoolDataType
    {
        private bool m_Val;
        private ScriptParameter m_Parameter;
        private readonly string m_strTrue_Val  = "true";    // string value being used by true flag in script
        private readonly string m_strFalse_Val = "false";   // string value being used by False flag in script

        public BoolDataType(ScriptParameter p, XmlNode DataTypeNode)
        {
            m_Parameter = p;
            XmlNode FirstChild = DataTypeNode.FirstChild;
            m_Val = Convert.ToBoolean(p.ParameterValue);
            if (FirstChild.Attributes.Count>0)
            {
                m_strTrue_Val  = FirstChild.Attributes.GetNamedItem("true").Value;
                m_strFalse_Val = FirstChild.Attributes.GetNamedItem("false").Value;
            }
        }

        public bool Value
        {
            get { return m_Val; }
            set
            {
                m_Val = value;
                if (value == true)
                    m_Parameter.ParameterValue = m_strTrue_Val;
                else
                    m_Parameter.ParameterValue = m_strFalse_Val;
            }
        }

    }

}
