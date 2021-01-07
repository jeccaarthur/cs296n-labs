using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Winterfell.Repositories;
using Winterfell.Controllers;
using Winterfell.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Tests
{
    public class MessageTests
    {
        private readonly ILogger<HomeController> _logger;

        [Fact]
        // test HomeController method that adds a message - Message
        public void TestAddMessage()
        {
            // arrange - create fake repo, controller, new message
            var fakeRepo = new FakeMessagesRepository();
            var controller = new HomeController(_logger, fakeRepo);
            var message = new Message()
            {
                Sender = new User() { Name = "Jecca" },
                Recipient = new User() { Name = "Misha" },
                Subject = "Bacon",
                Body = "Stop begging"
            };

            // act - add message to list via controller method
            controller.Message(message);

            // assert - confirm that message was added to fakeRepo, and that Date is correct
            var retrievedMessage = fakeRepo.Messages.ToList()[0];
            Assert.Equal(0, System.DateTime.Now.Date.CompareTo(retrievedMessage.Date.Date));
        }

        [Fact]
        // test HomeController method that displays all messages
        public void TestDisplayAllMessages()
        {
            // arrange - create list, fake repo, controller, new messages
            List<Message> messages = new List<Message>();
            var fakeRepo = new FakeMessagesRepository();
            var controller = new HomeController(_logger, fakeRepo);

            var message1 = new Message()
            {
                Sender = new User() { Name = "Jecca" },
                Recipient = new User() { Name = "Misha" },
                Subject = "Bacon",
                Body = "Stop begging"
            };

            var message2 = new Message()
            {
                Sender = new User() { Name = "Misha" },
                Recipient = new User() { Name = "Jecca" },
                Subject = "Bacon",
                Body = "Give me bacon"
            };

            var message3 = new Message()
            {
                Sender = new User() { Name = "Misha" },
                Recipient = new User() { Name = "Misha" },
                Subject = "Bacon",
                Body = "Get the bacon"
            };

            // act - add messages to list via controller method
            controller.Message(message1);
            controller.Message(message2);
            controller.Message(message3);

            // assert - confirm messages contains all 3 new messages
            messages = fakeRepo.Messages.ToList();
            var retrievedMessage1 = messages[0];
            var retrievedMessage2 = messages[1];
            var retrievedMessage3 = messages[2];

            Assert.Equal(3, messages.Count);
            Assert.Equal("Stop begging", retrievedMessage1.Body);
            Assert.Equal("Give me bacon", retrievedMessage2.Body);
            Assert.Equal("Get the bacon", retrievedMessage3.Body);
        }
    }
}
