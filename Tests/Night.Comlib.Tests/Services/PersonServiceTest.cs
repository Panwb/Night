using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Night.Comlib.DAL;
using Night.Comlib.DomainModel;
using Night.Comlib.Services;
using Xunit;

namespace Night.Comlib.Tests.Services
{
    public class PersonService : ServiceBase<Person>
    {
        [Fact]
        public void OutTransactionScope()
        {
            try
            {
                var conn = new Database().Connection;
                conn.Execute("update stu_info set name='Jay2' where id=1");
                throw new Exception("throw error out of transaction scope");
            }
            catch (Exception)
            {

            }

            var connection = new Database().Connection;
            var person = connection.QueryFirst<Person>("select * from stu_info where id=1");
            Assert.True(person.Name == "Jay2");
        }

        [Fact]
        public void InTransactionScope()
        {
            ExecuteCommand<PersonServiceResult>(() =>
            {
                var conn = new Database().Connection;
                conn.Execute("update stu_info set name='Tom2' where id=2");
                throw new Exception("throw error in transaction scope");
            });

            var connection = new Database().Connection;
            var person = connection.QueryFirst<Person>("select * from stu_info where id=2");
            Assert.False(person.Name == "Tom2");
        }
    }

    public class PersonServiceResult : ServiceResultBase
    {
        public PersonServiceResult() : base(new List<RuleViolation>())
        {
        }
    }

    public class Person : EntityBase
    {
        public string Name { get; set; }
    }
}
