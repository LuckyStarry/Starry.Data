/* MIT License
 * Copyright (c) 2016 Sun Bo
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Data
{
    /// <summary>
    /// DbBridge is a object which used to bridge Starry.Data's features.
    /// </summary>
    public sealed partial class DbBridge : IDbBridge
    {
        private IDbColumnMappingFactory mappingFactory;
        IDbColumnMappingFactory IDbBridge.MappingFactory
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.mappingFactory = value;
            }
            get { return this.mappingFactory; }
        }
        /// <summary>
        /// Gets and sets the factory used to generate <see cref="Starry.Data.IDbColumnMapping"/>
        /// </summary>
        public static IDbColumnMappingFactory MappingFactory
        {
            set { DbBridge.Instance.MappingFactory = value; }
            get { return DbBridge.Instance.MappingFactory; }
        }
    }
}
