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
namespace Starry.Data
{
    /// <summary>
    /// Represents a object used to get the value from <see cref="System.Data.IDataReader"/>.
    /// </summary>
    public interface IDbColumnMapping
    {
        /// <summary>
        /// Gets the value from <see cref="System.Data.IDataReader"/>.
        /// </summary>
        /// <param name="dataReader"><see cref="System.Data.IDataReader"/></param>
        /// <returns>Value from <see cref="System.Data.IDataReader"/>.</returns>
        object GetValue(System.Data.IDataReader dataReader);
    }
    /// <summary>
    /// Represents a object used to get the value from <see cref="System.Data.IDataReader"/> and change its type to T.
    /// </summary>
    /// <typeparam name="T">The type of Data</typeparam>
    public interface IDbColumnMapping<T> : IDbColumnMapping
    {
        /// <summary>
        /// Gets the value from <see cref="System.Data.IDataReader"/> and change its type to T.
        /// </summary>
        /// <param name="dataReader"><see cref="System.Data.IDataReader"/></param>
        /// <returns>Value from <see cref="System.Data.IDataReader"/>.</returns>
        new T GetValue(System.Data.IDataReader dataReader);
    }
}
