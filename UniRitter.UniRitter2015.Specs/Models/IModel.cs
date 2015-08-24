using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniRitter.UniRitter2015.Specs.Models
{
    public interface IModel
    {
        bool AttributeEquals(IModel compareTo);
        bool IsEquals(IModel compareTo);
        int GetHashCode();
        Guid? GetId();
    }
}