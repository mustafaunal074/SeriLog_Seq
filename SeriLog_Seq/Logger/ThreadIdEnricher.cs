using Serilog.Core;
using Serilog.Events;
using System.Threading;

namespace SeriLog_Seq.Logger
{
    public class ThreadIdEnricher : ILogEventEnricher
    {
        /*
         ******** Dinamik Enrich Belirleme *********
         
         Log atılmadan önce ilgili log’a dair atanacak bir value varsa bunu dinamik olarak
         ‘ILogEventEnricher’ interface’i eşliğinde geliştirebilir ve ‘Enrich.With(…)’
         fonksiyonuyla sisteme entegre edebiliriz..

        Aşağıdaki örnekte

        Misal;
        LogEvent ile birlikte ThreadId değerinin kaydedilmesini istiyorsak eğer bunun için dinamik enrich kullanmamız gerekmektedir.

        sonra bu enrichi gidp eklememiz gerekir.!!!!!!!!!!

         */

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "ThreadId", Thread.CurrentThread.ManagedThreadId));
        }
    }
}
