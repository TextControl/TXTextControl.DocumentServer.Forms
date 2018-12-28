/*-------------------------------------------------------------------------------------------------------------
 * module:			TXTextControl.DocumentServer.Forms
 *
 * copyright:		© Text Control GmbH
 * version:			TextControl 26.0
 *-----------------------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TXTextControl.DocumentServer.Forms
{
    /*-------------------------------------------------------------------------------------------------------
	** PDF class
	**-----------------------------------------------------------------------------------------------------*/
    public class PDF
    {
        /*-------------------------------------------------------------------------------------------------------
	    ** ImportForms method
	    **-----------------------------------------------------------------------------------------------------*/
        public static AcroForm[] ImportForms(string Filename)
        {
            // check, if the file exists
            if (File.Exists(Filename) == false)
                throw new FileNotFoundException("The specified file could not be found.", Filename);

            string sAcroFormsXml;

            // create a temporary ServerTextControl to import the PDF form XML
            using (TXTextControl.ServerTextControl tx = new TXTextControl.ServerTextControl())
            {
                tx.Create();
                TXTextControl.LoadSettings ls = new TXTextControl.LoadSettings();
                ls.PDFImportSettings = TXTextControl.PDFImportSettings.GenerateXML;

                tx.Load(Filename, TXTextControl.StreamType.AdobePDF, ls);

                sAcroFormsXml = tx.Text;
            }

            // new XML document to load the resulting XML
            XmlDocument xmlDocument;
            xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(sAcroFormsXml);

            // create a new list of AcroForm elements
            List<AcroForm> lAcroForm = new List<AcroForm>();

            // loop through the XML and get all AcroForm nodes
            foreach (System.Xml.XmlElement xmlElement in
                xmlDocument.DocumentElement.SelectNodes("AcroForms/AcroForm"))
            {
                // read the XML node
                XmlReader xmlReader = XmlReader.Create(new StringReader(xmlElement.OuterXml));

                // create an XML proxy class according to the XML node
                XmlSerializer s = new XmlSerializer(typeof(XmlAcroForm), new XmlRootAttribute("AcroForm"));
                XmlAcroForm obj = (XmlAcroForm)s.Deserialize(xmlReader);

                // create a new AcroForm object
                AcroForm newAcroForm = new AcroForm()
                {
                    AlternateFieldName = obj.AlternateFieldName,
                    FieldName = obj.FieldName,
                    FieldRect = obj.FieldRect
                };

                // switch between field types
                switch (obj.FieldType)
                {
                    case FieldType.Tx:

                        AcroFormTextField newAcroFormTextField = new AcroFormTextField(newAcroForm);
                        newAcroFormTextField.Value = obj.FieldValue;

                        lAcroForm.Add(newAcroFormTextField);

                        break;

                    case FieldType.Btn: // in case of a button

                        if(obj.ButtonFieldFlags.IsPushbutton == false && // Case CheckBox
                            obj.ButtonFieldFlags.IsRadio == false)
                        {
                            AcroFormCheckBox newAcroFormCheckBox = new AcroFormCheckBox(newAcroForm);
                            newAcroFormCheckBox.IsChecked = "Yes".Equals(obj.FieldValue);

                            lAcroForm.Add(newAcroFormCheckBox);
                        }
                        else if(obj.ButtonFieldFlags.IsPushbutton) // Case PushButton
                        {
                            AcroFormButton newAcroFormButton = new AcroFormButton(newAcroForm);

                            lAcroForm.Add(newAcroFormButton);
                        }
                        else if(obj.ButtonFieldFlags.IsRadio) // Case RadioButton
                        {
                            AcroFormRadioButton newAcroFormRadioButton = new AcroFormRadioButton(newAcroForm);
                            newAcroFormRadioButton.IsChecked = "Yes".Equals(obj.FieldValue);

                            lAcroForm.Add(newAcroFormRadioButton);
                        }

                        break;

                    case FieldType.Ch: // in case of a choice element

                        AcroFormChoice newAcroFormChoice = new AcroFormChoice(newAcroForm);

                        // set all properties 1:1
                        newAcroFormChoice.Options = obj.ChoiceFieldOptions.ChoiceFieldElements;
                        newAcroFormChoice.Sort = obj.ChoiceFieldFlags.Sort;
                        newAcroFormChoice.DoNotSpellCheck = obj.ChoiceFieldFlags.DoNotSpellCheck;
                        newAcroFormChoice.CanEdit = obj.ChoiceFieldFlags.CanEdit;
                        newAcroFormChoice.CommitOnSelChange = obj.ChoiceFieldFlags.CommitOnSelChange;
                        newAcroFormChoice.MultiSelect = obj.ChoiceFieldFlags.MultiSelect;
                        newAcroFormChoice.Value = obj.FieldValue;

                        if (obj.ChoiceFieldFlags.IsComboBox) // Case ComboBox
                        {
                            AcroFormComboBox newAcroFormComboBox = new AcroFormComboBox(newAcroFormChoice);
                            lAcroForm.Add(newAcroFormComboBox);
                        }
                        else // Case ListBox
                        {
                            AcroFormListBox newAcroFormListBox = new AcroFormListBox(newAcroFormChoice);
                            lAcroForm.Add(newAcroFormListBox);
                        }
                        
                        break;
                }
            }

            // return the array of elements
            return lAcroForm.ToArray();
        }
    }
}
