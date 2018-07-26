using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FluentValidation
{
    public static class RequireUppercaseValidator
    {
        public static IRuleBuilderOptions<T, string> RequireUppercase<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(p=>p.Any(c => char.IsUpper(c))).WithMessage("Password require uppercase character");
        }
    }
}
