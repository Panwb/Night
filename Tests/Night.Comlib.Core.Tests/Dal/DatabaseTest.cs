﻿using Xunit;

namespace Night.Comlib.Core.DAL.Tests
{
    public class DatabaseTest
    {
        [Fact]
        public void Query()
        {
            var sql = "select * from stu_info";
            var result = new Database().Query(sql);

            Assert.NotNull(result);
        }
    }
}