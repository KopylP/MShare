using System;
using MassTransit;
using MShare.Framework.Api;

namespace MShare.Framework.Infrastructure.Execution
{
    internal class ContextUpdater : IExecutionContextUpdater
    {
        private readonly IExecutionContext _context;

        public ContextUpdater(IExecutionContext context)
        {
            _context = context;
        }

        public void Update(IExecutionContext newContext)
        {
            if (_context is ExecutionContext context)
            {
                context.UpdateFrom(newContext);
            }
        }
    }

    public static class IContextUpdaterExtentions
    {
        public static void Update(this IExecutionContextUpdater updater, ConsumeContext context)
        {
            var executionContext = context.GetHeader<IExecutionContext>(nameof(IExecutionContext));
            updater.Update(executionContext);
        }
    }
}

