﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using SearchTool.Common;
using SearchTool.SearchComponent.Contracts;

namespace SearchTool.SearchComponent.Utilities
{
    /// <summary>
    /// Rest client to make GET API call
    /// </summary>
    internal class HttpRestAPIHelper : ICommunicationHelper
    {
        private readonly HttpClient m_HttpClient;

        public HttpRestAPIHelper()
        {
            m_HttpClient = new HttpClient();
        }

        /// <summary>
        /// Rest GET call
        /// </summary>
        /// <param name="uri">fully formatted URL</param>
        /// <returns></returns>
        public  async Task<IHttpAPIResponse> Get(string uri)
        {
            HTTPAPIResponse httpResp = new HTTPAPIResponse();
            ErrorCodes errorCode = ErrorCodes.NoError;

            try
            {
                HttpResponseMessage response = await m_HttpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Logger.Log("HTTP call succeeded. URL:" + uri);
                    string responseStringm = await response.Content.ReadAsStringAsync();
                    httpResp.ResponseString = responseStringm;
                }
                else
                {
                    Logger.Log("HTTP call failed with reason:" + response.ReasonPhrase + " for URL:" + uri);
                    errorCode = ErrorCodes.APIErrorResponse;
                }
            }
            catch (Exception exp)
            {
                Logger.Log("Exception while calling HTTP Get for URI" + uri + "Details:" + exp);
                errorCode = ErrorCodes.InternalException;
            }
            httpResp.Code = errorCode;
            return httpResp;
        }
    }
}
