/*-------------------------------------------------------------------------------------------------------------
 * module:			TXTextControl.DocumentServer.Forms
 *
 * copyright:		© Text Control GmbH
 * version:			TextControl 30.0
 *-----------------------------------------------------------------------------------------------------------*/

using System.Drawing;
using System.Xml.Serialization;

namespace TXTextControl.DocumentServer.Forms
{
    /*-------------------------------------------------------------------------------------------------------
	** AcroForm XML base class
    *  This class is used to import the XML structure into a readable object
	**-----------------------------------------------------------------------------------------------------*/
    [XmlRoot("AcroForm")]
    public class XmlAcroForm
    {
        [XmlElement(ElementName = "FieldName")]
        public string FieldName { get; set; }

        [XmlElement(DataType = "string",
        ElementName = "AlternateFieldName")]
        public string AlternateFieldName { get; set; }

        [XmlElement("FieldType", typeof(FieldType))]
        public FieldType FieldType { get; set; }

        [XmlElement(DataType = "string",
        ElementName = "FieldValue")]
        public string FieldValue { get; set; }

        [XmlElement("FieldRect", typeof(XmlRectangle))]
        public Rectangle FieldRect { get; set; }

        [XmlElement("ChoiceFieldOptions", typeof(XmlChoiceFieldOptions))]
        public XmlChoiceFieldOptions ChoiceFieldOptions { get; set; }

        [XmlElement("ChoiceFieldFlags", typeof(XmlChoiceFieldFlags))]
        public XmlChoiceFieldFlags ChoiceFieldFlags { get; set; }

        [XmlElement("ButtonFieldFlags", typeof(XmlButtonFieldFlags))]
        public XmlButtonFieldFlags ButtonFieldFlags { get; set; }
    }

    public class XmlChoiceFieldOptions
    {
        [XmlElement("ChoiceFieldElement")]
        public string[] ChoiceFieldElements { get; set; }
    }

    public class XmlButtonFieldFlags
    {
        [XmlAttribute("IsRadio", DataType = "boolean")]
        public bool IsRadio { get; set; }

        [XmlAttribute("IsPushbutton", DataType = "boolean")]
        public bool IsPushbutton { get; set; }
    }

    public class XmlChoiceFieldFlags
    {
        [XmlAttribute("IsComboBox", DataType = "boolean")]
        public bool IsComboBox { get; set; }

        [XmlAttribute("CanEdit", DataType = "boolean")]
        public bool CanEdit { get; set; }

        [XmlAttribute("Sort", DataType = "boolean")]
        public bool Sort { get; set; }

        [XmlAttribute("MultiSelect", DataType = "boolean")]
        public bool MultiSelect { get; set; }

        [XmlAttribute("DoNotSpellCheck", DataType = "boolean")]
        public bool DoNotSpellCheck { get; set; }

        [XmlAttribute("CommitOnSelChange", DataType = "boolean")]
        public bool CommitOnSelChange { get; set; }
    }

    public struct XmlRectangle
    {
        [XmlAttribute("left", DataType = "int")]
        public int left { get; set; }

        [XmlAttribute("right", DataType = "int")]
        public int right { get; set; }

        [XmlAttribute("top", DataType = "int")]
        public int top { get; set; }

        [XmlAttribute("bottom", DataType = "int")]
        public int bottom { get; set; }

        public static implicit operator Rectangle(XmlRectangle xmlRectangle)
        {
            return new Rectangle(xmlRectangle.left, xmlRectangle.top, xmlRectangle.left + xmlRectangle.right, xmlRectangle.top - xmlRectangle.bottom);
        }

        public static implicit operator XmlRectangle(Rectangle rectangle)
        {
            return new XmlRectangle() { left = rectangle.Left, right = rectangle.Right, top = rectangle.Top, bottom = rectangle.Bottom };
        }
    }
}

