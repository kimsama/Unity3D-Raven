using UnityEngine;
using System;
using Newtonsoft.Json;

namespace SharpRaven.Data {
    public class SentryException {
        /// <summary>
        /// The type of exception.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type;
        /// <summary>
        /// The message of the exception.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value;
        /// <summary>
        /// The module where the exception happened.
        /// </summary>
        [JsonProperty(PropertyName = "module")]
        public string Module;

        public SentryException(Exception e) {
            this.Module = e.Source;
            this.Type = e.Message;
            this.Value = e.Message;
        }
		
		public SentryException(string log, string stack, LogType logType)
		{
            this.Module = log;
            this.Type = logType.ToString();
            this.Value = stack;			
		}
    }
}
