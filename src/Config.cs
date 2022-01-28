using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
//using System.Text.Json;
//using System.Text.Json.Serialization;

using Utf8Json;

namespace Trifolium {
	public class Config {
		public class whitelistClass : gameserverClass {
			//[JsonPropertyName("enabled")]
			public bool enabled { get; set; }
			//[JsonPropertyName("location")]
			public string location { get; set; }
		}
		public class gameserverClass : CfgClass {
			//[JsonPropertyName("port")]
			public int port { get; set; }
			//[JsonPropertyName("whitelist")]
			public whitelistClass whitelist { get; set; }
			//[JsonPropertyName("chatEnabled")]
			public bool chatEnabled { get; set; }
		}
		public class CfgClass {
			//[JsonPropertyName("gameVersion")]
			public string gameVersion;
			//[JsonPropertyName("gameserver")]
			public gameserverClass gameserver;
			//[JsonPropertyName("chatEnabled")]
		}

		public static CfgClass GetConfig() {
			CfgClass cfg = JsonSerializer.Deserialize<CfgClass>(File.ReadAllText("config.json"));

			return cfg;
		}
	}
}