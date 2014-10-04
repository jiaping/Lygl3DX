using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lygl.UI.Controls
{
    public abstract class NumericBoxBase<T> : TextBox where T : struct
    {
        private const string DEFAULT_FORMATSTRING = "G";
        private const string DEFAULT_NULLTEXT = "";

        public NumericBoxBase()
        {
            this.NullText = DEFAULT_NULLTEXT;
            this.FormatString = DEFAULT_FORMATSTRING;
        }

        private T? _value;

        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Bindable(BindableSupport.Yes)]
        public T? Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                this.Text = value.HasValue
                    ? this.ToText(value.Value)
                    : this.NullText;
                this.OnValueChanged(EventArgs.Empty);
            }
        }

        protected abstract bool CanParse(string text);
        protected abstract T? DoParse(string text);
        protected abstract string ToText(T value);

        private static readonly object EVENT_VALUECHANGED = new object();



      

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(T?), typeof(NumericBoxBase<T>), new PropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
        }



        public void OnValueChanged(EventArgs e)
        {
            EventHandler handler = base.Events[EVENT_VALUECHANGED] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
            //Debug.Print("New value: {0}", this.Value);
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        private int _eventActive = 0;
        protected override void OnTextChanged(EventArgs e)
        {
            if (_eventActive > 0)
            {
                return;
            }
            try
            {
                this._eventActive++;
                // See if new value is a valid Money value
                if (!IsValid(this.Text))
                {
                    this.GoToLastValidText();
                    return;
                }

                base.OnTextChanged(e);

                if (!IsValid(this.Text))
                {
                    this.GoToLastValidText();
                    return;
                }
                else
                {
                    StoreAsValidText();
                }

                //Debug.Print("Text   : {0}", this.Text);
                this.OnValueChanged(e);
            }
            finally
            {
                this._eventActive--;
            }
        }

        private string _lastValidText = string.Empty;
        private int _lastValidSelectionStart = 0;
        private int _lastValidSelectionLength = 0;

        private void GoToLastValidText()
        {
            this.Text = this._lastValidText;
            this.SelectionStart = this._lastValidSelectionStart;
            this.SelectionLength = this._lastValidSelectionLength;
        }

        private void StoreAsValidText()
        {
            this._value = this.CanParse(this.Text)
                ? this.DoParse(this.Text)
                : null;

            this._lastValidText = this.Text;
            this._lastValidSelectionStart = this.SelectionStart;
            this._lastValidSelectionLength = this.SelectionLength;
        }

        public bool IsValid(string text)
        {
            if (text == this.NullText)
            {
                return true;
            }
            if (!this.CanParse(this.Text))
            {
                return false;
            }
            return true;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (this._eventActive > 0)
            {
                return;
            }
            try
            {
                this._eventActive++;

                this.Text = this.Value.HasValue ? this.ToText(this.Value.Value) : this.NullText;
                this.StoreAsValidText();
            }
            finally
            {
                this._eventActive--;
            }
        }

        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            if (this._eventActive > 0)
            {
                return;
            }
            try
            {
                this._eventActive++;
                if (this.IsValid(this.Text))
                {
                    this.StoreAsValidText();
                }
                else
                {
                    this.GoToLastValidText();
                }

            }
            finally
            {
                this._eventActive--;
            }
        }

        [DefaultValue(DEFAULT_NULLTEXT)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual string NullText { get; set; }

        [DefaultValue(DEFAULT_FORMATSTRING)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual string FormatString { get; set; }

    }
}
