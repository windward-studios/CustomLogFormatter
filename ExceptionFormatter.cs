using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kailua.net.windward.utils
{
    public class ExceptionFormatter : log4net.Layout.LayoutSkeleton
    {
        private PatternLayout patternLayout;
        PatternLayout PatternLayout
        {
            get { return patternLayout ?? (patternLayout = new PatternLayout(Pattern)); }
        }

        public String Pattern { get; set; }
        
        public override void ActivateOptions()
        {
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            if(loggingEvent == null)
            {
                throw new ArgumentNullException("loggingEvent");
            }

            String header = String.Empty;
            PatternLayout.Format(writer, loggingEvent);
            Exception curEx = loggingEvent.ExceptionObject;

            while (curEx != null)
            {
                writer.Write(curEx.GetType().FullName + ": " + curEx.Message);
                writer.Write(header + "\nStackTrace:    ");
                if (curEx.StackTrace != null)
                {
                    writer.Write(header + curEx.StackTrace.ToString());
                }
                curEx = curEx.InnerException;
                if (curEx != null)
                {
                    writer.Write("\nINNER EXCEPTION:    ");
                }
            }
        }
    }
}
