using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace ConsoleApplication
{
    [LayoutRenderer("hello-world")]
    public class HelloWorldLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {            
            builder.Append("hello world");
        }
    }
}