using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Night.Comlib.Services
{
    [DataContract]
    public class RuleViolation
    {
        [DebuggerStepThrough]
        public RuleViolation(string parameterName, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage), "the error message is null or empty");
            }

            ParameterName = parameterName;
            ErrorMessage = errorMessage;
        }

        [DataMember]
        public string ParameterName
        {
            get;
            private set;
        }

        [DataMember]
        public string ErrorMessage
        {
            get;
            private set;
        }
    }
}
