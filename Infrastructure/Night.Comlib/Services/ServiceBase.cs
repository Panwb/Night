using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Night.Comlib.DomainModel;

namespace Night.Comlib.Services
{
    public abstract class ServiceBase<TEntity> : IService<TEntity>
        where TEntity : IEntity
    {
        protected TResult ExecuteCommand<TResult>(Func<TResult> command) where TResult : ServiceResultBase
        {
            var options = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};
            using (var trans = new TransactionScope(TransactionScopeOption.Required, options))
            {
                try
                {
                    var result = command.Invoke();
                    if (result == null)
                    {
                        trans.Complete();
                        return null;
                    }

                    //TODO: Logging the catched exception information

                    if (!result.HasViolation)
                    {
                        trans.Complete();
                    }

                    return result;
                }
                catch (Exception exception)
                {
                    var type = typeof (TResult);
                    if(type.IsAbstract) throw;

                    var instance = Activator.CreateInstance(type) as ServiceResultBase;
                    if (instance != null)
                    {
                        instance.ViolationType = ViolationType.Exception;
                        instance.RuleViolations.Add(new RuleViolation("exception", exception.Message));
                    }

                    //TODO: Logging the catched exception information

                    return instance as TResult;
                }
            }
        }
    }
}
