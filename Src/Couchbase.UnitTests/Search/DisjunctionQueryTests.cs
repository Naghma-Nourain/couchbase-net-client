﻿using System;
using Couchbase.Search;
using Couchbase.Search.Queries.Compound;
using Couchbase.Search.Queries.Simple;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Couchbase.UnitTests.Search
{
    [TestFixture]
    public class DisjunctionQueryTests
    {
        [Test]
        public void Boost_ReturnsDisjunctionQuery()
        {
            var query = new DisjunctionQuery().Boost(2.2);

            Assert.IsInstanceOf<DisjunctionQuery> (query);
        }

        [Test]
        public void Boost_WhenBoostIsLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var query = new DisjunctionQuery();

            Assert.Throws<ArgumentOutOfRangeException>(() => query.Boost(-.1));
        }

        [Test]
        public void Export_ReturnsValidJson()
        {
            var query = new DisjunctionQuery(
                new TermQuery("hotel").Field("type")
            );

            var result = query.Export().ToString(Formatting.None);

            var expected = JsonConvert.SerializeObject(new
            {
                query = new
                {
                    boost = 0.0,
                    min = 1,
                    disjuncts = new[]
                    {
                        new
                        {
                            query = new
                            {
                                term = "hotel",
                                boost = 0.0,
                                field = "type",
                                prefix_length = 0,
                                fuzziness = 0
                            }
                        }
                    }
                }
            }, Formatting.None);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Export_With_SearchParams_ReturnsValidJson()
        {
            var query = new DisjunctionQuery(
                new TermQuery("hotel").Field("type")
            );

            var searchParams = new SearchParams();
            var result = query.Export(searchParams).ToString(Formatting.None);

            var expected = JsonConvert.SerializeObject(new
            {
                ctl = new
                {
                    timeout = 75000
                },
                query = new
                {
                    boost = 0.0,
                    min = 1,
                    disjuncts = new[]
                    {
                        new
                        {
                            query = new
                            {
                                term = "hotel",
                                boost = 0.0,
                                field = "type",
                                prefix_length = 0,
                                fuzziness = 0
                            }
                        }
                    }
                }
            }, Formatting.None);

            Assert.AreEqual(expected, result);
        }
    }
}

#region [ License information          ]

/* ************************************************************
 *
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2015 Couchbase, Inc.
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * ************************************************************/

#endregion
