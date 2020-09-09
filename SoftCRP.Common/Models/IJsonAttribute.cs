using System;
using System.Collections.Generic;
using System.Text;

namespace SoftCRP.Common.Models
{
    public interface IJsonAttribute
    {
        object TryConvert(string modelValue, Type targertType, out bool success);
    }
}
