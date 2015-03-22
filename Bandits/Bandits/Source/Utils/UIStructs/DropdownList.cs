using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Utils
{
    public class DropdownListStruct<T>
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public T NativeValue
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Value))
                {
                    return (T)Convert.ChangeType(Value, typeof(T));
                }

                return default(T);
            }
        }
    }
}