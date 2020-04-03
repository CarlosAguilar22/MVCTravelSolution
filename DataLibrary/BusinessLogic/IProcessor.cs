using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public enum ClientModelType {
        Travel,
        User
    }

    public interface IProcessor
    {
        int Create(Object travel);
        List<Object> Load();
        Object GetModelById(int Id);
    }
}
