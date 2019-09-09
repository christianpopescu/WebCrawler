using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WebLayerService
{
    public class WebClientHelper
    {
        #region Http Helpers

        /// <summary>
        /// Get the content of an URL Synchronous Version
        /// </summary>
        /// <param name="pUrl">
        /// The URL the defines the content to get</param>
        /// <returns>
        /// Byte array of the answer
        /// </returns>
        public Stream GetURLContents(string pUrl)
        {
            if (pUrl == null) throw new ArgumentNullException("pUrl");
            // The downloaded resource ends up in the variable named content. 
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL. 
            var webReq = (HttpWebRequest) WebRequest.Create(pUrl);

            // Send the request to the Internet resource and wait for 
            // the response. 
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app. 
            using (WebResponse response = webReq.GetResponse())
            {
                // Get the data stream that is associated with the specified URL. 
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.  
                    if (responseStream != null) responseStream.CopyTo(content);
                }
            }

            // Return the result as a byte array. 
            return content;
        }


        /// <summary>
        /// Get the content of an URL Asynchronous Version
        /// </summary>
        /// <param name="pUrl">
        /// The URL the defines the content to get</param>
        /// <returns>
        /// Stream a
        /// </returns>
        public async Task<Stream> GetURLContentsAsync(string pUrl)
        {
            // The downloaded resource ends up in the variable named content. 
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL. 
            var webReq = (HttpWebRequest) WebRequest.Create(pUrl);

            // Send the request to the Internet resource and wait for 
            // the response.                 
            using (WebResponse response = await webReq.GetResponseAsync())

                // The previous statement abbreviates the following two statements. 

                //Task<WebResponse> responseTask = webReq.GetResponseAsync(); 
                //using (WebResponse response = await responseTask)
            {
                // Get the data stream that is associated with the specified pUrl. 
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content. 
                    if (responseStream != null) await responseStream.CopyToAsync(content);

                    // The previous statement abbreviates the following two statements. 

                    // CopyToAsync returns a Task, not a Task<T>. 
                    //Task copyTask = responseStream.CopyToAsync(content); 

                    // When copyTask is completed, content contains a copy of 
                    // responseStream. 
                    //await copyTask;
                }
            }
            // Return the result as stream.
            return content;
        }

        #endregion

        #region IO Helpers

        public async void WriteStreamAsTextFile(string pOutputFileName, Stream pStreamToWrite)
        {
            pStreamToWrite.Position = 0; //Go to start
            using (var sr = new StreamReader(pStreamToWrite))
            {
                using (var sw = new StreamWriter(pOutputFileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        await sw.WriteLineAsync(line);
                }
            }
        }

        public async void WriteStreamAsBinaryToFile(string pOutputFileName, Stream pStreamToWrite)
        {
            pStreamToWrite.Position = 0; //Go to start
            using (var br = new BinaryReader(pStreamToWrite))
            {
                using (var bw = new BinaryWriter(File.Open(pOutputFileName, FileMode.Create)))
                {
                    byte[] buffer;
                    int len = 0;
                    while ((len=(buffer = br.ReadBytes(1024)).Length)>0)   
                        bw.Write(buffer,0,len);
                }
            }
        }
        #endregion
    }
}
