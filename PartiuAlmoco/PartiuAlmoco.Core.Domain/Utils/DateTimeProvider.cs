using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Utils
{
    /// <summary>
    /// O único objetivo desta classe é facilitar os testes. Não usar para outros fins.
    /// </summary>
    public abstract class DateTimeProvider
    {
        private static DateTime? _customDate = null;

        public static DateTime Now
        {
            get { return !_customDate.HasValue ? DateTime.Now : _customDate.Value; }
        }

        public static void SetCustomDate(DateTime customDate)
        {
            _customDate = customDate;
        }

        public static void ResetToDefault()
        {
            _customDate = null;
        }
    }
}
