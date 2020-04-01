using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class FactoryProcessor
    {
        public static IProcessor CreateProcessor(ClientModelType modelType)
        {
            switch(modelType)
            {
                    case ClientModelType.Travel:
                        {
                            return new TravelProcessor();
                        }
                //case ClientModelType.User:
                //    {

                //    }

                default: return new TravelProcessor();
            }
        }
    }
}
