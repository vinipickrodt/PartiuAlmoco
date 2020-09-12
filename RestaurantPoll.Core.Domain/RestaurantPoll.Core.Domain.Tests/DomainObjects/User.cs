using Moq;
using PartiuAlmoco.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PartiuAlmoco.Core.Domain.Tests.DomainObjects
{
    public class User
    {
        [Fact]
        public void Users_Must_Have_Valid_Email()
        {
            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    ""
                );
            }));

            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    null
                );
            }));

            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    "john"
                );
            }));

            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    "@"
                );
            }));

            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    "@gmail"
                );
            }));

            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    "@gmail."
                );
            }));

            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    "john@gmail"
                );
            }));

            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    "john@gmail"
                );
            }));

            Assert.ThrowsAny<ArgumentException>(new Action(() =>
            {
                new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                    "John Juarez Jones",
                    "3j",
                    "john@gmail."
                );
            }));

            new Entities.User(new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                "John Juarez Jones",
                "3j",
                "john@gmail.com"
            );
        }
    }
}
