using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CoreService
{
    public class CoreService : ICoreService
    {
        public float Learn(List<string> values)
        {
            return 10.2f;
        }
    }
}
