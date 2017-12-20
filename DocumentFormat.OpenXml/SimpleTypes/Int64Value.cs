﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Xml;

namespace DocumentFormat.OpenXml
{
    /// <summary>
    /// Represents the Int64 value for attributes.
    /// </summary>
    [DebuggerDisplay("{InnerText}")]
    public class Int64Value : OpenXmlSimpleValue<Int64>
    {
        /// <summary>
        /// Initializes a new instance of the Int64Value class.
        /// </summary>
        public Int64Value()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Int64Value class using the supplied Int64 value.
        /// </summary>
        /// <param name="value">The Int64 value.</param>
        public Int64Value(Int64 value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Int64Value by deep copying the supplied Int64Value class.
        /// </summary>
        /// <param name="source">The source Int64Value class.</param>
        public Int64Value(Int64Value source)
            : base(source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
        }

        /// <summary>
        /// Gets or sets the inner XML text.
        /// </summary>
        public override string InnerText
        {
            get
            {
                if (this.TextValue == null && this.InnerValue.HasValue)
                {
                    // this.TextValue = this._value.ToString();
                    this.TextValue = XmlConvert.ToString(this.InnerValue.Value);
                }
                else
                {
                    Debug.Assert(this.TextValue == null && !this.InnerValue.HasValue ||
                                 this.TextValue != null && !this.InnerValue.HasValue ||
                                 this.TextValue != null && this.TextValue == this.InnerValue.ToString() ||
                                // special case: signed number like text is "+5", value is 5
                                 this.TextValue != null && this.TextValue == "+" + this.InnerValue.ToString());
                }
                return this.TextValue;
            }
        }

        /// <summary>
        /// Convert the text to meaningful value.
        /// </summary>
        internal override void Parse()
        {
            this.InnerValue = XmlConvert.ToInt64(this.TextValue);
        }

        /// <summary>
        /// Convert the text to meaningful value.
        /// </summary>
        /// <returns></returns>
        internal override bool TryParse()
        {
            Int64 value;
            this.InnerValue = null;

            try
            {
                value = XmlConvert.ToInt64(this.TextValue);
                this.InnerValue = value;
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        /// <summary>
        /// Implicitly converts the specified Int64Value to an Int64 value.
        /// </summary>
        /// <param name="xmlAttribute">The Int64Value to convert.</param>
        /// <returns>The converted Int64 value.</returns>
        /// <exception cref="InvalidOperationException">Thrown when xmlAttribute is null.</exception>
        public static implicit operator Int64(Int64Value xmlAttribute)
        {
            if (xmlAttribute == null)
            {
                throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
            }
            return ToInt64(xmlAttribute);
        }

        /// <summary>
        /// Implicitly converts an Int64 value to an Int64Value value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        /// <returns>A new Int64Value instance with the value.</returns>
        public static implicit operator Int64Value(Int64 value)
        {
            return FromInt64(value);
        }

        /// <summary>
        /// Returns a new Int64Value object that was created from an Int64 value.
        /// </summary>
        /// <param name="value">An Int64 value to use to create a new Int64Value object.</param>
        /// <returns>An Int64Value that corresponds to the value parameter.</returns>
        public static Int64Value FromInt64(Int64 value)
        {
            return new Int64Value(value);
        }

        /// <summary>
        /// Returns the Int64 representation of an Int64Value object.
        /// </summary>
        /// <param name="xmlAttribute">
        /// An Int64Value object used to retrieve an Int64 representation.
        /// </param>
        /// <returns>An Int64 value that represents an Int64Value object.</returns>
        public static Int64 ToInt64(Int64Value xmlAttribute)
        {
            if (xmlAttribute == null)
            {
                throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
            }

            return xmlAttribute.Value;
        }

        internal override OpenXmlSimpleType CloneImp()
        {
            return new Int64Value(this);
        }
    }
}
