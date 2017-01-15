using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace Night.Comlib.Services
{
    [DataContract]
    public abstract class ServiceResultBase
    {
        [DebuggerStepThrough]
        protected ServiceResultBase(IEnumerable<RuleViolation> ruleViolations)
            : this(ruleViolations, ViolationType.Default)
        {

        }

        [DebuggerStepThrough]
        private ServiceResultBase(IEnumerable<RuleViolation> ruleViolations, ViolationType violationType)
        {
            if(ruleViolations == null)
            {
                throw new ArgumentNullException(nameof(ruleViolations), "The rule violations is null or empty");
            }

            RuleViolations = new List<RuleViolation>(ruleViolations);
            ViolationType = violationType;
        }

        [DataMember]
        public IList<RuleViolation> RuleViolations
        {
            get;
            private set;
        }

        public string MergedErrorMessages()
        {
            return ErrorMessage;
        }

        public bool HasViolation
        {
            get { return RuleViolations.Count > 0; }
        }

        [DataMember]
        public ViolationType ViolationType
        {
            get;
            set;
        }

        private string ErrorMessage
        {
            get
            {
                string msg = string.Empty;
                if (RuleViolations != null && RuleViolations.Count > 0)
                {
                    foreach (RuleViolation item in RuleViolations)
                    {
                        if (msg != string.Empty)
                            msg += Environment.NewLine;
                        msg += item.ErrorMessage;
                    }
                }
                return msg;
            }
        }
    }

    public enum ViolationType
    {
        Default,
        Validation,
        Exception,
        Operational
    }
}
