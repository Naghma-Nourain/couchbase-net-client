﻿using System;
using Couchbase.Configuration;
using Couchbase.Configuration.Client;
using Couchbase.Configuration.Server.Providers;
using NUnit.Framework;

namespace Couchbase.UnitTests.Configuration.Server.Providers
{
    [TestFixture]
    public class ConfigProviderBaseTests
    {
        private class TestConfigProvider : ConfigProviderBase
        {
            public TestConfigProvider(ClientConfiguration clientConfig)
                : base(clientConfig, null, null, null, null, null)
            {
            }

            public override IConfigInfo GetConfig(string name, string password)
            {
                throw new NotImplementedException();
            }

            public override bool RegisterObserver(IConfigObserver observer)
            {
                throw new NotImplementedException();
            }

            public override void UnRegisterObserver(IConfigObserver observer)
            {
                throw new NotImplementedException();
            }

            public override void Dispose()
            {
                throw new NotImplementedException();
            }

            public new BucketConfiguration GetOrCreateConfiguration(string bucketName)
            {
                return base.GetOrCreateConfiguration(bucketName);
            }
        }

        [TestCase]
        public void Subsequent_Buckets_Do_Not_Default_To_Use_Initial_Bucket_Password()
        {
            var clientConfig = new ClientConfiguration();
            clientConfig.BucketConfigs["default"].Password = "password";

            var provider = new TestConfigProvider(clientConfig);
            var sampleBucket = provider.GetOrCreateConfiguration("default");
            var beersBucket = provider.GetOrCreateConfiguration("sample");

            Assert.AreEqual(sampleBucket.Password, "password");
            Assert.AreEqual(beersBucket.Password, string.Empty);
        }
    }
}
