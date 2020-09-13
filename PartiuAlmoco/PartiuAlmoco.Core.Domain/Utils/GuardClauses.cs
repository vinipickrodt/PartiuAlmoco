using PartiuAlmoco.Core.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ardalis.GuardClauses
{
    public static class GuardClausesExtensions
    {
        /// <summary>
        /// Validates if the e-mail is formed correctly.
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <param name="parameterName">Parameter name</param>
        public static void InvalidEmail(this IGuardClause guardClause, string email, string parameterName)
        {
            if (!Validation.IsValidEmail(email))
            {
                throw new ArgumentException(parameterName);
            }
        }
    }
}
