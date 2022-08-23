using System;
using MShare.Framework.Types;

namespace MShare.Framework.Application.Validation
{
    public class ValidatorsSpecification : SimpleSpecification<Type>
    {
        public static ValidatorsSpecification Instance => new();

        public override Func<Type, bool> Clause => x => !x.IsInterface && x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IRequestValidator<,>));
    }
}

