﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SInnovations.Gis.Vector;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SInnovations.Gis.Vector.Layers;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using SInnovations.Gis.Vector.Projections;

namespace SInnovations.Gis.Tests
{

    public class ThisClassCouldBeAutoGenerated : OgrEntity
    {

        public dynamic prop1 { get; set; }
        public string prop0 { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        public string feature = @"{ ""type"": ""Feature"",
        ""geometry"": {""type"": ""Point"", ""coordinates"": [102.0, 0.5]},
        ""properties"": {""prop0"": ""value0""}
        }";

        public string featurecollection = @"{ ""type"": ""FeatureCollection"",
    ""features"": [
      { ""type"": ""Feature"",
        ""geometry"": {""type"": ""Point"", ""coordinates"": [102.0, 0.5]},
        ""properties"": {""prop0"": ""value0""}
        },
      { ""type"": ""Feature"",
        ""geometry"": {
          ""type"": ""LineString"",
          ""coordinates"": [
            [102.0, 0.0], [103.0, 1.0], [104.0, 0.0], [105.0, 1.0]
            ]
          },
        ""properties"": {
          ""prop0"": ""value0"",
          ""prop1"": 0.0
          }
        },
      { ""type"": ""Feature"",
         ""geometry"": {
           ""type"": ""Polygon"",
           ""coordinates"": [
             [ [100.0, 0.0], [101.0, 0.0], [101.0, 1.0],
               [100.0, 1.0], [100.0, 0.0] ]
             ]
         },
         ""properties"": {
           ""prop0"": ""value0"",
           ""prop1"": {""this"": ""that""}
           }
         }
       ]
     }";
        [TestMethod]
        public void TestMethod1()
        {
            var feat = JObject.Parse(feature);
            var collection = JObject.Parse(featurecollection);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new DbGeographyGeoJsonConverter());
            settings.Converters.Add(new OgrEntityConverter());

            var obj = feat.ToObject<ThisClassCouldBeAutoGenerated>(JsonSerializer.Create(settings));
            var objs = collection.ToObject<ThisClassCouldBeAutoGenerated[]>(JsonSerializer.Create(settings));
            

        }

        [TestMethod]
        public void TeestMethod2()
        {
            var json = @"{""boundingPolygon"":{""type"":""Polygon"",""coordinates"":[[[1254031.3317415663,7554983.1377011435],[1263356.6491923577,7557581.99666284],[1275739.4477745562,7553607.2711920105],[1286440.6317344808,7547645.182985767],[1297600.4378641166,7529453.170253895],[1294542.9567327097,7521350.845255666],[1294390.0826761392,7510955.409408882],[1299893.548712672,7504381.824976358],[1314110.8359737147,7494292.137242714],[1318849.9317273956,7496432.374034699],[1330315.485970172,7490776.033941596],[1335054.581723853,7485578.316018204],[1332455.722762157,7482520.834886797],[1325117.76804678,7483743.82733936],[1313040.7175777222,7484355.323565641],[1300352.170882383,7486495.560357626],[1291638.349657873,7497502.492430692],[1277268.1883402597,7512331.275918015],[1278949.8029625337,7527465.80751848],[1268707.24117232,7541835.968836093],[1259076.175608388,7545657.820250352],[1254031.3317415663,7554983.1377011435]]],""crs"":{""type"":""name"",""properties"":{""name"":""EPSG:3857""}}}}";

            var obj1 = JsonConvert.DeserializeObject<TestClass1>(json);
            Assert.AreEqual(3857, obj1.boundingPolygon.WellKnownValue.CoordinateSystemId);
            var obj2 = JsonConvert.DeserializeObject<TestClass2>(json);
            Assert.AreEqual(4326, obj2.boundingPolygon.WellKnownValue.CoordinateSystemId);
            var obj3 = JsonConvert.DeserializeObject<TestClass3>(json);
            Assert.AreEqual(25832, obj3.boundingPolygon.WellKnownValue.CoordinateSystemId);
        }

        public class TestClass1
        {
            [JsonConverter(typeof(DbGeographyGeoJsonConverter))]
            public DbGeometry boundingPolygon { get; set; }
        }
        public class TestClass2
        {
            [JsonConverter(typeof(EpsgDbGeometryConverter), 4326)]
            public DbGeometry boundingPolygon { get; set; }
        }
        public class TestClass3
        {

            [JsonConverterAttribute(typeof(EpsgDbGeometryConverter), 25832)]
            public DbGeometry boundingPolygon { get; set; }
        }
        //public void ThisCouldBeAWebAPIController(JToken json)
        //{
        //    VectorLayer<OgrEntity, OgrEntity> layer = null; //this is the VectorLayer<T,T1>
        //    layer.Add(json);
        //}
    }
}
