using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Segment.Test
{
    [TestFixture()]
    public class ConnectionTests
    {
        [SetUp]
        public void Init()
        {
            Logger.Handlers += LoggingHandler;
        }

        [TearDown]
        public void Dispose()
        {
            Logger.Handlers -= LoggingHandler;
        }

        [Test()]
        public void ProxyTestNet35()
        {
            // Set proxy address, like as "http://localhost:8888"
            Analytics.Initialize(Constants.WRITE_KEY, new Config().SetAsync(false).SetProxy(""));

            Actions.Identify(Analytics.Client);

            Assert.AreEqual(1, Analytics.Client.Statistics.Submitted);
            Assert.AreEqual(1, Analytics.Client.Statistics.Succeeded);
            Assert.AreEqual(0, Analytics.Client.Statistics.Failed);

            Analytics.Dispose();
        }

        [Test()]
        public void GZipTestNet35()
        {
            // Set GZip/Deflate on request header
            Analytics.Initialize(Constants.WRITE_KEY, new Config().SetAsync(false).SetRequestCompression(true));

            Actions.Identify(Analytics.Client);

            Assert.AreEqual(1, Analytics.Client.Statistics.Submitted);
            Assert.AreEqual(1, Analytics.Client.Statistics.Succeeded);
            Assert.AreEqual(0, Analytics.Client.Statistics.Failed);

            Analytics.Dispose();
        }

        static void LoggingHandler(Logger.Level level, string message, IDictionary<string, object> args)
        {
            if (args != null)
            {
                foreach (string key in args.Keys)
                {
                    message += String.Format(" {0}: {1},", "" + key, "" + args[key]);
                }
            }
            Console.WriteLine(String.Format("[ConnectionTests] [{0}] {1}", level, message));
        }
    }
}
