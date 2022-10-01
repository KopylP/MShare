using System;
namespace MShare.Framework.Infrastructure.Messaging
{
    public class MessageOptions : IMessageOptions
    {
        public string SelfUri { get; set; } = "";
    }
}

