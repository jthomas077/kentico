using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using CMS.Base;

namespace YOUR.NAMESPACE.HERE
{
    /// <summary>
    /// Abstracts the usage of IDataContainer, thus extending Kentico to custom objects
    /// </summary>
    public abstract class KenticoExtender : IDataContainer
    {
        /// <summary>
        /// Gets a list of column names.
        /// </summary>
        [JsonIgnore]
        public List<string> ColumnNames
        {
            get
            {
                var columnNames = new List<string>();

                foreach (var propertyInfo in GetType().GetProperties())
                {
                    columnNames.Add(propertyInfo.Name);
                }

                return columnNames;
            }
        }

        /// <summary>
        /// Gets or sets the value of the column.
        /// </summary>
        /// <param name="columnName">Column name</param>
        public object this[string columnName]
        {
            get
            {
                return GetValue(columnName);
            }

            set
            {
                SetValue(columnName, value);
            }
        }

        /// <summary>
        /// Returns a boolean value indicating whether the class contains the specified column.
        /// Passes on the specified column's value through the second parameter.
        /// </summary>
        /// <param name="columnName">Column name</param>
        /// <param name="value">Return value</param>
        public bool TryGetValue(string columnName, out object value)
        {
            if (ContainsColumn(columnName))
            {
                value = GetValue(columnName);

                return true;
            }
            else
            {
                value = null;

                return false;
            }
        }

        /// <summary>
        /// Returns true if the class contains the specified column.
        /// </summary>
        /// <param name="columnName">Column name</param>
        public bool ContainsColumn(string columnName)
        {
            return ColumnNames.Contains(columnName);
        }

        /// <summary>
        /// Gets the value of the specified column.
        /// </summary>
        /// <param name="columnName">Column name</param>
        public object GetValue(string columnName)
        {
            try
            {
                return GetType().GetProperty(columnName).GetValue(this);
            }
            catch (Exception)
            {
                throw new ArgumentNullException(String.Format("Column Name: {0} does not exist", columnName));
            }
        }

        /// <summary>
        /// Sets the value of the specified column.
        /// </summary>
        /// <param name="columnName">Column name</param>
        /// <param name="value">New value</param>
        public bool SetValue(string columnName, object value)
        {
            throw new NotImplementedException();
        }
    }
}
