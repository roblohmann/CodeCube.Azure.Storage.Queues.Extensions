using Azure.Storage.Queues.Models;
using System;
using System.Text;
using CodeCube.Core.Exentions;

namespace CodeCube.Azure.Storage.Queues.Extensions
{
    //NOTE: Code gracefully taken from https://briancaos.wordpress.com/2021/06/16/net-core-queuemessage-json-to-typed-object-using-azure-storage-queues/
    public static class QueueMessageExtensions
    {
        /// <summary>
        /// Retrieve the content from the message on the queue as string.
        /// </summary>
        /// <param name="message">The <see cref="QueueMessage"/> to retrieve the content from.</param>
        /// <returns>Content from the <see cref="QueueMessage"/> as UTF8-encoded string.</returns>
        public static string AsString(this QueueMessage message)
        {
            byte[] data = Convert.FromBase64String(message.MessageText);
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// Retrieve the content from the message on the queue as <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to return.</typeparam>
        /// <param name="message">The <see cref="QueueMessage"/> to retrieve the content from.</param>
        /// <returns>The content from the <see cref="QueueMessage"/> deserialized as <see cref="T"/></returns>
        public static T As<T>(this QueueMessage message) where T : class
        {
            byte[] data = Convert.FromBase64String(message.MessageText);
            string json = Encoding.UTF8.GetString(data);

            //return Deserialize<T>(json, true);
            return json.DeserializeFromCamelCase<T>();
        }

        // #region privates
        // private static T Deserialize<T>(string json, bool ignoreMissingMembersInObject) where T : class
        // {
        //     MissingMemberHandling missingMemberHandling = MissingMemberHandling.Error;
        //     if (ignoreMissingMembersInObject)
        //         missingMemberHandling = MissingMemberHandling.Ignore;
        //
        //     return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { MissingMemberHandling = missingMemberHandling });
        // }
        // #endregion
    }
}