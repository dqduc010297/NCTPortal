using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FluentValidation
{
    public static class RequireDigitValidator
    {
        public static IRuleBuilderOptions<T, string> RequireDigit<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            //return ruleBuilder.Must(p => Regex.IsMatch(p.ToString(), @"^\d$")).WithMessage("Require digit");
            return ruleBuilder.Must(p => p.Any(c => char.IsNumber(c))).WithMessage("Password require digit character");
        }
    }
}
