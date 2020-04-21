/*-------------------------------------------------------------------------------------------------------------
 * module:			TXTextControl.DocumentServer.Forms
 *
 * copyright:		© Text Control GmbH
 * version:			TextControl 28.0
 *-----------------------------------------------------------------------------------------------------------*/

using System.Drawing;

namespace TXTextControl.DocumentServer.Forms
{
    /*-------------------------------------------------------------------------------------------------------
	** AcroForm class
	**-----------------------------------------------------------------------------------------------------*/
    public class AcroForm
    {
        /*-------------------------------------------------------------------------------------------------------
	    ** Constructors
	    **-----------------------------------------------------------------------------------------------------*/
        public AcroForm()
        {
        }

        public AcroForm(AcroForm Parent)
        {
            this.AlternateFieldName = Parent.AlternateFieldName;
            this.FieldName = Parent.FieldName;
            this.FieldRect = Parent.FieldRect;
            this.FieldType = Parent.FieldType;
        }

        /*-------------------------------------------------------------------------------------------------------
	    ** Properties
	    **-----------------------------------------------------------------------------------------------------*/
        /// <summary>
		/// Gets or sets the name of the field
		/// </summary>
        public string FieldName { get; set; }
        /// <summary>
		/// Gets or sets the alternate field name of the field
		/// </summary>
        public string AlternateFieldName { get; set; }
        /// <summary>
		/// Gets or sets the type of the field
		/// </summary>
        public FieldType FieldType { get; set; }
        /// <summary>
		/// Gets or sets the rectangle bounds of the field in the document
		/// </summary>
        public Rectangle FieldRect { get; set; }
    }

    /*-------------------------------------------------------------------------------------------------------
    ** AcroFormButton class
    **-----------------------------------------------------------------------------------------------------*/
    public class AcroFormButton : AcroForm
    {
        public AcroFormButton(AcroForm Parent) : base(Parent)
        {
        }
    }

    /*-------------------------------------------------------------------------------------------------------
    ** AcroFormRadioButton class
    **-----------------------------------------------------------------------------------------------------*/
    public class AcroFormRadioButton : AcroFormCheckBox
    {
        public AcroFormRadioButton(AcroForm Parent) : base(Parent)
        {
        }
    }

    /*-------------------------------------------------------------------------------------------------------
    ** AcroFormCheckBox class
    **-----------------------------------------------------------------------------------------------------*/
    public class AcroFormCheckBox : AcroForm
    {
        public AcroFormCheckBox(AcroForm Parent) : base(Parent)
        {
        }

        /// <summary>
		/// Specifies whether the check box is checked or not
		/// </summary>
        public bool IsChecked { get; set; }
    }

    /*-------------------------------------------------------------------------------------------------------
    ** AcroFormListBox class
    **-----------------------------------------------------------------------------------------------------*/
    public class AcroFormListBox : AcroFormComboBox
    {
        public AcroFormListBox(AcroFormChoice Parent) : base(Parent)
        {
        }
    }

    /*-------------------------------------------------------------------------------------------------------
    ** AcroFormComboBox class
    **-----------------------------------------------------------------------------------------------------*/
    public class AcroFormComboBox : AcroFormChoice
    {
        public AcroFormComboBox(AcroFormChoice Parent) : base(Parent)
        {
            this.Options = Parent.Options;
            this.MultiSelect = Parent.MultiSelect;
            this.CanEdit = Parent.CanEdit;
            this.CommitOnSelChange = Parent.CommitOnSelChange;
            this.DoNotSpellCheck = Parent.DoNotSpellCheck;
            this.Sort = Parent.Sort;
            this.Value = Parent.Value;
        }
    }

    /*-------------------------------------------------------------------------------------------------------
    ** AcroFormChoice base class
    **-----------------------------------------------------------------------------------------------------*/
    public class AcroFormTextField : AcroForm
    {
        public AcroFormTextField(AcroForm Parent) : base(Parent)
        {
        }

        /// <summary>
        /// Gets or sets the selected value of a choice list
        /// </summary>
        public string Value { get; set; }
    }

    /*-------------------------------------------------------------------------------------------------------
    ** AcroFormChoice base class
    **-----------------------------------------------------------------------------------------------------*/
    public class AcroFormChoice : AcroForm
    {
        public AcroFormChoice(AcroForm Parent) : base(Parent)
        {
        }

        /// <summary>
        /// Gets or sets the option elements of a choice field
        /// </summary>
        public string[] Options { get; set; }
        /// <summary>
        /// Specifies whether a field can be edited
        /// </summary>
        public bool CanEdit { get; set; }
        /// <summary>
        /// Specifies whether the list can be sorted
        /// </summary>
        public bool Sort { get; set; }
        /// <summary>
        /// Specifies whether multiple options can be selected
        /// </summary>
        public bool MultiSelect { get; set; }
        /// <summary>
        /// Specifies whether spell checking is enabled or not
        /// </summary>
        public bool DoNotSpellCheck { get; set; }
        /// <summary>
        /// Specifies whether the form is submitted when the selected is changed
        /// </summary>
        public bool CommitOnSelChange { get; set; }
        /// <summary>
        /// Gets or sets the selected value of a choice list
        /// </summary>
        public string Value { get; set; }
    }

    /*-------------------------------------------------------------------------------------------------------
    ** FieldType enum
    **-----------------------------------------------------------------------------------------------------*/
    public enum FieldType
    {
		Btn,
		Tx,
		Sig,
		Ch
    }
}

