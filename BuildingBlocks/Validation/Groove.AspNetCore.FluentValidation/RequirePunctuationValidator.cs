using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FluentValidation
{
    public static class RequirePunctuationValidator
    {
        public static IRuleBuilderOptions<T, string> RequirePunctuation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(p => p.Any(c => char.IsPunctuation(c))).WithMessage("Password require special character");
        }
    }
}
